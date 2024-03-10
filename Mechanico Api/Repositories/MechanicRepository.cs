using Mechanico_Api.Contexts;
using Mechanico_Api.Entities;
using Mechanico_Api.Interfaces;
using Mechanico_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Mechanico_Api.Repositories;

public class MechanicRepository : IMechanicRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ISmsCodeRepository _smsCodeRepository;
    private readonly IJwtRepository _jwtRepository;

    public MechanicRepository(AppDbContext appDbContext, ISmsCodeRepository smsCodeRepository,
        IJwtRepository jwtRepository)
    {
        _appDbContext = appDbContext;
        _smsCodeRepository = smsCodeRepository;
        _jwtRepository = jwtRepository;
    }

    public async Task<ActionResult> GetMechanic(Guid mechanicId)
    {
        var mechanic = await GetMechanicById(mechanicId);
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
        var mechanics = await _appDbContext.Mechanics.Where(m => m.City == city)
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

        var token = _jwtRepository.generateUserJwt(new JwtModel { Id = mechanic.Id.ToString(), Role = "mechanic" });
        return new ActionResult(new Result { StatusCode = 200, Message = "کد وارد شده درست است", Token = token });
    }
}