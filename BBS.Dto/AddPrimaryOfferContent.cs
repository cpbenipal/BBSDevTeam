using System.Collections.Generic;

namespace BBS.Dto
{
    public class AddPrimaryOfferContent
    {
        public List<AddPrimaryOfferDto> Content { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
