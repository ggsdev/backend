using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Factories
{
    public class UserOperationFactory
    {
        public UserOperation CreateUserOperation(UserGroupOperationDTO operation, UserPermission userPermission, GlobalOperation foundOperation, Group group)
        {
            var userOperationId = Guid.NewGuid();
            return new UserOperation
            {
                Id = userOperationId,
                OperationName = operation.OperationName,
                UserPermission = userPermission,
                GlobalOperation = foundOperation,
                GroupName = group.Name
            };
        }
    }
}
