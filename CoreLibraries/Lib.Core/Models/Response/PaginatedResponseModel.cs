namespace Lib.Core.Models.Response
{
    public class PaginatedResponseModel
    {
        public PaginatedResponseModel()
        {
            IsSuccess = default;
            Message = default;
            Response = null;
            Errors = null;
            TotalRecords = default;
        }

        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Response { get; set; }
        public IList<string>? Errors { get; set; }

        public int Status => IsSuccess ? 200 : 400;

        public long TotalRecords { get; set; }
    }
}
