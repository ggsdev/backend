namespace PRIO.src.Shared.Infra.EF.Models
{
    public class Backup
    {
        public Guid Id { get; set; }

        public DateTime date { get; set; }
        public string Directory { get; set; }
    }
}
