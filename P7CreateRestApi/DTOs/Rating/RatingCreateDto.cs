using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.Rating
{
    public class RatingCreateDto
    {
        [StringLength(50, ErrorMessage = "La notation Moody's ne peut pas dépasser 50 caractères")]
        public string MoodysRating { get; set; }

        [StringLength(50, ErrorMessage = "La notation S&P ne peut pas dépasser 50 caractères")]
        public string SandPRating { get; set; }

        [StringLength(50, ErrorMessage = "La notation Fitch ne peut pas dépasser 50 caractères")]
        public string FitchRating { get; set; }

        [Range(0, 255, ErrorMessage = "Le numéro d'ordre doit être entre 0 et 255")]
        public byte? OrderNumber { get; set; }
    }
}