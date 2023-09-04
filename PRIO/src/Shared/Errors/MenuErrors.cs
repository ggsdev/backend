using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Groups.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.ViewModels;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Shared.Errors
{
    public static class MenuErrors
    {
        public static async Task ValidateMenu(DataContext _context, InsertUserPermissionViewModel body)
        {
            await ValidateMenuInternal(_context, body.Menus);
        }

        public static async Task ValidateMenu(DataContext _context, CreateGroupViewModel body)
        {
            await ValidateMenuInternal(_context, body.Menus);
        }

        public static async Task ValidateMenu(DataContext _context, InsertGroupPermission body)
        {
            await ValidateMenuInternal(_context, body.Menus);
        }

        private static async Task ValidateMenuInternal(DataContext _context, List<MenuParentInGroupViewModel> menus)
        {
            if (menus is null)
                throw new NotFoundException("Menus not found");
            foreach (var menuParent in menus)
            {
                var foundMenuParent = await _context.Menus
                .Include(x => x.Parent)
                    .Where(x => x.Id == menuParent.MenuId)
                    .FirstOrDefaultAsync();

                if (foundMenuParent is null)
                    throw new NotFoundException("Menu Parent is not found");

                if (menuParent.Childrens is not null)
                {
                    foreach (var menuChildren in menuParent.Childrens)
                    {
                        var foundMenuChildren = await _context.Menus
                            .Where(x => x.Id == menuChildren.ChildrenId)
                            .Include(x => x.Parent)
                            .FirstOrDefaultAsync();

                        if (foundMenuChildren is null)
                            throw new NotFoundException("Menu Children is not found");

                        if (foundMenuChildren.Parent is null)
                            throw new NotFoundException("Relation Menu Children is not found");

                        string[] parts = foundMenuChildren.Order.Split('.');
                        string numberBeforeDot = parts[0];

                        if (numberBeforeDot != foundMenuParent.Order)
                            throw new ConflictException("This child menu does not belong to the parent menu");

                        foreach (var operationsChildrens in menuChildren.Operations)
                        {
                            var foundOperationChildren = await _context.GlobalOperations.Where(x => x.Id == operationsChildrens.OperationId).FirstOrDefaultAsync();
                            if (foundOperationChildren is null)
                                throw new NotFoundException("Operation Children is not found");
                        }
                    }
                }
                if (foundMenuParent.Parent == null)
                {

                    var verifyChildren = await _context.Menus
                    .Where(x => !x.Id.Equals(foundMenuParent.Id))
                    .Where(x => x.Order.Equals(foundMenuParent.Order))
                    .FirstOrDefaultAsync();

                    if (verifyChildren is null && menuParent.Operations is not null)
                    {
                        throw new NotFoundException("Menu Parent don't need operations");
                    }
                    else if (verifyChildren is not null && menuParent.Operations is null)
                    {
                        throw new NotFoundException("Menu Parent need operations");
                    }
                    else if (verifyChildren is null && menuParent.Operations is not null)
                    {
                        if (menuParent.Operations.Count == 0)
                        {
                            throw new NotFoundException("Menu Parent need almost one operationId");
                        }
                        foreach (var operationsParent in menuParent.Operations)
                        {
                            var foundOperationParent = await _context.GlobalOperations
                                .Where(x => x.Id == operationsParent.OperationId)
                                .FirstOrDefaultAsync();

                            if (foundOperationParent is null)
                                throw new NotFoundException("Operation Parent is not found");
                        }
                    }

                }
                else
                {
                    if (menuParent.Operations is not null && menuParent.Operations.Count > 0)
                    {
                        if (menuParent.Operations.Count == 0)
                        {
                            throw new NotFoundException("Menu Parent need almost one operationId");
                        }
                        foreach (var operationsParent in menuParent.Operations)
                        {
                            var foundOperationParent = await _context.GlobalOperations
                                .Where(x => x.Id == operationsParent.OperationId)
                                .FirstOrDefaultAsync();

                            if (foundOperationParent is null)
                                throw new NotFoundException("Operation Parent is not found");
                        }
                    }
                    else if (menuParent.Operations is null)
                    {
                        throw new NotFoundException("Menu Children need operations");
                    }
                }
            }
        }
    }
}
