using Mechanico_Api.Contexts;
using Mechanico_Api.Entities;

namespace Mechanico_Api.Interfaces;

public interface IUserRepository
{
    public Task<ActionResult> SendCode(string phoneNumber);
    public Task<ActionResult> CheckCode(string phoneNumber, string code);
    public Task<User?> GetUserById(Guid userId);
}