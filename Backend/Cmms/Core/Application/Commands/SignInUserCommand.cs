using Cmms.Core.Application.Exceptions;
using Cmms.Core.Application.Models;
using Cmms.Core.Domain;
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
    public class SignInUserCommand : IRequest<Guid>
    {
        public SignInUserCommand()
        {
           
        }
        public string email { set; get; }
        public string password { set; get; }
    }


    public class SignInUserCommandValidator : AbstractValidator<SignInUserCommand>
    {
        public SignInUserCommandValidator(IUserService _userService)
        {
            RuleFor(c => c.email).NotEmpty().WithMessage("Email address is required")
                 .EmailAddress().WithMessage("A valid email is required");

            RuleFor(c => c.password).NotEmpty().WithMessage("Password is Required");
        }
    }


    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, Guid>
    {
        private readonly IUserService _userService;
        public SignInUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<Guid> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {

            var user=  await _userService.GetUserByEmail(request.email);
            if (user == null)
                throw new BadRequestException("Email Address is Not Exist");

            return user.UserIdentity;


        }
    }


    }
