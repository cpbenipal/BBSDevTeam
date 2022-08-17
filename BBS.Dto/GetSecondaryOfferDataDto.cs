namespace BBS.Dto
{
    public class GetSecondaryOfferDataDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int TotalShares { get; set; }

        public decimal OfferPrice { get; set; }

        public int OfferShareId { get; set; }
    }
}
