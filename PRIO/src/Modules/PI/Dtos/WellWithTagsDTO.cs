using PRIO.src.Modules.Hierarchy.Installations.Dtos;

namespace PRIO.src.Modules.PI.Dtos
{
    public class AttributeReturnDTO
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Field { get; set; }
        public string WellName { get; set; }
        public string CategoryOperator { get; set; }
        public string GroupParameter { get; set; }
        public string Parameter { get; set; }
        public string Tag { get; set; }
        public bool Operational { get; set; }
        public string CreatedAt { get; set; }
    }

    public class UepAttributesWellsDto
    {
        public Guid UepId { get; set; }
        public string UepName { get; set; }
        public List<InstallationWithAttributesDTO> Installations { get; set; }
    }
}
