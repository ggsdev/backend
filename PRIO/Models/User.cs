namespace PRIO.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

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
