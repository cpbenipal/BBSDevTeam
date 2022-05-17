using AutoMapper;
using BBS.Dto;
using BBS.Models;

namespace BBS.Swagger
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CountryDto, Country>();
            CreateMap<Country, CountryDto>();

            CreateMap<NationalityDto, Nationality>();
            CreateMap<Nationality, NationalityDto>();

            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();

            CreateMap<UserLogin, UserLoginDto>();
            CreateMap<UserLoginDto, UserLogin>();

            CreateMap<UserRole, UserRoleDto>();
            CreateMap<UserRoleDto, UserRole>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<Share, RegisteredShareDto>();
            CreateMap<RegisteredShareDto, Share>();

            CreateMap<IssueDigitalShareDto, IssuedDigitalShare>();
            CreateMap<IssuedDigitalShare, IssueDigitalShareDto>();

            CreateMap<GetDigitalSharesItemDto, IssuedDigitalShare>();
            CreateMap<IssuedDigitalShare, GetDigitalSharesItemDto>();

            CreateMap<OfferShareDto, OfferedShare>();
            CreateMap<OfferedShare, OfferShareDto>();

            CreateMap<GetOfferedSharesItemDto, OfferedShare>();
            CreateMap<OfferedShare, GetOfferedSharesItemDto>();
        }

    }
}
