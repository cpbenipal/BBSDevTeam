﻿using System.Collections.Generic;

namespace BBS.Dto
{
    public class RegisteredShareDto
    {
        public int Id { get; set; }
        public string BusinessLogo { get; set; }
        public string ShareOwnerShipDocument { get; set; }
        public string CompanyInformationDocument { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string GrantType { get; set; }
        public string EquityRound { get; set; }
        public string DebtRound { get; set; }
        public int NumberOfShares { get; set; }
        public string DateOfGrant { get; set; }
        public decimal SharePrice { get; set; }
        public string? LastValuation { get; set; }
        public string? GrantValuation { get; set; }
        public List<RestrictionDto> Restriction { get; set; }
        public string StorageLocation { get; set; }
        public string VerficationStatus { get; set; }
        public string AddedDate { get; set; }
        public int UserLoginId { get; set; }
    }
}
