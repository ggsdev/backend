namespace PRIO.Models
{
    public class Session
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; } = true; //sessão ativa
        public DateTime ExpiresIn { get; set; } = DateTime.UtcNow.AddDays(5).ToLocalTime();
        public User User { get; set; }
    }
}
