using App.Infrastructure.Service.OTPLogServiceImp;
using Lib.Core.Models.Response;
using MediatR;
using System.Net;
using HttpStatusCode = Lib.Core.Models.Response.HttpStatusCode;

namespace App.Application.Features.OnbordingUser.Command.OnboardingVerifyOTP
{
    public class OnboardingVerifyCommandHandler
        : IRequestHandler<OnboardingVerifyCommand, ResponseModel>
    {
        private readonly IOTPService _otpService;

        public OnboardingVerifyCommandHandler(IOTPService otpService)
        {
            _otpService = otpService
                ?? throw new ArgumentNullException(nameof(otpService));
        }

        public async Task<ResponseModel> Handle(OnboardingVerifyCommand request, CancellationToken cancellationToken)
        {
            ResponseModel response = new();
            if (string.IsNullOrWhiteSpace(request.Phone) )
            {
               
                response.Message = "Phone and OTP are required";
                return response;
            }

            // ✅ AUTO VERIFY OTP
            bool isVerified = await _otpService.VerifyOtpAsync(
                request.Phone
               
            );

            if (!isVerified)
            {
                response.StatusCode = (int)HttpStatusCode.Failed;
                response.Message = "Invalid or expired OTP";
                return response;
            }

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Message = "OTP verified successfully";
            response.Response = new
            {
                Phone = request.Phone,
                Verified = true
            };

            return response;
        }
    }
}
