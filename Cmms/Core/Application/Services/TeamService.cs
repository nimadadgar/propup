using Cmms.Core.Application.Exceptions;
using Cmms.Core.Domain;
using Cmms.Infrastructure.Context;
using Cmms.Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Application
{
    public interface ITeamService : IRepository<TeamGroup>
    {
        Task<TeamGroup> AddTeam(AddTeamGroupDto viewModel);
        Task<TeamGroup> UpdateTeam(UpdateTeamGroupDto viewModel);

    }
    public class TeamService : ITeamService
    {
        private readonly ApplicationContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public TeamService(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<TeamGroup> AddTeam(AddTeamGroupDto viewModel)
        {
           
               var current = new TeamGroup { Id = Guid.NewGuid() };
                _context.TeamGroups.Add(current);
           
            current.TeamGroupName = viewModel.teamName;
            current.Description = viewModel.description;

            current.ClearMembers();
            current.AddMembers(viewModel.users);
            _context.TeamGroups.Add(current);
           await _context.SaveChangesAsync();
            return current;
        }

        public async Task<TeamGroup> UpdateTeam(UpdateTeamGroupDto viewModel)
        {
           
              var  current = await _context.TeamGroups.FindAsync(viewModel.id);
           
            if (current == null)
                throw new BadRequestException("team group parameters is wrong");

            current.TeamGroupName = viewModel.teamName;
            current.Description = viewModel.description;

            current.ClearMembers();
            current.AddMembers(viewModel.users);
            await _context.SaveChangesAsync();
            return current;
        }

    }


}
