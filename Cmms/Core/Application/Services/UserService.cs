using Cmms.Core.Application.Exceptions;
using Cmms.Core.Application.Services;
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
    public interface IUserService : IRepository<User>
    {
        Task<InvitedUser> AddInvitedUser(CreateInviteUserDto invitedUser);
        Task<bool> AcceptInvitedUser(Guid inviteId,string Password);
        Task<InvitedUser> GetInviteUserById(Guid inviteId);
        Task<InvitedUser> ProcessGetInviteUser(Guid inviteId);
        Task<List<UserInfoModelDto>> UserList();

    }



    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly GeneralService _generalService;
        private readonly IGraphService _graphServices;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public UserService(ApplicationContext context,
            IGraphService graphService,
            GeneralService generalService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _generalService= generalService ?? throw new ArgumentNullException(nameof(generalService)); ;
            _graphServices = graphService ?? throw new ArgumentNullException(nameof(graphService)); ;


        }
        public async Task<InvitedUser> AddInvitedUser(CreateInviteUserDto user)
        {

           var isExistUser=await _graphServices.GetUserByEmail(user.Email);
            if (isExistUser!=null)
                throw new BadRequestException("User with this email already exist");

        var isExistInvited=   await GetInviteUserByEmail(user.Email);
            if (isExistInvited != null)
                throw new BadRequestException("Invite with this email already exist");



            var accessLevels =await _generalService.GetAccessLevels();
            Job job = await _generalService.GetJobById(user.JobTitle)?? throw new BadRequestException("parameter is wrong");
            Factory factory = await _generalService.GetFactoryById(user.FactoryName) ?? throw new BadRequestException("parameter is wrong");

            InvitedUser invitedUser = new InvitedUser { Id = Guid.NewGuid() };
            _context.Add(invitedUser);
            invitedUser.InvitedStatus = UserStatusType.Pending;
            invitedUser.FirstName = user.FirstName;
            invitedUser.SurName = user.SurName;
            invitedUser.Email = user.Email;
            invitedUser.MobileNumber = user.MobileNumber;
            invitedUser.JobTitle = job.JobTitle;
            invitedUser.LocationName = factory.FactoryName;
            invitedUser.Expire = DateTimeOffset.Now;

           await _context.SaveChangesAsync();

            return invitedUser;


        }
      public async  Task<InvitedUser> GetInviteUserById(Guid inviteId)
        {
          var current= await _context.InvitedUsers.FindAsync(inviteId);
            return current;

        }
        public  Task<InvitedUser> GetInviteUserByEmail(string email)
        {
            return _context.InvitedUsers.Where(d => d.Email == email).FirstOrDefaultAsync();
        }
       

        public async  Task<bool> AcceptInvitedUser(Guid inviteId,string Password)
        {
           var inviteUser=await GetInviteUserById(inviteId);
            if (inviteUser == null)
                throw new BadRequestException("Invite User Was Expired");

         var result=  await _graphServices.CreateUser(inviteUser, Password);
          _context.Remove(inviteUser);
            await _context.SaveChangesAsync();
            return true;

        }

       public async Task<InvitedUser> ProcessGetInviteUser(Guid inviteId)
        {
            var invite = await GetInviteUserById(inviteId);
            if (invite == null)
                return null;

            var userGraph = await _graphServices.GetUserByEmail(invite.Email);
            if (userGraph!=null)
            {
                _context.Remove(invite);
               await _context.SaveChangesAsync();
                return null;
            }
            else
            {
                return invite;
            }
            


        }

        public async Task<List<UserInfoModelDto>> UserList()
        {

          var result= await _graphServices.GetAllUsers();
            return result;


        }




    }
}
  