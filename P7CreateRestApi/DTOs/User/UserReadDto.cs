namespace P7CreateRestApi.DTOs.User
{
    public class UserReadDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public IList<string> Roles { get; set; }
    }
}
