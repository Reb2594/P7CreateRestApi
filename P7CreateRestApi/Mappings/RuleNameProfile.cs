using P7CreateRestApi.Domain;
using AutoMapper;
using P7CreateRestApi.DTOs.RuleName;

namespace P7CreateRestApi.Mappings
{
    /// <summary>
    /// Profil AutoMapper pour gérer les conversions entre l'entité RuleName et ses DTOs.
    /// </summary>
    public class RuleNameProfile : Profile
    {
        public RuleNameProfile()
        {
            CreateMap<RuleName, RuleNameReadDto>();
            CreateMap<RuleNameCreateDto, RuleName>();
            CreateMap<RuleNameUpdateDto, RuleName>();
        }
    }
}
