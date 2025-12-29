namespace P7CreateRestApi.DTOs.Rating
{
    /// <summary>
    /// DTO utilisé pour renvoyer les données d'un rating au client.
    /// Contient toutes les informations utiles pour l'affichage. (GET)
    /// </summary>
    public class RatingReadDto
    {
        public int Id { get; set; }
        public string MoodysRating { get; set; }
        public string SandPRating { get; set; }
        public string FitchRating { get; set; }
        public byte? OrderNumber { get; set; }
    }
}