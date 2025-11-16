using P7CreateRestApi.Domain;
using AutoMapper;
using P7CreateRestApi.DTOs.BidList;

namespace P7CreateRestApi.Mappings
{
    /// <summary>
    /// Profil AutoMapper pour gérer les conversions entre l'entité BidList et ses DTOs.
    /// </summary>
    public class BidListProfile : Profile
    {
        public BidListProfile()
        {
            CreateMap<BidList, BidListReadDto>();
            CreateMap<BidListCreateDto, BidList>();
            CreateMap<BidListUpdateDto, BidList>();
        }
    }
}
