using Cmms.Core.Application.Exceptions;
using Cmms.Core.Application.Models;
using Cmms.Core.Domain;
using Cmms.Infrastructure.Middleware;
using FluentValidation;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cmms.Core.Application.Commands
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public RegisterUserCommand()
        {
           
        }
        public string fullName { set; get; }
        public string email { set; get; }
        public string password { set; get; }
        public string company { set; get; }
    }


    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(c => c.email).NotEmpty().WithMessage("Email address is required")
                 .EmailAddress().WithMessage("A valid email is required");
            RuleFor(c => c.fullName).NotEmpty().WithMessage("FullName is Required");
            //  RuleFor(c => c.nationalCode).Matches(@"^\d{10}$").WithMessage("کد ملی را درست وارد کنید");
        }
    }


    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserService _userService;
        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {


            var currentUser = await _userService.GetUserByEmail(request.email);
            if (currentUser != null)
                throw new BadRequestException("The Email already exists");

            var user = new User
            {
                Email = request.email,
                Company = request.company,
                Password = request.password,
                FullName = request.fullName,
                CreationTime = DateTime.Now,
                Id = Guid.NewGuid(),
                IsActive = true,
                LastAccessDateTime = DateTime.Now,
                UserIdentity = Guid.NewGuid(),
            };

            _userService.Add(user);
          await  _userService.UnitOfWork.SaveChangesAsync();
            return user.UserIdentity;


        }
    }


    }
