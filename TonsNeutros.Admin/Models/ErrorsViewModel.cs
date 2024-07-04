namespace TonsNeutros.Admin.Models
{
    public class ErrorsViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
