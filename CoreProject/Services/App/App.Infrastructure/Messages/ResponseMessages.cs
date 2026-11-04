namespace App.Infrastructure.Messages;
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
    public const string ExceptionOccured = "Exception: ";
    public const string InvalidParameters = "Invalid parameters";
    public const string NotFound = "No record found.";
    public const string RecordFound = "Record found.";

    // Internal API Access
    public const string InternalAccess_InvalidApiResponse = "Invalid response from";

    // Access Token validation
    public const string InvalidAccessToken = "Invalid Request.";
    public const string PermissionDenied = "Permission Denied.";

    // validation
    public const string ValidationFailed = "Validation failed!";
    public const string ValidationSuccess = "Validation successful!";
}
    

