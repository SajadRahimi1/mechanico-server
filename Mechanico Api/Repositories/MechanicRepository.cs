using Courseproject.Common.Interfaces;
using Mechanico_Api.Contexts;
using Mechanico_Api.Dtos;
using Mechanico_Api.Entities;
using Mechanico_Api.Interfaces;
using Mechanico_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Mechanico_Api.Repositories;

public class MechanicRepository : IMechanicRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ISmsCodeRepository _smsCodeRepository;
    private readonly IJwtRepository _jwtRepository;
    private readonly IFileRepository _fileRepository;

    public MechanicRepository(AppDbContext appDbContext, ISmsCodeRepository smsCodeRepository, IJwtRepository jwtRepository, IFileRepository fileRepository)
    {
        _appDbContext = appDbContext;
        _smsCodeRepository = smsCodeRepository;
        _jwtRepository = jwtRepository;
        _fileRepository = fileRepository;
    }
    
    public async Task<ActionResult> GetMechanic(Guid mechanicId)
    {
        var mechanic = await _appDbContext.Mechanics.Include(m => m.Comments).Include(m => m.Categories)
            .SingleOrDefaultAsync(m => m.Id == mechanicId);
        return mechanic is null
            ? new ActionResult(new Result { StatusCode = 404, Message = "مکانک یافت نشد" })
            : new ActionResult(new Result { Data = mechanic });
    }

    public async Task<ActionResult> VisitMechanic(Guid mechanicId, string userId)
    {
        var mechanic = await GetMechanicById(mechanicId);
        if (mechanic is null) return new ActionResult(new Result { StatusCode = 404, Message = "مکانک یافت نشد" });
        var user = await _appDbContext.Users.SingleOrDefaultAsync(u => u.Id.ToString() == userId);
        var visited = new Visited
        {
            Mechanic = mechanic,
            User = user,
            MechanicId = mechanic.Id,
            UserId = user.Id
        };
        var savedVisited = await _appDbContext.Visiteds.AddAsync(visited);
        await _appDbContext.SaveChangesAsync();
        return new ActionResult(new Result { Data = savedVisited.Entity });
    }

    public Task<ActionResult> CommentMechanic(Comment comment)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult> GetMechanicByCity(string city)
    {
        var mechanics = await _appDbContext.Mechanics.Where(m => m.City == city && m.Status == MechanicStatus.Accepted)
            .ToListAsync();
        return new ActionResult(new Result { Data = mechanics });
    }

    private async Task<Mechanic?> GetMechanicById(Guid id) =>
        await _appDbContext.Mechanics.SingleOrDefaultAsync(m => m.Id == id);

    private async Task<Mechanic?> GetMechanicByPhoneNumber(string phoneNumber) =>
        await _appDbContext.Mechanics.SingleOrDefaultAsync(m => m.PhoneNumber == phoneNumber);

    public async Task<ActionResult> SendCode(string phoneNumber)
    {
        var mechanic = await GetMechanicByPhoneNumber(phoneNumber);
        if (mechanic is not null) return await _smsCodeRepository.SendCode(mechanic.Id);

        var createdMechanic = await _appDbContext.Mechanics.AddAsync(new Mechanic { PhoneNumber = phoneNumber });
        mechanic = createdMechanic.Entity;
        await _appDbContext.SaveChangesAsync();

        return await _smsCodeRepository.SendCode(mechanic.Id);
    }

    public async Task<ActionResult> UpdateMechanic(Mechanic mechanic)
    {
        var oldMechanic = await GetMechanicById(mechanic.Id);
        if (oldMechanic is null)
            return new ActionResult(new Result { StatusCode = 401, Message = "توکن شما اشتباه است" });
        mechanic.PhoneNumber = oldMechanic.PhoneNumber;

        var updatedMechanic = _appDbContext.Mechanics.Update(mechanic);
        await _appDbContext.SaveChangesAsync();
        return new ActionResult(new Result { Data = updatedMechanic.Entity });
    }

    public async Task<ActionResult> CheckCode(string phoneNumber, string code)
    {
        var mechanic = await GetMechanicByPhoneNumber(phoneNumber);
        if (mechanic is null)
            return new ActionResult(new Result { StatusCode = 404, Message = "کاربری با این شماره یافت نشد" });
        var isCodeValid = await _smsCodeRepository.CheckCode(mechanic.Id, code);
        if (!isCodeValid)
            return new ActionResult(new Result
            {
                StatusCode = 403,
                Message = "کد وارد شده درست نیست"
            });

        var token = _jwtRepository.GenerateUserJwt(new JwtModel { Id = mechanic.Id.ToString(), Role = "mechanic" });
        return new ActionResult(new Result { StatusCode = 200, Message = "کد وارد شده درست است", Token = token });
    }

    public async Task<ActionResult> GetUserVisited(string mechanicId)
    {
        if (mechanicId.IsNullOrEmpty())
            return new ActionResult(new Result { StatusCode = 401, Message = "توکن صحیح نیست" });
        var mechanic = await _appDbContext.Mechanics.Include(m => m.Visiteds)
            .SingleOrDefaultAsync(u => u.Id.ToString() == mechanicId);
        return new ActionResult(new Result { Data = mechanic });
    }

    public async Task<ActionResult> GetUserCommented(string mechanicId)
    {
        if (mechanicId.IsNullOrEmpty())
            return new ActionResult(new Result { StatusCode = 401, Message = "توکن صحیح نیست" });
        var mechanic = await _appDbContext.Mechanics.Include(m => m.Comments)
            .SingleOrDefaultAsync(u => u.Id.ToString() == mechanicId);
        mechanic.Comments= mechanic.Comments.Where(c => c.CommentStatus == CommentStatus.Accepted).ToList();
        return new ActionResult(new Result { Data = mechanic });
    }

    public async Task<ActionResult> UpdateLicenseImage(LicenseImageDto licenseImageDto, string mechanicId)
    {
        var mechanic = await GetMechanicById(Guid.Parse(mechanicId));
        if (mechanic is null)
        {
            return new ActionResult(new Result { StatusCode = 404 });
        }

        mechanic.LicenseImage = await _fileRepository.SaveFileAsync(licenseImageDto.LicenseImage);
        mechanic = _appDbContext.Mechanics.Update(mechanic).Entity;
        await _appDbContext.SaveChangesAsync();

        return new ActionResult(new Result { Data = mechanic });
    }
}