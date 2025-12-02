using P7CreateRestApi.Domain;
using AutoMapper;
using P7CreateRestApi.DTOs.Rule;

namespace P7CreateRestApi.Mappings
{
    /// <summary>
    /// Profil AutoMapper pour gérer les conversions entre l'entité Rule et ses DTOs.
    /// </summary>
    public class RuleProfile : Profile
    {
        public RuleProfile()
        {
            CreateMap<Rule, RuleReadDto>();
            CreateMap<RuleCreateDto, Rule>();
            CreateMap<RuleUpdateDto, Rule>();
        }
    }
}
