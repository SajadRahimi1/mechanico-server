using Mechanico_Api.Contexts;
using Mechanico_Api.Entities;
using Mechanico_Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActionResult = Mechanico_Api.Contexts.ActionResult;

namespace Mechanico_Api.Repositories;

public class UserRepository:IUserRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ISmsCodeRepository _smsCodeRepository;

    public UserRepository(AppDbContext appDbContext, ISmsCodeRepository smsCodeRepository)
    {
        _appDbContext = appDbContext;
        _smsCodeRepository = smsCodeRepository;
    }


    public async Task<ActionResult> SendCode(string phoneNumber)
    {
        var user = GetUserByPhoneNumber(phoneNumber);
        if (user is not null) return await _smsCodeRepository.SendCode(user.Id);
        
        var createdUser = await  _appDbContext.Users.AddAsync(new User { PhoneNumber = phoneNumber });
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
        return new ActionResult(new Result{StatusCode = isCodeValid?200:403,Message = isCodeValid? "کد وارد شده درست است": "کد وارد شده درست نیست"});
    }

    public Task<User?> GetUserById(Guid userId)
    {
        return _appDbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<ActionResult> GetAll()
    {
        var users= await _appDbContext.Users.ToListAsync();
        return new ActionResult(new Result { Data = users });
    }

    public User? GetUserByPhoneNumber(string phoneNumber)
    {
        return  _appDbContext.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
    }

}