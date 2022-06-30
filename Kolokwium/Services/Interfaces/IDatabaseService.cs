using KolokwiumPoprawa.Models.DTO;
using System.Threading.Tasks;

namespace Kolokwium.Services.Interfaces
{
    public interface IDatabaseService
    {
        Task<NewTeam> GetTeam(int id);

        Task<string> AddMember(int memberId, int teamId);
    }
}
