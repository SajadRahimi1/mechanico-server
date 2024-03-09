using Mechanico_Api.Contexts;
using Mechanico_Api.Entities;
using Mechanico_Api.Interfaces;
using Mechanico_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActionResult = Mechanico_Api.Contexts.ActionResult;

namespace Mechanico_Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ISmsCodeRepository _smsCodeRepository;
    private readonly IJwtRepository _jwtRepository;

    public UserRepository(AppDbContext appDbContext, ISmsCodeRepository smsCodeRepository, IJwtRepository jwtRepository)
    {
        _appDbContext = appDbContext;
        _smsCodeRepository = smsCodeRepository;
        _jwtRepository = jwtRepository;
    }

    public async Task<ActionResult> SendCode(string phoneNumber)
    {
        var user = GetUserByPhoneNumber(phoneNumber);
        if (user is not null) return await _smsCodeRepository.SendCode(user.Id);

        var createdUser = await _appDbContext.Users.AddAsync(new User { PhoneNumber = phoneNumber });
        user = createdUser.Entity;
        await _appDbContext.SaveChangesAsync();

        return await _smsCodeRepository.SendCode(user.Id);
    }

    public async Task<ActionResult> CheckCode(string phoneNumber, string code)
    {
        var user = GetUserByPhoneNumber(phoneNumber);
        if (user is null)
            return new ActionResult(new Result { StatusCode = 404, Message = "کاربری با این شماره یافت نشد" });
        var isCodeValid = await _smsCodeRepository.CheckCode(user.Id, code);
        if (!isCodeValid)
            return new ActionResult(new Result
            {
                StatusCode = 403,
                Message = "کد وارد شده درست نیست"
            });

        var token = _jwtRepository.generateUserJwt(new JwtModel { Id = user.Id.ToString() });
        return new ActionResult(new Result { StatusCode = 200, Message = "کد وارد شده درست است", Token = token });
    }

    public Task<User?> GetUserById(Guid userId)
    {
        return _appDbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<ActionResult> GetAll()
    {
        var users = await _appDbContext.Users.ToListAsync();
        return new ActionResult(new Result { Data = users });
    }

    public async Task<User?> UpdateUser(User user)
    {
        var selectUser = await GetUserById(user.Id);
        if (selectUser is null)
        {
            return null;
        }
        var updatedUser=  _appDbContext.Users.Update(user);
        _appDbContext.ChangeTracker.Clear();
        await _appDbContext.SaveChangesAsync();
        return updatedUser.Entity;
    }

    public User? GetUserByPhoneNumber(string phoneNumber)
    {
        return _appDbContext.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
    }
    
    
}