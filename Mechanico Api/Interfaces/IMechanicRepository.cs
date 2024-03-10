using Mechanico_Api.Contexts;
using Mechanico_Api.Entities;

namespace Mechanico_Api.Interfaces;

public interface IMechanicRepository
{
    public Task<ActionResult> GetMechanic(Guid mechanicId);
    public Task<ActionResult> VisitMechanic(Guid mechanicId, string userId);
    public Task<ActionResult> CommentMechanic(Comment comment);
}