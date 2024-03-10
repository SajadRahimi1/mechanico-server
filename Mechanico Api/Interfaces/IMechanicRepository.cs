using Mechanico_Api.Contexts;
using Mechanico_Api.Dtos;
using Mechanico_Api.Entities;

namespace Mechanico_Api.Interfaces;

public interface IMechanicRepository
{
    public Task<ActionResult> GetMechanic(Guid mechanicId);
    public Task<ActionResult> VisitMechanic(Guid mechanicId, string userId);
    public Task<ActionResult> CommentMechanic(Comment comment);
    public Task<ActionResult> GetMechanicByCity(string city);
    public Task<ActionResult> CheckCode(string phoneNumber, string code);
    public Task<ActionResult> SendCode(string phoneNumber);
    public Task<ActionResult> UpdateMechanic(Mechanic mechanic);
    
    public Task<ActionResult> GetUserVisited(string mechanicId);
    
    public Task<ActionResult> GetUserCommented(string mechanicId);

    public Task<ActionResult> UpdateLicenseImage(LicenseImageDto licenseImageDto,string mechanicId);
}