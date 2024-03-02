using Mechanico_Api.Contexts;
using Mechanico_Api.Entities;
using Mechanico_Api.Interfaces;

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
        if (user is null)
        {
            var createdUser = await  _appDbContext.Users.AddAsync(new User { PhoneNumber = phoneNumber });
            user = createdUser.Entity;
        }

        return await _smsCodeRepository.SendCode(user.Id);
    }

    private User? GetUserByPhoneNumber(string phoneNumber)
    {
        return  _appDbContext.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
    }

}