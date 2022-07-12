namespace BBS.Dto
{
    public class GetDigitalSharesItemDto
    {
        public int Id { get; set; }
        public int ShareId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string CompanyName { get; set; }
        public int NumberOfShares { get; set; }
        public bool IsCertified { get; set; }
        public string CertificateUrl { get; set; }
        public string CertificateKey { get; set; }
        public string AddedDate { get; set; }
        public int UserLoginId { get; set; }
    }
}
