using Cmms.Core.Application.Exceptions;
using Cmms.Core.Domain;
using Cmms.Infrastructure.Context;
using Cmms.Infrastructure.Dto;
using Cmms.Infrastructure.Utils;
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
        Task<PaginatedList<TeamGroupListItemDto>> GetAll(PaginationFilter pagination);
        Task<TeamGroupItemDto> GetById(Guid id);


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

        public async Task<PaginatedList<TeamGroupListItemDto>> GetAll(PaginationFilter pagination)
        {
            var query = _context.TeamGroups.AsNoTracking().AsQueryable();

            foreach (var f in pagination.filters)
            {
                switch (f.name)
                {
                    case "teamGroupName":
                        query = query.Where(d => d.TeamGroupName.Contains(f.value) == true);
                        break;
                }
            }
        var items=  await  PaginatedList<TeamGroupListItemDto>.CreateAsync(query.Select(x => TeamGroupListItemDto.ToListItem(x)), pagination.pageNumber, pagination.pageSize);
            return items;
        }

        public async Task<TeamGroupItemDto> GetById(Guid id)
        {
            var item =await _context.TeamGroups.AsNoTracking().Where(d => d.Id == id).Select(x=>TeamGroupItemDto.ToItem(x)).FirstOrDefaultAsync();
            if (item == null)
                throw new BadRequestException("your teamgroup not found");

           var userIds= item.members.Select(d => d.id).ToList();
        var users=   await _context.Users.Where(x => userIds.Contains(x.Id)).ToListAsync();
            item.members= (from u in users
             join m in item.members on u.Id equals m.id
             select new TeamGroupMemberItemDto
             {
                 fullName = u.FullName,
                 id = u.Id
             }).ToList();

            return item;
        }

    }


}
