using AutoMapper;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs.User;

namespace P7CreateRestApi.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserReadDto>();
            CreateMap<UserRegisterDto, ApplicationUser>();
            CreateMap<UserUpdateDto, ApplicationUser>();
        }
    }
}
