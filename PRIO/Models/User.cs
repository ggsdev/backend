namespace PRIO.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();
        public DateTime? UpdatedAt { get; set; } = null;
        public bool IsActive { get; set; } = true;

        #region Relationships
        //public Role Role { get; set; }
        public Session? Session { get; set; } = null;
        public List<Cluster>? Clusters { get; set; } = null;
        public List<Field>? Fields { get; set; } = null;
        public List<Installation>? Installations { get; set; } = null;
        public List<Reservoir>? Reservoirs { get; set; } = null;
        public List<Completion>? Completions { get; set; } = null;
        public List<Well>? Wells { get; set; } = null;
        public List<Unit>? Units { get; set; } = null;

        #endregion
    }
}
