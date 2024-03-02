using Mechanico_Api.Contexts;
using Mechanico_Api.Entities;

namespace Mechanico_Api.Interfaces;

public interface ISmsCodeRepository
{
    public SmsCode? GetSmsByReceiverId(Guid receiverId);

    public Task<ActionResult> SendCode(Guid receiverId);
}