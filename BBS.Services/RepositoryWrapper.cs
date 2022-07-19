using BBS.Entities;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly BusraDbContext _repoContext;
        private readonly IHashManager _hashManager;
        public RepositoryWrapper(BusraDbContext repositoryContext, IHashManager hashManager)
        {
            _repoContext = repositoryContext;
            _hashManager = hashManager;
        }

        private ICountryManager _countryManager;
        public ICountryManager CountryManager
        {
            get
            {
                if (_countryManager == null)
                {
                    var repositoryBase = new RepositoryBase<Country>(_repoContext);
                    _countryManager = new CountryManager(repositoryBase);
                }
                return _countryManager;
            }
        }

        private INationalityManager _nationalityManager;
        public INationalityManager NationalityManager
        {
            get
            {
                if (_nationalityManager == null)
                {
                    var repositoryBase = new RepositoryBase<Nationality>(_repoContext);
                    _nationalityManager = new NationalityManager(repositoryBase);
                }
                return _nationalityManager;
            }
        }

        private IPersonManager _personManager;
        public IPersonManager PersonManager
        {
            get
            {
                if (_personManager == null)
                {
                    var repositoryBase = new RepositoryBase<Person>(_repoContext);
                    _personManager = new PersonManager(repositoryBase);
                }
                return _personManager;
            }
        }

        private IRoleManager _roleManager;
        public IRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null)
                {
                    var repositoryBase = new RepositoryBase<Role>(_repoContext);
                    _roleManager = new RoleManager(repositoryBase);
                }
                return _roleManager;
            }
        }

        private IUserRoleManager _userRoleManager;
        public IUserRoleManager UserRoleManager
        {
            get
            {
                if (_userRoleManager == null)
                {
                    var repositoryBase = new RepositoryBase<UserRole>(_repoContext);
                    _userRoleManager = new UserRoleManager(repositoryBase);
                }
                return _userRoleManager;
            }
        }

        private IUserLoginManager _userLoginManager;
        public IUserLoginManager UserLoginManager
        {
            get
            {
                if (_userLoginManager == null)
                {
                    var repositoryBase = new RepositoryBase<UserLogin>(_repoContext);
                    _userLoginManager = new UserLoginManager(repositoryBase, _hashManager);
                }
                return _userLoginManager;
            }
        }

        private IPersonalAttachmentManager _personalAttachmentManager;
        public IPersonalAttachmentManager PersonalAttachmentManager
        {
            get
            {
                if (_personalAttachmentManager == null)
                {
                    var repositoryBase = new RepositoryBase<PersonalAttachment>(_repoContext);
                    _personalAttachmentManager = new PersonalAttachmentManager(repositoryBase);
                }
                return _personalAttachmentManager;
            }
        }

        private IDebtRoundManager _debtRoundManager;
        public IDebtRoundManager DebtRoundManager
        {
            get
            {
                if (_debtRoundManager == null)
                {
                    var repositoryBase = new RepositoryBase<DebtRound>(_repoContext);
                    _debtRoundManager = new DebtRoundManager(repositoryBase);
                }
                return _debtRoundManager;
            }
        }

        private IEquityRoundManager _equityRoundManager;
        public IEquityRoundManager EquityRoundManager
        {
            get
            {
                if (_equityRoundManager == null)
                {
                    var repositoryBase = new RepositoryBase<EquityRound>(_repoContext);
                    _equityRoundManager = new EquityRoundManager(repositoryBase);
                }
                return _equityRoundManager;
            }
        }

        private IGrantTypeManager _grantTypeManager;
        public IGrantTypeManager GrantTypeManager
        {
            get
            {
                if (_grantTypeManager == null)
                {
                    var repositoryBase = new RepositoryBase<GrantType>(_repoContext);
                    _grantTypeManager = new GrantTypeManager(repositoryBase);
                }
                return _grantTypeManager;
            }
        }

        private IRestrictionManager _restrictionManager;
        public IRestrictionManager RestrictionManager
        {
            get
            {
                if (_restrictionManager == null)
                {
                    var repositoryBase = new RepositoryBase<Restriction>(_repoContext);
                    _restrictionManager = new RestrictionManager(repositoryBase);
                }
                return _restrictionManager;
            }
        }


        private IStorageLocationManager _storageLocationManager;
        public IStorageLocationManager StorageLocationManager
        {
            get
            {
                if (_storageLocationManager == null)
                {
                    var repositoryBase = new RepositoryBase<StorageLocation>(_repoContext);
                    _storageLocationManager = new StorageLocationManager(repositoryBase);
                }
                return _storageLocationManager;
            }
        }

        private IShareManager _share;
        public IShareManager ShareManager
        {
            get
            {
                if (_share == null)
                {
                    var repositoryBase = new RepositoryBase<Share>(_repoContext);
                    _share = new ShareManager(repositoryBase);
                }
                return _share;
            }
        }

        private IIssuedDigitalShareManager _issuedShare;
        public IIssuedDigitalShareManager IssuedDigitalShareManager
        {
            get
            {
                if (_issuedShare == null)
                {
                    var repositoryBase = new RepositoryBase<IssuedDigitalShare>(_repoContext);
                    _issuedShare = new IssuedDigitalShareManager(repositoryBase);
                }
                return _issuedShare;
            }
        }

        private IEmployementTypeManager _employementType;
        public IEmployementTypeManager EmployementTypeManager
        {
            get
            {
                if (_employementType == null)
                {
                    var repositoryBase = new RepositoryBase<EmployementType>(_repoContext);
                    _employementType = new EmployementTypeManager(repositoryBase);
                }
                return _employementType;
            }
        }

        private ICompanyManager _company;
        public ICompanyManager CompanyManager
        {
            get
            {
                if (_company == null)
                {
                    var repositoryBase = new RepositoryBase<Company>(_repoContext);
                    _company = new CompanyManager(repositoryBase);
                }
                return _company;
            }
        }

        private IStateManager _state;
        public IStateManager StateManager
        {
            get
            {
                if (_state == null)
                {
                    var repositoryBase = new RepositoryBase<State>(_repoContext);
                    _state = new StateManager(repositoryBase);
                }
                return _state;
            }
        }

        private IOfferTypeManager _offerType;
        public IOfferTypeManager OfferTypeManager
        {
            get
            {
                if (_offerType == null)
                {
                    var repositoryBase = new RepositoryBase<OfferType>(_repoContext);
                    _offerType = new OfferTypeManager(repositoryBase);
                }
                return _offerType;
            }
        }

        private IOfferedShareManager _offeredShare;
        public IOfferedShareManager OfferedShareManager
        {
            get
            {
                if (_offeredShare == null)
                {
                    var repositoryBase = new RepositoryBase<OfferedShare>(_repoContext);
                    _offeredShare = new OfferedShareManager(repositoryBase);
                }
                return _offeredShare;
            }
        }

        private IOfferTimeLimitManager _offerTimeLimit;
        public IOfferTimeLimitManager OfferTimeLimitManager
        {
            get
            {
                if (_offerTimeLimit == null)
                {
                    var repositoryBase = new RepositoryBase<OfferTimeLimit>(_repoContext);
                    _offerTimeLimit = new OfferTimeLimitManager(repositoryBase);
                }
                return _offerTimeLimit;
            }
        }

        private IOfferPaymentManager _offerPayment;
        public IOfferPaymentManager OfferPaymentManager
        {
            get
            {
                if (_offerPayment == null)
                {
                    var repositoryBase = new RepositoryBase<OfferPayment>(_repoContext);
                    _offerPayment = new OfferPaymentManager(repositoryBase);
                }
                return _offerPayment;
            }
        }

        private IPaymentTypeManager _paymentType;
        public IPaymentTypeManager PaymentTypeManager
        {
            get
            {
                if (_paymentType == null)
                {
                    var repositoryBase = new RepositoryBase<PaymentType>(_repoContext);
                    _paymentType = new PaymentTypeManager(repositoryBase);
                }
                return _paymentType;
            }
        }

        private IBidShareManager _bidShare;
        public IBidShareManager BidShareManager
        {
            get
            {
                if (_bidShare == null)
                {
                    var repositoryBase = new RepositoryBase<BidShare>(_repoContext);
                    _bidShare = new BidShareManager(repositoryBase);
                }
                return _bidShare;
            }
        }

        private IInvestorDetailManager _investorDetail;
        public IInvestorDetailManager InvestorDetailManager
        {
            get
            {
                if (_investorDetail == null)
                {
                    var repositoryBase = new RepositoryBase<InvestorDetail>(_repoContext);
                    _investorDetail = new InvestorDetailManager(repositoryBase);
                }
                return _investorDetail;
            }
        }

        private IInvestorRiskTypeManager _investorRiskType;
        public IInvestorRiskTypeManager InvestorRiskTypeManager
        {
            get
            {
                if (_investorRiskType == null)
                {
                    var repositoryBase = new RepositoryBase<InvestorRiskType>(_repoContext);
                    _investorRiskType = new InvestorRiskTypeManager(repositoryBase);
                }
                return _investorRiskType;
            }
        }

        private IInvestorTypeManager _investorType;
        public IInvestorTypeManager InvestorTypeManager
        {
            get
            {
                if (_investorType == null)
                {
                    var repositoryBase = new RepositoryBase<InvestorType>(_repoContext);
                    _investorType = new InvestorTypeManager(repositoryBase);
                }
                return _investorType;
            }
        }

        private ICategoryManager _category;
        public ICategoryManager CategoryManager
        {
            get
            {
                if (_category == null)
                {
                    var repositoryBase = new RepositoryBase<Category>(_repoContext);
                    _category = new CategoryManager(repositoryBase);
                }
                return _category;
            }
        }

        private IOfferedShareMainTypeManager _offeredShareMainType;
        public IOfferedShareMainTypeManager OfferedShareMainTypeManager
        {
            get
            {
                if (_offeredShareMainType == null)
                {
                    var repositoryBase = new RepositoryBase<OfferedShareMainType>(_repoContext);
                    _offeredShareMainType = new OfferedShareMainTypeManager(repositoryBase);
                }
                return _offeredShareMainType;
            }
        }
    }
}