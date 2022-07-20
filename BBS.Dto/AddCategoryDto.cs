namespace BBS.Dto
{
    public class AddCategoryDto
    {
        public string Name { get; set; }
        public string Tags { get; set; }
        public string DealTeaser { get; set; }
        public string CompanyProfile { get; set; }
        public string TermsAndLegal { get; set; }
        public string Documents { get; set; }
        public int OfferedShareMainTypeId { get; set; }
    }
}
