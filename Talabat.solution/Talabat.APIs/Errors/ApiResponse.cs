
namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int ErrorCode { get; set; }  
        public string ?ErrorMessage { get; set; }
        public ApiResponse(int errorcode,string ?msg=null)
        {
            ErrorCode = errorcode;
            ErrorMessage = msg??DeafultMsg(ErrorCode);
        }

        private string? DeafultMsg(int errorCode)
        {
            return errorCode switch
            {
                404 => "NotFound",
                401 => "You Are Not Authorization",
                400 => "Bad Request",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
