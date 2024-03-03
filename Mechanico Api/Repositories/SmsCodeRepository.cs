using Mechanico_Api.Contexts;
using Mechanico_Api.Entities;
using Mechanico_Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mechanico_Api.Repositories;

public class SmsCodeRepository : ISmsCodeRepository
{
    private readonly AppDbContext _appDbContext;

    public SmsCodeRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public SmsCode? GetSmsByReceiverId(Guid receiverId)
    {
        return _appDbContext.SmsCodes.SingleOrDefault(s => s.ReceiverId == receiverId);
    }

    public async Task<ActionResult> SendCode(Guid receiverId)
    {
        var smsCode = GetSmsByReceiverId(receiverId);
        if (smsCode is null)
        {
            string code = new Random().Next(1000, 9999).ToString();
            var newSmsCode =
                await _appDbContext.SmsCodes.AddAsync(new SmsCode { ReceiverId = receiverId, Code = code });
            //TODO: call api for send sms 
            return new ActionResult(new Result { Message = "کد با موفقیت ارسال شد", StatusCode = 201 });
        }
        else
        {
            if (DateTime.Now.Subtract(smsCode.UpdatedAt).Minutes < 2)
            {
                return new ActionResult(new Result { Message = "کد با موفقیت ارسال شد", StatusCode = 200 });
            }

            var code = new Random().Next(1000, 9999).ToString();
            smsCode.Code = code;
            _appDbContext.SmsCodes.Update(smsCode);
            // _appDbContext.ChangeTracker.Clear();
            await _appDbContext.SaveChangesAsync();

            return new ActionResult(new Result { Message = "کد با موفقیت ارسال شد", StatusCode = 202 });
        }
    }
}