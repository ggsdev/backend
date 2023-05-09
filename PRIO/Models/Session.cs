namespace Prio_BackEnd.Models
{
    public class Session
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User User { get; set; }
    }
}
