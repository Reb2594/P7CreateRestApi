using P7CreateRestApi.Domain;
using AutoMapper;
using P7CreateRestApi.DTOs.CurvePoint;

namespace P7CreateRestApi.Mappings
{
    /// <summary>
    /// Profil AutoMapper pour gérer les conversions entre l'entité CurvePoint et ses DTOs.
    /// </summary>
    public class CurvePointProfile : Profile
    {
        public CurvePointProfile()
        {
            CreateMap<CurvePoint, CurvePointReadDto>();
            CreateMap<CurvePointCreateDto, CurvePoint>();
            CreateMap<CurvePointUpdateDto, CurvePoint>();
        }
    }
}
