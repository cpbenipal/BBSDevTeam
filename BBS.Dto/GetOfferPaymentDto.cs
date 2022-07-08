namespace BBS.Dto
{
    public class GetOfferPaymentDto
    {
        public int Id { get; set; }
        public int OfferedShareId { get; set; }
        public string PaymentType { get; set; }
        public string TransactionId { get; set; }
        public string CompanyName { get; set; }
    }
}
