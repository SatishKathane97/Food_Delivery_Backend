using APP.Domain.Enums;
using Lib.Core.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Features.OnbordingUser.Command.OnboardindSendOTP
{
    public class OnboardingSendOTPCommand : IRequest<ResponseModel>
    {
        public string? Phone { get; set; }
        public CountryType CountryType { get; set; }   // ✅ ENUM DIRECT

    }
}
