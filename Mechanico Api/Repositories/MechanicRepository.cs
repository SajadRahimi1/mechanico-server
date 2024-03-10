using Mechanico_Api.Contexts;
using Mechanico_Api.Entities;
using Mechanico_Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mechanico_Api.Repositories;

public class MechanicRepository : IMechanicRepository
{
    private readonly AppDbContext _appDbContext;

    public MechanicRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
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

    private async Task<Mechanic?> GetMechanicById(Guid id) =>
        await _appDbContext.Mechanics.SingleOrDefaultAsync(m => m.Id == id);
}