using App.Application.Authorization;
using App.Infrastructure.Service.UserServiceImp;
using APP.Domain.Enums;
using APP.Domain.FactoryService;
using Lib.Core.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.UserOperation.Commands.LoginUser
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseModel>
    {
        
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;
        public LoginCommandHandler(
            IJwtService jwtService, IUserService userService)
        {
           
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _userService = userService ?? throw new ArgumentNullException(nameof(_userService));
        }

        public async Task<ResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            ResponseModel response = new();

            try
            {
                if (string.IsNullOrWhiteSpace(request.Email) ||
                    string.IsNullOrWhiteSpace(request.Password))
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Please enter email and password!";
                    return response;
                }

                var user = await _userService.GetAsync(x => x.Email == request.Email);
                if (user == null)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Invalid email or password!";
                    return response;
                }

                // 🚫 Plain text password check
                if (request.Password != user.PasswordHash) // or user.Password depending on your column name
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Invalid email or password!";
                    return response;
                }


                // 🔹 Create claim model for JWT
                var claimsModel = new ClaimsModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Role = user.Role   // 👈 role set as string
                };

                var token = _jwtService.GenerateToken(claimsModel);

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Login successful!";
                response.Response = token;
                return response;

            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.ExceptionOccured;
                response.Message = ResponseMessages.ExceptionOccured + ex.Message;
                return response;
            }
        }
    }
}
