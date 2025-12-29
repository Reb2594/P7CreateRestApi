using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.CurvePoint
{
    /// <summary>
    /// DTO utilisé pour mettre à jour un curvepoint existant.
    /// Contient uniquement les champs modifiables par le client. (PUT)
    /// </summary>
    public class CurvePointUpdateDto
    {
        public byte? CurveId { get; set; }

        public DateTime? AsOfDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le terme doit être un nombre positif")]
        public double? Term { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La valeur du point de courbe doit être un nombre positif")]
        public double? CurvePointValue { get; set; }
    }
}
