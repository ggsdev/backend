using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Factories
{
    public class GroupOperationFactory
    {
        public GroupOperation CreateGroupOperation(GlobalOperation globalOperation, GroupPermission groupPermission, string groupName, string operationName)
        {
            var groupOperationChildrenId = Guid.NewGuid();
            return new GroupOperation
            {
                Id = groupOperationChildrenId,
                GlobalOperation = globalOperation,
                GroupPermission = groupPermission,
                GroupName = groupName,
                OperationName = operationName
            };
        }
    }
}
