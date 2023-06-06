using PRIO.Models.BaseModels;

namespace PRIO.Models.Users
{
    public class UserHistory : BaseHistoryModel
    {
        public string? Name { get; set; }
        public string? NameOld { get; set; }
        public string? Email { get; set; }
        public string? EmailOld { get; set; }
        public string? Password { get; set; }
        public string? PasswordOld { get; set; }
        public string? Username { get; set; }
        public string? UsernameOld { get; set; }
        public string? Type { get; set; }
        public string? TypeOld { get; set; }
        public User? User { get; set; }
        public Guid? UserOperationId { get; set; }
    }
}
