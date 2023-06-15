namespace PRIO.Models.UserControlAccessModels
{
    public class Session
    {
        public Guid? Id { get; set; }
        public string? Token { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime ExpiresIn { get; set; } = DateTime.UtcNow.AddDays(5);
        public User? User { get; set; }
        public string? UserHttpAgent { get; set; } = string.Empty;
    }
}
