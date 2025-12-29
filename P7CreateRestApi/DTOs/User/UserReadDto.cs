namespace P7CreateRestApi.DTOs.User
{
    /// <summary>
    /// DTO utilisé pour renvoyer les données d'un utilisateur au client.
    /// Contient toutes les informations utiles pour l'affichage. (GET)
    /// </summary>
    public class UserReadDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public IList<string> Roles { get; set; }
    }
}
