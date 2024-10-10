namespace OnlineStore.APIs.Error
{
    public class ExceptionResponse : ApiErrorResponse
    {
        public string? Details { get; set; }

        public ExceptionResponse(int code,string? Message = null ,string? deatails = null) : base(500)
        {
            Details = deatails;
        }

    }
}
