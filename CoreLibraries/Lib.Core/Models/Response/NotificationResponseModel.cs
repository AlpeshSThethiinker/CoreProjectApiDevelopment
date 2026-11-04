namespace Lib.Core.Models.Response
{
    public sealed record NotificationResponseModel
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Subtitle { get; set; }
        public bool? IsNotified { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? TriggeredDateTime { get; set; }
    }
}
