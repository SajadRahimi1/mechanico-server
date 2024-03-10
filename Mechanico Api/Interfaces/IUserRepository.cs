using Mechanico_Api.Dtos;
using Mechanico_Api.Entities;
using ActionResult = Mechanico_Api.Contexts.ActionResult;

namespace Mechanico_Api.Interfaces;

public interface IUserRepository
{
    public Task<ActionResult> SendCode(string phoneNumber);
    public Task<ActionResult> CheckCode(string phoneNumber, string code);
    public Task<User?> GetUserById(Guid userId);

    public Task<ActionResult> GetAll();

    public Task<User?> UpdateUser(User user);

    public Task<ActionResult> GetUserVisited(string userId);
    
    public Task<ActionResult> GetUserCommented(string userId);

}