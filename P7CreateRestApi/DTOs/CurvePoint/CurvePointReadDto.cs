namespace P7CreateRestApi.DTOs.CurvePoint
{
    /// <summary>
    /// DTO utilisé pour renvoyer les données d'un curvepoint au client.
    /// Contient toutes les informations utiles pour l'affichage. (GET)
    /// </summary>
    public class CurvePointReadDto
    {
        public int Id { get; set; }
        public byte? CurveId { get; set; }
        public DateTime? AsOfDate { get; set; }
        public double? Term { get; set; }
        public double? CurvePointValue { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
