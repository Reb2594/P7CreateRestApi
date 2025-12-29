using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.Rule
{
    /// <summary>
    /// DTO utilisé pour mettre à jour une rule existante.
    /// Contient uniquement les champs modifiables par le client. (PUT)
    /// </summary>
    public class RuleUpdateDto
    {
        [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
        public string Description { get; set; }

        public string Json { get; set; }
        public string Template { get; set; }
        public string SqlStr { get; set; }
        public string SqlPart { get; set; }
    }
}
