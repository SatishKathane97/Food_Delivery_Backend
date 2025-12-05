using Lib.Core.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.UserOperation.Commands.LoginUser
{
    public class LoginCommand : IRequest<ResponseModel>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
