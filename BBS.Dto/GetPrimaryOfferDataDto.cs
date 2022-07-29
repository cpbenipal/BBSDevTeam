using System.Collections.Generic;

namespace BBS.Dto
{
    public class GetPrimaryOfferDataDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Company { get; set; }
    }

    public class GetPrimaryOffersDto 
    {
        public int Id { get; set; }        
        public string Company { get; set; }
        public List<CompanyInfo> CompanyInfo { get; set; }
        public List<InvestorDetails> InvestorDetails { get; set; }
        public int TotalInvestors { get; set; }
    }
    public class CompanyInfo
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public CatContent Content { get; set; } 
    }
    public class CatContent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class InvestorDetails
    {
        public string FirstName { get; set; }         
        public string LastName { get; set; }         
        public string Email { get; set; }
        public double PlacementAmount { get; set; }
        public bool IsESign { get; set; } = false;
        public bool IsDownload { get; set; } = false;
        public string VerificationStatus { get; set; } 
    }
    public class CompanyDto
    {
        public int CompanyId { get; set; } 
    }
    public class CompanyListDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
    public class CompanyDetailDto 
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<CatContent> Content { get; set; }
    }
} 