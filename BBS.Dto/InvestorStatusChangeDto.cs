namespace BBS.Dto
{
    public class InvestorStatusChangeDto
    {
        public string PersonEmail { get; set; }
        public string Status { get; set; } = "Complete";
    }

    public class ShareStatusChangeDto
    {
        public int ShareId { get; set; }
        public string Status { get; set; } = "Complete";
    }
}
