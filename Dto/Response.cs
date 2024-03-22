

namespace DotnetAssignment2.Dto
{
    public class Response
    {
        public int StatusCode { get; set; } 
        public string? Message { get; set; } = null;
        public object? Data { get; set; }
    }
}
