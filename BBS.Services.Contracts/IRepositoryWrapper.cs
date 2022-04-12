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
    }
}
