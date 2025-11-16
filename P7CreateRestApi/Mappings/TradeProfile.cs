using AutoMapper;
using P7CreateRestApi.DTOs.Trade;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Mappings
{
    /// <summary>
    /// Profil AutoMapper pour gérer les conversions entre l'entité Trade et ses DTOs.
    /// </summary>
    public class TradeProfile : Profile
    {
        public TradeProfile()
        {
            CreateMap<Trade, TradeReadDto>();
            CreateMap<TradeCreateDto, Trade>();
            CreateMap<TradeUpdateDto, Trade>();
        }
    }
}
