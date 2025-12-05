using Lib.Core.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.UserOperation.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<ResponseModel>
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Phone { get; set; }
        public string? RoleId { get; set; }
    }
}
