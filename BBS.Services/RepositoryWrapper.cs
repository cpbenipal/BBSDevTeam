using BBS.Entities;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private BusraDbContext _repoContext;
        public RepositoryWrapper(BusraDbContext repositoryContext)
        {
            _repoContext = repositoryContext;
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
                    _userLoginManager = new UserLoginManager(repositoryBase);
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

    }
}