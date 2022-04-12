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
        }
    }
}
