namespace P7CreateRestApi.DTOs.CurvePoint
{
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
