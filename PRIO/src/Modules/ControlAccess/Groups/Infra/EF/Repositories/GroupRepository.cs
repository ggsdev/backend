using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Factories;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.Interfaces;
using PRIO.src.Modules.ControlAccess.Groups.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;
        private readonly GroupPermissionFactory _groupPermissionFactory;
        private readonly GroupOperationFactory _groupOperationFactory;

        public GroupRepository(DataContext context, GroupPermissionFactory groupPermissionFactory, GroupOperationFactory groupOperationFactory)
        {
            _context = context;
            _groupPermissionFactory = groupPermissionFactory;
            _groupOperationFactory = groupOperationFactory;
        }
        public async Task<Group?> GetGroupByIdAsync(Guid id)
        {
            return await _context.Groups.Include(x => x.GroupPermissions).ThenInclude(x => x.Operations).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
        }
        public async Task<Group?> GetGroupByNameAsync(string groupName)
        {

            return await _context.Groups.FirstOrDefaultAsync(x => x.Name == groupName);
        }
        public async Task<List<Group>> GetGroups()
        {
            return await _context.Groups.ToListAsync();
        }
        public async Task ValidateMenu(InsertGroupPermission body)
        {
            await MenuErrors.ValidateMenu(_context, body);
        }
        public async Task ValidateMenusByCreateViewModel(CreateGroupViewModel body)
        {
            await MenuErrors.ValidateMenu(_context, body);

        }
        public async Task<Group?> GetGroupWithPermissionsAndOperationsByIdAsync(Guid groupId)
        {
            return await _context.Groups
                .Where(x => x.Id == groupId)
                .Include(x => x.GroupPermissions!)
                .ThenInclude(x => x.Operations)
                .FirstOrDefaultAsync();
        }
        public async Task<Group> NewGroupPermissionsAsync(Group group, InsertGroupPermission body)
        {
            await MenuErrors.ValidateMenu(_context, body);

            foreach (var menuParent in body.Menus)
            {
                var groupPermissionParentId = Guid.NewGuid();

                var foundMenuParent = await _context.Menus
                    .Where(x => x.Id == menuParent.MenuId)
                    .FirstOrDefaultAsync();

                var addDotInOrder = foundMenuParent.Order + ".";
                var foundMenusChildrensInParent = await _context.Menus
                    .Where(x => x.Order.Contains(addDotInOrder))
                    .ToListAsync();

                var createGroupPermissionParent = _groupPermissionFactory.CreateGroupPermission(foundMenuParent.Icon, foundMenuParent.Name, foundMenuParent.Order, foundMenuParent.Route, foundMenuParent, group.Name, foundMenusChildrensInParent, group);
                await _context.GroupPermissions.AddAsync(createGroupPermissionParent);

                if (menuParent.Childrens is not null)
                {
                    if (menuParent.Childrens.Count != 0)
                    {
                        foreach (var menuChildren in menuParent.Childrens)
                        {
                            var groupPermissionChildrenId = Guid.NewGuid();

                            var foundMenuChildren = await _context.Menus.Where(x => x.Id == menuChildren.ChildrenId).FirstOrDefaultAsync();

                            var addDotInOrderChildren = foundMenuChildren.Order + ".";
                            var foundMenusChildrensInChildren = await _context.Menus.Where(x => x.Order.Contains(addDotInOrderChildren)).ToListAsync();
                            var createGroupPermissionChildren = _groupPermissionFactory.CreateGroupPermission(foundMenuChildren.Icon, foundMenuChildren.Name, foundMenuChildren.Order, foundMenuChildren.Route, foundMenuChildren, group.Name, foundMenusChildrensInChildren, group);
                            await _context.GroupPermissions.AddAsync(createGroupPermissionChildren);

                            foreach (var operationsChildren in menuChildren.Operations)
                            {
                                var groupOperationChildrenId = Guid.NewGuid();
                                var foundOperation = await _context.GlobalOperations.Where(x => x.Id == operationsChildren.OperationId).FirstOrDefaultAsync();
                                var createGroupOperationChildren = _groupOperationFactory.CreateGroupOperation(foundOperation, createGroupPermissionChildren, group.Name, foundOperation.Method);
                                await _context.GroupOperations.AddAsync(createGroupOperationChildren);
                            }
                        }
                    }
                }
                if (createGroupPermissionParent.hasChildren == false)
                {
                    foreach (var operationsParent in menuParent.Operations)
                    {
                        var groupOperationParentId = Guid.NewGuid();
                        var foundOperation = await _context.GlobalOperations.Where(x => x.Id == operationsParent.OperationId).FirstOrDefaultAsync();
                        var createGroupOperationParent = _groupOperationFactory.CreateGroupOperation(foundOperation, createGroupPermissionParent, group.Name, foundOperation.Method);
                        await _context.GroupOperations.AddAsync(createGroupOperationParent);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return group;
        }
        public void UpdateUser(User user)
        {
            _context.Update(user);
        }
        public void UpdateGroup(Group group)
        {
            _context.Groups.Update(group);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
