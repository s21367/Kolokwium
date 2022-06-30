using Kolokwium.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KolokwiumPoprawa.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private IDatabaseService Service { get; }

        public TeamController(IDatabaseService service)
            => Service = service;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var result = await Service.GetTeam(id);
            
            return result == null ? NotFound("Team does not exist") : Ok(result);
        }

    }
}
