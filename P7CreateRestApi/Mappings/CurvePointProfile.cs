using P7CreateRestApi.Domain;
using AutoMapper;
using P7CreateRestApi.DTOs.CurvePoint;

namespace P7CreateRestApi.Mappings
{
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
