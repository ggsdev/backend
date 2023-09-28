﻿namespace PRIO.src.Modules.PI.Infra.EF.Models
{
    public class Instance
    {
        public Guid Id { get; set; }
        public string WebId { get; set; }
        public string PIId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SelfRoute { get; set; }
        public string ElementsRoute { get; set; }
        public Database Database { get; set; }
        public List<Elements> ElementsInstace { get; set; }


    }
}
