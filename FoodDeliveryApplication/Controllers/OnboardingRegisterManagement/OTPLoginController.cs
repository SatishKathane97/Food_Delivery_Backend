using App.Application.Features.OnbordingUser.Command.OnboardindSendOTP;
using App.Application.Features.OnbordingUser.Command.OnboardingResendOtp;
using App.Application.Features.OnbordingUser.Command.OnboardingVerifyOTP;
using App.Application.Features.UserOperation.Commands.RegisterUser;
using Lib.Core.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers.OnboardingRegisterManagement
{
    public class OTPLoginController : BaseController
    {
        public OTPLoginController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("sendOtp")]
        public async Task<ActionResult<ResponseModel>> Send([FromBody] OnboardingSendOTPCommand model)
        {
            ResponseModel response = new();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = ResponseMessages.InvalidParameters;
                    return response;
                }
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.ExceptionOccured;
                response.Message = ResponseMessages.ExceptionOccured + ex.Message;
                return BadRequest(response);
            }
        }


        [HttpPost]
        [Route("rsendOtp")]
        public async Task<ActionResult<ResponseModel>> Resend([FromBody] OnboardingResendOtpCommand model)
        {
            ResponseModel response = new();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = ResponseMessages.InvalidParameters;
                    return response;
                }
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.ExceptionOccured;
                response.Message = ResponseMessages.ExceptionOccured + ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("verifyOtp")]
        public async Task<ActionResult<ResponseModel>> Verify([FromBody]OnboardingVerifyCommand model)
        {
            ResponseModel response = new();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = ResponseMessages.InvalidParameters;
                    return response;
                }
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.ExceptionOccured;
                response.Message = ResponseMessages.ExceptionOccured + ex.Message;
                return BadRequest(response);
            }
        }
    }
}
