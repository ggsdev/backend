namespace PRIO.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = null;
        public bool IsActive { get; set; } = true;

        #region Relationships
        //public Role Role { get; set; }
        public Session? Session { get; set; } = null;
        public List<Cluster>? Clusters { get; set; }
        public List<Field>? Fields { get; set; }
        public List<Installation>? Installations { get; set; }
        public List<Reservoir>? Reservoirs { get; set; }
        public List<Completion>? Completions { get; set; }
        public List<Well>? Wells { get; set; }
        public List<Unit>? Units { get; set; }

        #endregion
    }
}
