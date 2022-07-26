namespace BBS.Services.Contracts
{
    public interface IRepositoryWrapper
    {
        public IPersonManager PersonManager { get; }
        public ICountryManager CountryManager { get; }
        public INationalityManager NationalityManager { get; }
        public IUserRoleManager UserRoleManager { get; }
        public IUserLoginManager UserLoginManager { get; }
        public IRoleManager RoleManager { get; }
        public IPersonalAttachmentManager PersonalAttachmentManager { get; }
        public IDebtRoundManager DebtRoundManager { get; }
        public IEquityRoundManager EquityRoundManager { get; }
        public IGrantTypeManager GrantTypeManager { get; }
        public IRestrictionManager RestrictionManager { get; }
        public IStorageLocationManager StorageLocationManager { get; }
        public IShareManager ShareManager { get; }
        public IIssuedDigitalShareManager IssuedDigitalShareManager { get; }
        public IEmployementTypeManager EmployementTypeManager { get; }
        public ICompanyManager CompanyManager { get; }
        public IStateManager StateManager { get; }
        public IOfferTypeManager OfferTypeManager { get; }
        public IOfferedShareManager OfferedShareManager { get; }
        public IOfferTimeLimitManager OfferTimeLimitManager { get; }
        public IOfferPaymentManager OfferPaymentManager { get; }
        public IPaymentTypeManager PaymentTypeManager { get; }
        public IBidShareManager BidShareManager { get; }
        public IInvestorDetailManager InvestorDetailManager { get; }
        public IInvestorTypeManager InvestorTypeManager { get; }
        public IInvestorRiskTypeManager InvestorRiskTypeManager { get; }
        public ICategoryManager CategoryManager { get; }
        public IOfferedShareMainTypeManager OfferedShareMainTypeManager { get; }
        public ISecondaryOfferShareDataManager SecondaryOfferShareDataManager { get; }
    }
}
