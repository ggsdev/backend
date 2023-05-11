﻿namespace PRIO.Models
{
    public class Unit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public User? User { get; set; }
        public List<Cluster> Clusters { get; set; }
    }
}
