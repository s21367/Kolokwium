using Kolokwium.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KolokwiumPoprawa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private IDatabaseService Service { get; }

        public MemberController(IDatabaseService service)
            => Service = service;

        [HttpPost("teams/{idTeam}/members/{idMember}")]
        public async Task<IActionResult> AddMemberToTeam(int idTeam, int idMember)
        {
            var result = await Service.AddMember(idMember, idTeam);
            
            return result switch
            {
                "Added" => Ok("Member added"),
                "TeamNotFound" => NotFound("Team not found"),
                "MemberNotFound" => NotFound("Member not found"),
                "OrganizationMismatch" => BadRequest("Member not in the same organization that team"),
                "AlreadyExists" => BadRequest("Already exist"),
                _ => BadRequest(result),
            };
        } 

    }
}
