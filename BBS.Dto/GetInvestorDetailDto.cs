namespace BBS.Dto
{
    public class GetInvestorDetailDto
    {
        public int InvestorsCount { get; set; }
        public int PendingAccountsCount { get; set; }
        public int ApprovedAccountsCount { get; set; }
        public int HighRiskInvestorsCount { get; set; }
        public int NormalInvestorsCount { get; set; }
        public int RetailInvestorsCount { get; set; }
        public int QualifiedInvestorsCount { get; set; }
    }
}
