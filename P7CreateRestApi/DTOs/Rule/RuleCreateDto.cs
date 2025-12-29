using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.Rule
{
    /// <summary>
    /// DTO utilisé pour créer une nouvelle rule.
    /// Contient uniquement les champs à renseigner par le client. (POST)
    /// </summary>
    public class RuleCreateDto
    {
        [Required(ErrorMessage = "Le nom de la règle est obligatoire")]
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
