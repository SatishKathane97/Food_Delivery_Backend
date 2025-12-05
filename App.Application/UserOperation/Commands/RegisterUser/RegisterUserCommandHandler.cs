using App.Application.Authorization;
using App.Infrastructure.Service.UserServiceImp;
using APP.Domain.Entities.UserDto;
using APP.Domain.Enums;
using Lib.Core.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace App.Application.UserOperation.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResponseModel>
    {
        private readonly IUserService _userService;
        private readonly ClaimsBaseService _claimsBaseService;
        public RegisterUserCommandHandler(IUserService userService, ClaimsBaseService claimsBaseService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _claimsBaseService = claimsBaseService ?? throw new ArgumentNullException(nameof(claimsBaseService));
        }

        public async Task<ResponseModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            ResponseModel response = new();
            try
            {
                


                if (request ==null)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Invalid request";
                    return response;
                }
                var existingUser = await _userService.GetAsync(a => a.Email == request.Email);

                if (existingUser != null)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Email already exists!";
                    return response;
                }

                // Optional additional duplicate check
                var existingPhone = await _userService.GetAsync(a => a.Phone == request.Phone);
                if (existingPhone != null)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Phone number already exists!";
                    return response;
                }

                var user = new User
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    PasswordHash = request.PasswordHash,
                    Phone = request.Phone,
                    Role = request.RoleId,
                };
                var insertUser = await _userService.Insert(user);

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = ResponseMessages.InsertSuccess;
                response.Response = insertUser;
                return response;

            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.ExceptionOccured;
                response.Message =   ResponseMessages.ExceptionOccured+ex.Message;
            }
            return response;
        }
    }
}
