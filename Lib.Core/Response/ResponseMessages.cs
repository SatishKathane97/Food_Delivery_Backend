namespace Lib.Core.Models.Response
{
    public class ResponseMessages
    {
        public static class Test
        {
            public static string PingMessage { get; } = "App apis for web are working properly";
            public static string AuthorizedPingMessage(string email) => $"Dear, {email}: App apis for mobile are working properly";
        }

        //Common
        public const string BadRequest = "Bad Request";
        public const string SomethingWrong = "Something went wrong. Please try again later.";
        public const string ExceptionOccurred = "Exception: ";
        public const string InvalidParameters = "Invalid parameters";
        public const string NotFound = "No record found.";
        public const string RecordFound = "Record found.";
        public const string EmailNotFound = "No such email registered.";
        public const string NameAlreadyExist = "Name Already Exists !!!";
        public const string AlreadyExist = "Already Exists !!!";
        public const string SystemGeneratedFailed = "System-generated entries is predefined and cannot be updated or deleted.";
        public const string UserUpdate = "User Updated";
        public const string AlreadyInUseUpdate = "Cannot change status already is in use !!!";
        public const string AlreadyInUseDelete = "Cannot delete already is in use !!!";

        // Internal API Access
        public const string InternalAccess_InvalidApiResponse = "Invalid response from";

        // Access Token validation
        public const string InvalidAccessToken = "Invalid Request.";
        public const string PermissionDenied = "Permission Denied.";

        // validation
        public const string ValidationFailed = "Validation failed!";
        public const string ValidationSuccess = "Validation successful!";


        // User Onboarding
        public const string Onboarding_Fail_UserExits = "Already company user exists.";

        // Signup
        public const string Signup_Success = "Signup successfully done!";

        // Signin
        public const string Signin_Failed_InvalidUsernamePassword = "Invalid Username OR Password.";
        public const string Signin_Failed_UserNotAllowed = "User is not allowed to use app.";
        public const string Signin_Success = "Login successful!";

        // OTP
        public const string OTP_InvalidToken = "Invalid OTP token.";

        // ForgotPassword
        public const string ForgotPassword_VerifyOTP_Success = "OTP verified successfully!";
        public const string ForgotPassword_ResetPassword_Success = "Password reset successfully!";

        public const string InsertSuccess = "inserted successfully.";
        public const string UpdateSuccess = "updated successfully.";
        public const string DeleteSuccess = "deleted successfully.";

        // Login
        public const string Login_Failed_InvalidUsernamePassword = "Invalid Username OR Password.";
        public const string Login_Failed_UserNotAllowed = "User is not allowed to use app.";
        public const string Login_Success = "Login successful!";

        // Role
        public const string RoleDeleted = "Role has been Deleted.";

        // Unit
        public const string UnitDeleted = "Unit has been Deleted.";

        // Unit Conversion
        public const string UnitConversionDeleted = "Unit Conversion has been Deleted.";

        // Exchange Rate
        public const string ExchangeRateDeleted = "Exchange Rate has been Deleted.";

        // Bucket
        public const string BucketDeleted = "Bucket has been Deleted.";

        // Color
        public const string ColorDeleted = "Color has been Deleted.";

        // CutShape
        public const string CutShapeDeleted = "CutShape has been Deleted.";

        // TODO check msg
        public const string ExceptionOccured = "";

        // TODO check msg
        public const string RDLCNotFound = "RDLC File Not Found";
    }

    public static class ResponseExtension
    {
        public static ResponseModel setSuccessResponse(this ResponseModel responseModel, object response, string Message = "")
        {
            responseModel.Response = response;
            responseModel.StatusCode = (int)HttpStatusCode.OK;
            responseModel.Message = (!string.IsNullOrEmpty(Message)) ? Message : "Operation Succeed!";
            return responseModel;

        }

        public static ResponseModel setFailureResponse(this ResponseModel responseModel, int httpStatusCode = 0, object? response = null)
        {
            responseModel.Response = response;
            responseModel.StatusCode = (httpStatusCode != 0) ? httpStatusCode : (int)HttpStatusCode.BadRequest;
            responseModel.Errors = responseModel.Errors;
            return responseModel;
        }
    }
}
