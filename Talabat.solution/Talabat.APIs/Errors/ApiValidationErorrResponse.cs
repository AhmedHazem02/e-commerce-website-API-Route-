namespace Talabat.APIs.Errors
{
    public class ApiValidationErorrResponse:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErorrResponse():base(400)
        {
            Errors=new List<string>();
        }
    }
}
