namespace StorageManagement.Models
{
    public class Password
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Url { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}