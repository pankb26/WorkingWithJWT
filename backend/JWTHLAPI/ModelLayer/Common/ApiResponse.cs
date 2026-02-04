namespace JWTHLAPI.ModelLayer.Common
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public ApiResponse() { }
        public ApiResponse(bool isSuccess, string message, object result = null)
        {
            Success = isSuccess;
            Message = message;
            Result = result;
        }
    }
}