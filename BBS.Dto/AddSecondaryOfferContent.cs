using System.Collections.Generic;

namespace BBS.Dto
{
    public class AddSecondaryOfferContent
    {
        public List<AddSecondaryOfferDto> Content { get; set; }
        public int OfferShareId { get; set; } 
    }
}
