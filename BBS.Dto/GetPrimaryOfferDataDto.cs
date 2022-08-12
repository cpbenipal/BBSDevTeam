﻿using System;
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

    public class GetAllPrimaryOffersDto 
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public decimal OfferPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalTargetAmount { get; set; }
        public string ClosingDate { get; set; }
        public int TotalBids { get; set; } 
    }


    public class GetPrimaryOffersDto 
    {
        public int Id { get; set; }        
        public string Company { get; set; }     
        public decimal OfferPrice { get; set; }     
        public int Quantity { get; set; }
        public string InvestmentManager { get; set; }
        public decimal TotalTargetAmount { get; set; }
        public decimal MinimumInvestment { get; set; }
        public string ClosingDate { get; set; }
        public string Tags { get; set; }
        public string ShortDescription { get; set; }        
        public List<CatContent> WebView { get; set; }
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
    public class CategoryDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
    }
    public class InvestorDetails
    {
        public int OfferShareId { get; set; } 
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
    public class InvestorDto
    {
        public int UserLoginId { get; set; }
        public int VerificationStatus { get; set; }  
    }
    public class CompanyListDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public decimal OfferPrice { get; set; }
        public int Quantity { get; set; }
        public string InvestmentManager { get; set; }
        public decimal TotalTargetAmount { get; set; }
        public decimal MinimumInvestment { get; set; }
        public string ClosingDate { get; set; }
        public string Tags { get; set; }
        public string ShortDescription { get; set; }
        public decimal RaisedAmount { get; set; }
        public string DaysLeft { get; set; }
        public string FeePercentage { get; set; } 
        public string Allocation { get; set; } = "1.4%";
        public int TotalInvestors { get; set; } 
        public List<InvestorDto> InvestorDto { get; set; }       
        public List<CatContent> WebView { get; set; } 
    }
    public class CompanyDetailDto 
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<CatContent> Content { get; set; }
    }
} 