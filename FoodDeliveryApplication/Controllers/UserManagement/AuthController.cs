using App.Application.UserOperation.Commands.RegisterUser;
using Lib.Core.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers.UserManagement
{
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("registerUser")]
        public async Task<ActionResult<ResponseModel>> Insert([FromBody] RegisterUserCommand model)
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
