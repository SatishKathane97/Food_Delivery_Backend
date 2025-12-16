using App.Infrastructure.Service.OTPLogServiceImp;
using APP.Domain.Enums;
using Lib.Core.Helper;
using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.OnbordingUser.Command.OnboardindSendOTP
{
    public class OnboardindSendOTPCommandHandler
        : IRequestHandler<OnboardingSendOTPCommand, ResponseModel>
    {
        private readonly IOTPService _otpService;

        public OnboardindSendOTPCommandHandler(IOTPService otpService)
        {
            _otpService = otpService
                ?? throw new ArgumentNullException(nameof(otpService));
        }

        public async Task<ResponseModel> Handle(
            OnboardingSendOTPCommand request,
            CancellationToken cancellationToken)
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

                // ✅ Get country info from ENUM
                var description = EnumHelper.GetDescription(request.CountryType);
                // "India +91"

                var parts = description.Split('+');
                var countryCode = "+" + parts[1].Trim();

                string otp = OtpHelper.GenerateNumericOtp(6);

                var otpLog = await _otpService.SaveOtpAsync(
                    request.Phone,
                    countryCode,
                    otp
                );

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "OTP sent successfully";
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
