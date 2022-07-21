namespace BBS.Dto
{
    public class AddCategoryContent
    {
        public int CategoryId { get; set; }
        public string Content { get; set; }
        public decimal? OfferPrice { get; set; }
        public int? TotalShares { get; set; } 
    }
}
