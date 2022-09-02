using System.Collections.Generic;

namespace BBS.Dto
{ 
    public class AddSecondaryOfferContent
    {
        public int OfferShareId { get; set; }
        public int Id { get; set; }       
        public string Title { get; set; }
        public string Content { get; set; }       
    }
}
