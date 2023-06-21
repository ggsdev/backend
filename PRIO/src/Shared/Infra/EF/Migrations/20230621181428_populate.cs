using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class populate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // criação do grupo
            var groupData = new object[]
            {"Master", Guid.NewGuid()};

            var nameGroup = (string)groupData[0];
            var idGroup = (Guid)groupData[1];

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Name", "IsActive", "Id", "CreatedAt", "UpdatedAt" },
                values: new object[] { nameGroup, true, idGroup, DateTime.UtcNow, DateTime.UtcNow });

            var userData = new object[]
            {"Master","master@prio.com.br","master","Master", Guid.NewGuid()};
            var nameUser = (string)userData[0];
            var emailUser = (string)userData[1];
            var passwordUser = (string)userData[2];
            var usernameUser = (string)userData[3];
            var idUser = (Guid)userData[4];

            migrationBuilder.InsertData(
             table: "Users",
             columns: new[] { "Name", "Email", "Password", "Username", "IsActive", "Id", "CreatedAt", "UpdatedAt", "GroupId" },
             values: new object[] { nameUser, emailUser, BCrypt.Net.BCrypt.HashPassword(passwordUser), usernameUser, true, idUser, DateTime.UtcNow, DateTime.UtcNow, idGroup });


            //criação das operacoes
            var idGetOperation = Guid.NewGuid();
            var idPostOperation = Guid.NewGuid();
            var idPatchOperation = Guid.NewGuid();
            var idPutOperation = Guid.NewGuid();
            var idDeleteOperation = Guid.NewGuid();

            var operationData = new List<object[]>
            {
                new object[] { "GET" , idGetOperation },
                new object[] { "POST", idPostOperation },
                new object[] { "PATCH", idPatchOperation },
                new object[] { "PUT", idPutOperation },
                new object[] { "DELETE" , idDeleteOperation }
            };
            foreach (var operation in operationData)
            {
                var methodOperation = (string)operation[0];
                var idOperation = (Guid)operation[1];

                migrationBuilder.InsertData(
                    table: "GlobalOperations",
                    columns: new[] { "Method", "IsActive", "Id", "CreatedAt", "UpdatedAt" },
                    values: new object[] { methodOperation, true, idOperation, DateTime.UtcNow, DateTime.UtcNow });
            }


            //criação do menu (1 - hasChildren, 2 - hasParent)
            var menuData = new List<object[]>
            {
                new object[] { "Administração", "icon_administracao", "1", "/administracao", Guid.NewGuid(), true, false },
                new object[] { "Cadastro básico", "icon_cadastro", "1.1", "/cadastro-basico", Guid.NewGuid(), false, true },
                new object[] { "Grupo", "icon_grupo", "1.2", "/grupo", Guid.NewGuid(), false, true  },
                new object[] { "Usuário", "icon_usuario", "1.3", "/usuario", Guid.NewGuid(), false, true },
                new object[] { "Configurar cálculos", "icon_configuracao", "1.4", "/configurar-calculos", Guid.NewGuid(), false, true  },
                new object[] { "Dados de produção", "icon_dados_producao", "2", "/dados-producao", Guid.NewGuid(), false, false  },
                new object[] { "Gestão da produção", "icon_gestao_producao", "3", "/gestao-producao", Guid.NewGuid(), false, false   },
                new object[] { "Identificação de falhas", "icon_falhas", "4", "/identificacao-falhas", Guid.NewGuid(), false, false   },
                new object[] { "Teste de poços", "icon_teste_pocos", "5", "/teste-pocos", Guid.NewGuid(), true, false  },
                new object[] { "Importar dados teste de Poço", "icon_importar_dados", "5.1", "/importar-dados", Guid.NewGuid(), false, true  },
                new object[] { "Geração de arquivos", "icon_geracao_arquivos", "6", "/geracao-arquivos", Guid.NewGuid(), false, false  },
                new object[] { "Relatórios", "icon_relatorios", "7", "/relatorios", Guid.NewGuid(), false, false }
            };
            foreach (var menu in menuData)
            {
                var nameMenu = (string)menu[0];
                var iconMenu = (string)menu[1];
                var orderMenu = (string)menu[2];
                var routeMenu = (string)menu[3];
                var idMenu = (Guid)menu[4];
                var hasChildren = (bool)menu[5];
                var hasParent = (bool)menu[6];
                var idGroupPermission = Guid.NewGuid();
                var idUserPermission = Guid.NewGuid();




                if (hasParent == true)
                {
                    // Adicionar logica para localizar o pai e adicionar
                    Guid? FindParentId(List<object[]> menuData, string orderMenu)
                    {
                        var parentOrder = GetParentOrder(orderMenu);

                        foreach (var menu in menuData)
                        {
                            var menuOrder = (string)menu[2];
                            if (menuOrder == parentOrder)
                            {
                                var parentId = (Guid)menu[4];
                                return parentId;
                            }
                        }
                        return null;
                    }

                    string GetParentOrder(string orderMenu)
                    {
                        var orderParts = orderMenu.Split('.');
                        var parentOrder = string.Join(".", orderParts.Take(orderParts.Length - 1));
                        return parentOrder;
                    }

                    var parentId = FindParentId(menuData, orderMenu);
                    migrationBuilder.InsertData(
                      table: "Menus",
                      columns: new[] { "Name", "Icon", "Order", "Route", "ParentId", "IsActive", "Id", "CreatedAt", "UpdatedAt" },
                      values: new object[] { nameMenu, iconMenu, orderMenu, routeMenu, parentId, true, idMenu, DateTime.UtcNow, DateTime.UtcNow, });
                }
                else
                {

                    migrationBuilder.InsertData(
                        table: "Menus",
                        columns: new[] { "Name", "Icon", "Order", "Route", "ParentId", "IsActive", "Id", "CreatedAt", "UpdatedAt" },
                        values: new object[] { nameMenu, iconMenu, orderMenu, routeMenu, null, true, idMenu, DateTime.UtcNow, DateTime.UtcNow });

                }

                migrationBuilder.InsertData(
                    table: "GroupPermissions",
                    columns: new[] { "Id", "GroupId", "GroupName", "MenuId", "MenuName", "MenuOrder", "MenuIcon", "MenuRoute", "CreatedAt", "UpdatedAt", "hasParent", "hasChildren", "IsActive" },
                    values: new object[] { idGroupPermission, idGroup, nameGroup, idMenu, nameMenu, orderMenu, iconMenu, routeMenu, DateTime.UtcNow, DateTime.UtcNow, hasParent, hasChildren, true });

                migrationBuilder.InsertData(
                    table: "UserPermissions",
                    columns: new[] { "Id", "UserId", "GroupMenuId", "GroupId", "GroupName", "MenuId", "MenuName", "MenuOrder", "MenuIcon", "MenuRoute", "CreatedAt", "hasParent", "hasChildren" },
                    values: new object[] { idUserPermission, idUser, idGroupPermission, idGroup, nameGroup, idMenu, nameMenu, orderMenu, iconMenu, routeMenu, DateTime.UtcNow, hasParent, hasChildren });

                if (hasChildren == false)
                {
                    foreach (var operation in operationData)
                    {
                        var methodOperation = (string)operation[0];
                        var idOperation = (Guid)operation[1];

                        migrationBuilder.InsertData(
                            table: "GroupOperations",
                            columns: new[] { "Id", "OperationName", "GlobalOperationId", "GroupPermissionId", "GroupName" },
                            values: new object[] { Guid.NewGuid(), methodOperation, idOperation, idGroupPermission, nameGroup });

                        migrationBuilder.InsertData(
                            table: "UserOperations",
                            columns: new[] { "Id", "OperationName", "GlobalOperationId", "UserPermissionId", "GroupName" },
                            values: new object[] { Guid.NewGuid(), methodOperation, idOperation, idUserPermission, nameGroup });
                    }
                }
            }



        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var menuData = new List<string>
            {
                "/relatorios",
                "/geracao-arquivos",
                "/importar-dados",
                "/teste-pocos",
                "/identificacao-falhas",
                "/gestao-producao",
                "/dados-producao",
                "/configurar-calculos",
                "/usuario",
                "/grupo",
                "/cadastro-basico",
                "/administracao"
            };

            foreach (var route in menuData)
            {
                migrationBuilder.DeleteData(
                    table: "Menus",
                    keyColumn: "Route",
                    keyValues: new object[] { route });
            }
        }
    }
}
