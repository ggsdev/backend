using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Factories
{
    public class GroupFactory
    {
        public Group CreateGroup(string name, string description)
        {
            var groupId = Guid.NewGuid();
            return new Group
            {
                Id = groupId,
                Name = name,
                CreatedAt = DateTime.UtcNow.AddHours(-3),
                Description = description
            };
        }
    }
}
