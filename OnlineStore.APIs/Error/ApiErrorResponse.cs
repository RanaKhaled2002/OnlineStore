namespace OnlineStore.APIs.Error
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }

        public ApiErrorResponse(int statusCode, string? errorMessage = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage ?? GetMessageForStatusCode(statusCode);
        }

        private string? GetMessageForStatusCode(int code)
        {
            var message = code switch
            {
                400 => "Bad Request",
                401 => "You r Not Authorized",
                404 => "Resource Not Found",
                500 => "Server Error",
                _ =>null
            };

            return message;
        }
    }
}
