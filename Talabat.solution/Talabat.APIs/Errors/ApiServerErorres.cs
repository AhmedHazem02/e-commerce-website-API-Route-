namespace Talabat.APIs.Errors
{
    public class ApiServerErorres:ApiResponse
    {   
        public string? Details { get; set; }
        public ApiServerErorres(int code,string? Msg=null,string?Details=null):base(code, Msg)
        {
            this.Details = Details;
        }

     
    }
}
