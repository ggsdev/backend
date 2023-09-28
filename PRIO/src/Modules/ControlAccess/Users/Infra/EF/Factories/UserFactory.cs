using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.ViewModels;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Factories
{
    public class UserFactory
    {
        public User CreateUser(CreateUserViewModel body, string treatedUsername)
        {
            var userId = Guid.NewGuid();
            return new User
            {
                Id = userId,
                Name = body.Name,
                Username = treatedUsername,
                Email = body.Email is not null ? body.Email : null,
                IsActive = body.IsActive,
                Description = body.Description is not null ? body.Description : null,
            };
        }
    }
}
