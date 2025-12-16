using App.Infrastructure.Service.OTPLogServiceImp;
using Lib.Core.Helper;
using Lib.Core.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Features.OnbordingUser.Command.OnboardingResendOtp
{
    public class OnboardingResendOtpCommandHandler : IRequestHandler<OnboardingResendOtpCommand, ResponseModel>
    {
        private readonly IOTPService _otpService;

        public OnboardingResendOtpCommandHandler(IOTPService otpService)
        {
            _otpService = otpService ?? throw new ArgumentNullException(nameof(otpService));
        }

        public async Task<ResponseModel> Handle(OnboardingResendOtpCommand request, CancellationToken cancellationToken)
        {
            ResponseModel response = new();

            try
            {
                if (string.IsNullOrWhiteSpace(request.Phone))
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Phone number is required";
                    return response;
                }

                // ✅ Get country code from ENUM
                var description = EnumHelper.GetDescription(request.CountryType);
                // Example: "India +91"

                var parts = description.Split('+');
                var countryCode = "+" + parts[1].Trim();

                // 🔐 Generate new OTP
                string otp = OtpHelper.GenerateNumericOtp(6);

                // 🔁 Reuse SaveOtpAsync (same as resend logic)
                var otpLog = await _otpService.SaveOtpAsync(
                    request.Phone,
                    countryCode,
                    otp
                );

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "OTP resent successfully";
                response.Response = new
                {
                    Phone = request.Phone,
                    CountryCode = countryCode,
                    ExpireAt = otpLog.ExpiresAt
                };

                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.ExceptionOccured;
                response.Message = ex.Message;
                response.Errors.Add(ex.Message);
                return response;
            }
        }
    }
}
