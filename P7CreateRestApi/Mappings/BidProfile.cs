using P7CreateRestApi.Domain;
using AutoMapper;
using P7CreateRestApi.DTOs.Bid;

namespace P7CreateRestApi.Mappings
{
    /// <summary>
    /// Profil AutoMapper pour gérer les conversions entre l'entité Bid et ses DTOs.
    /// </summary>
    public class BidProfile : Profile
    {
        public BidProfile()
        {
            CreateMap<Bid, BidReadDto>();
            CreateMap<BidCreateDto, Bid>();
            CreateMap<BidUpdateDto, Bid>();
        }
    }
}
