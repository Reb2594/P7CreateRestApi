using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.CurvePoint
{
    public class CurvePointCreateDto
    {
        [Required(ErrorMessage = "L'ID de la courbe est obligatoire")]
        public byte? CurveId { get; set; }

        public DateTime? AsOfDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le terme doit être un nombre positif")]
        public double? Term { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La valeur du point de courbe doit être un nombre positif")]
        public double? CurvePointValue { get; set; }
    }
}
