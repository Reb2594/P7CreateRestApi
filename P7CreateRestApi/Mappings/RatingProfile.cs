using AutoMapper;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs.Rating;


namespace P7CreateRestApi.Mappings
{
    /// <summary>
    /// Profil AutoMapper pour gérer les conversions entre l'entité Rating et ses DTOs.
    /// </summary>
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Rating, RatingReadDto>();
            CreateMap<RatingCreateDto, Rating>();
            CreateMap<RatingUpdateDto, Rating>();
        }
    }
}
