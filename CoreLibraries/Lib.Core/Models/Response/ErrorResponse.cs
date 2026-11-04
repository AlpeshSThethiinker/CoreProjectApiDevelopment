namespace Lib.Core.Models.Response
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Errors = new List<string>();
        }
        public bool HasErrors => Errors.Count > 0;
        public string? Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
