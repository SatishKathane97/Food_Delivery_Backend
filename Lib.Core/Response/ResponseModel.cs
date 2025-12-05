using System.ComponentModel;

namespace Lib.Core.Models.Response
{

    public class ResponseModel
    {
        public ResponseModel()
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Message = null;
            Response = null;
            Errors = [];
        }

        public string? Message { get; set; }
        public object? Response { get; set; }
        public List<string> Errors { get; set; }

        public int StatusCode { get; set; }
        public long TotalRecords { get; set; }

        public bool IsSuccess => (StatusCode == (int)HttpStatusCode.OK) ? true : false;
    }

    public enum HttpStatusCode
    {
        //The request has succeeded]
        [Description("Success")]
        OK = 200,

        //The server has fulfilled the request but does not need to return an entity-body.
        [Description("No Content")]
        NoContent = 204,

        //The request could not be understood by the server due to malformed syntax.
        [Description("Bad Request")]
        BadRequest = 400,

        [Description("Failed")]
        Failed = 500,

        //The request requires user authentication.
        [Description("Unauthorized")]
        Unauthorized = 401,

        //The request could not be completed due to a conflict with the current state of the resource.
        [Description("Conflict")]
        Conflict = 409,

        [Description("Exception Occured")]
        ExceptionOccured = 10000

    }
}
