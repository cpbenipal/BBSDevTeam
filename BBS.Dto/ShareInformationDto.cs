using System;
using System.ComponentModel;

namespace BBS.Dto
{
    public class ShareInformationDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int GrantTypeId { get; set; }
        public int EquityRoundId { get; set; }
        public int DebtRoundId { get; set; }
        public int NumberOfShares { get; set; }
        [DisplayName("DD/MM/YYYY")]
        public DateTime DateOfGrant { get; set; }
        public decimal SharePrice { get; set; }
        public int RestrictionId { get; set; }
        public int StorageLocationId { get; set; }
    }

}
