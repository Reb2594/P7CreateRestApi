namespace P7CreateRestApi.DTOs.Rule
{
    /// <summary>
    /// DTO utilisé pour renvoyer les données d'une rule au client.
    /// Contient toutes les informations utiles pour l'affichage. (GET)
    /// </summary>
    public class RuleReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Json { get; set; }
        public string Template { get; set; }
        public string SqlStr { get; set; }
        public string SqlPart { get; set; }
    }
}
