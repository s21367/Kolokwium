using Kolokwium.Contexts;
using Kolokwium.Services.Interfaces;
using KolokwiumPoprawa.Models;
using KolokwiumPoprawa.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KolokwiumPoprawa.Services
{
    public class DatabaseService : IDatabaseService
    {
        private DatabaseContext Context { get; }

        public DatabaseService(DatabaseContext context)
            => Context = context;

        public async Task<string> AddMember(int memberId, int teamId)
        {
            var members = Context.Members;

            var team = await Context.Teams 
                .Where(e => e.TeamID == teamId)
                .FirstOrDefaultAsync();
            
            if (team is null)
            {
                return "TeamNotFound";
            }

            var member = await Context.Members
                .Where(e => e.MemberID == memberId)
                .FirstOrDefaultAsync();
            
            if (member is null)
            {
                return "MemberNotFound";
            }

            if (team.OrganizationID != member.OrganizationID)
            {
                return "OrganizationMismatch";
            }

            var existingMember = await Context.Memberships
                .Where(e => e.Team.TeamID == teamId && e.Member.MemberID == memberId)
                .FirstOrDefaultAsync();
            
            if (existingMember is not null)
            {
                return "AlreadyExists";
            }

            var membership = new Membership
            {
                MemberID = memberId,
                TeamID = teamId,
                MembershipDate = DateTime.Now
            };

            await Context.Memberships.AddAsync(membership);
            await Context.SaveChangesAsync();

            return "Added";
        }

        public async Task<NewTeam> GetTeam(int id)
        {
            return await Context.Teams
                .Include(e => e.Organization)
                .Where(e => e.TeamID == id)
                .Select(e => new NewTeam
                {
                    Name = e.TeamName,
                    Description = e.TeamDescription,
                    OrganizationName = e.Organization.OrganizationName,
                    
                    Members = e.Memberships.Select(e => new NewMember
                    {
                        Name = e.Member.MemberName,
                        Surname = e.Member.MemberSurname,
                        MembershipDate = e.MembershipDate
                    })
                    .OrderBy(e => e.MembershipDate)
                    .ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}
