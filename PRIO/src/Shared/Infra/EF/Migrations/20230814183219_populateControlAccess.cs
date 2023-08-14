﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class populateControlAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // criação do grupo MASTER
            var groupData = new object[]
            {"Master", Guid.NewGuid()};
            var nameGroup = (string)groupData[0];
            var idGroup = (Guid)groupData[1];

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Name", "IsActive", "Id", "CreatedAt", "UpdatedAt" },
                values: new object[] { nameGroup, true, idGroup, DateTime.UtcNow, DateTime.UtcNow });

            // criação do grupo BASICO
            var basicGroup = new object[]
           {"Básico", Guid.NewGuid()};
            var nameBasicGroup = (string)basicGroup[0];
            var idBasicGroup = (Guid)basicGroup[1];

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Name", "IsActive", "Id", "CreatedAt", "UpdatedAt" },
                values: new object[] { nameBasicGroup, true, idBasicGroup, DateTime.UtcNow, DateTime.UtcNow });

            // criação user master
            var userData = new object[]
            {"Master","master@prio.com.br","master","Master", Guid.NewGuid()};
            var nameUser = (string)userData[0];
            var emailUser = (string)userData[1];
            var passwordUser = (string)userData[2];
            var usernameUser = (string)userData[3];
            var idUser = (Guid)userData[4];

            migrationBuilder.InsertData(
             table: "Users",
             columns: new[] { "Name", "Email", "Password", "Username", "IsActive", "Id", "CreatedAt", "UpdatedAt", "GroupId", "Type", "IsPermissionDefault" },
             values: new object[] { nameUser, emailUser, BCrypt.Net.BCrypt.HashPassword(passwordUser), usernameUser, true, idUser, DateTime.UtcNow, DateTime.UtcNow, idGroup, nameGroup, true });

            var usersData = new List<object[]>
            {
                new object[] { "Fernando", "falberdi.globalhitts@prio3.com.br", null, "falberdi.globalhitts", Guid.NewGuid()} ,
                new object[] { "Garcia", "gabriel@prio3.com.br", null, "gabriel", Guid.NewGuid()},
                new object[] { "Pablo", "pablo@prio3.com.br", null, "pablo", Guid.NewGuid()},
                new object[] { "Vera Braga", "vera@prio3.com.br", null, "vera", Guid.NewGuid()},
                new object[] { "Alessandro", "alessandro@prio3.com.br", null, "alessandro", Guid.NewGuid()},
            };

            foreach (var user in usersData)
            {
                var nameUserNotMaster = user[0];
                var emailUserNotMaster = user[1];
                var passwordUserNotMaster = user[2];
                var usernameUserNotMaster = (string)user[3];
                var idUserNotMaster = (Guid)user[4];

                migrationBuilder.InsertData(
                    table: "Users",
                            columns: new[]
                            {
                            "Name", "Email", "Password", "Username", "IsActive", "Id", "CreatedAt",
                            "UpdatedAt", "GroupId", "Type", "IsPermissionDefault"
                            },
                            values: new object[]
                            {
                            nameUserNotMaster, emailUserNotMaster, passwordUserNotMaster, usernameUserNotMaster,
                            true, idUserNotMaster, DateTime.UtcNow, DateTime.UtcNow, idGroup, nameGroup, true
                            });

            }

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
                new object[] { "Administração", "User", "1", "/administracao", Guid.NewGuid(), true, false },
                new object[] { "Cadastros Básicos", "icon_cadastro", "1.1", "/cadastrosBasicos", Guid.NewGuid(), false, true },
                new object[] { "Grupos", "icon_grupo", "1.2", "/grupos", Guid.NewGuid(), false, true  },
                new object[] { "Usuários", "icon_usuario", "1.3", "/usuarios", Guid.NewGuid(), false, true },
                new object[] { "Configurar Cálculos", "icon_configuracao", "1.4", "/configurarCalculos", Guid.NewGuid(), false, true  },
                new object[] { "Dados de Produção", "FileArrowUp", "2", "/dadosProducao", Guid.NewGuid(), true, false  },
                new object[] { "Gestão da Produção", "Gear", "3", "/gestaoProducao", Guid.NewGuid(), false, false   },
                new object[] { "Notificação de Falhas", "WarningOctagon", "4", "/notificacaoFalhas", Guid.NewGuid(), false, false   },
                new object[] { "Testes de Poços", "ClipboardText", "5", "/testePocos", Guid.NewGuid(), true, false  },
                new object[] { "Importar Dados Teste de Poço", "icon_importar_dados", "5.1", "/importarDadosTestePoco", Guid.NewGuid(), false, true  },
                new object[] { "Geração de Arquivos", "File", "6", "/geracaoArquivos", Guid.NewGuid(), false, false  },
                new object[] { "Relatórios", "Files", "7", "/relatorios", Guid.NewGuid(), false, false },
                new object[] { "Importar Dados de Hierarquia", "", "1.5", "/importarDadosHierarquia", Guid.NewGuid(), false, true },
                new object[] { "Importação dos Dados de Produção", "", "2.1", "/importarDadosProducao", Guid.NewGuid(), false, true }
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

                // PERMISSÕES PARA O GROUP MASTER
                migrationBuilder.InsertData(
                    table: "GroupPermissions",
                    columns: new[] { "Id", "GroupId", "GroupName", "MenuId", "MenuName", "MenuOrder", "MenuIcon", "MenuRoute", "CreatedAt", "UpdatedAt", "hasParent", "hasChildren", "IsActive" },
                    values: new object[] { idGroupPermission, idGroup, nameGroup, idMenu, nameMenu, orderMenu, iconMenu, routeMenu, DateTime.UtcNow, DateTime.UtcNow, hasParent, hasChildren, true });

                // PERMISSÕES PARA O GROUP BASICO
                var idGroupBasicoPermission = Guid.NewGuid();
                if ((string)menu[0] == "Administração" || (string)menu[0] == "Cadastros Básicos")
                {
                    migrationBuilder.InsertData(
                        table: "GroupPermissions",
                        columns: new[] { "Id", "GroupId", "GroupName", "MenuId", "MenuName", "MenuOrder", "MenuIcon", "MenuRoute", "CreatedAt", "UpdatedAt", "hasParent", "hasChildren", "IsActive" },
                        values: new object[] { idGroupBasicoPermission, idBasicGroup, nameBasicGroup, idMenu, nameMenu, orderMenu, iconMenu, routeMenu, DateTime.UtcNow, DateTime.UtcNow, hasParent, hasChildren, true });
                };

                // PERMISSAO PARA USER MASTER

                migrationBuilder.InsertData(
                    table: "UserPermissions",
                    columns: new[] { "Id", "UserId", "GroupMenuId", "GroupId", "GroupName", "MenuId", "MenuName", "MenuOrder", "MenuIcon", "MenuRoute", "CreatedAt", "hasParent", "hasChildren" },
                    values: new object[] { idUserPermission, idUser, idGroupPermission, idGroup, nameGroup, idMenu, nameMenu, orderMenu, iconMenu, routeMenu, DateTime.UtcNow, hasParent, hasChildren });

                if (hasChildren == false)
                {
                    // OPERACOES PARA MASTER
                    foreach (var operation in operationData)
                    {
                        var methodOperation = (string)operation[0];
                        var idOperation = (Guid)operation[1];

                        migrationBuilder.InsertData(
                            table: "GroupOperations",
                            columns: new[] { "Id", "OperationName", "GlobalOperationId", "GroupPermissionId", "GroupName" },
                            values: new object[] { Guid.NewGuid(), methodOperation, idOperation, idGroupPermission, nameGroup });

                        if ((string)menu[0] == "Administração" || (string)menu[0] == "Cadastros Básicos")
                        {
                            if (methodOperation == "GET")
                            {
                                migrationBuilder.InsertData(
                                    table: "GroupOperations",
                                    columns: new[] { "Id", "OperationName", "GlobalOperationId", "GroupPermissionId", "GroupName" },
                                    values: new object[] { Guid.NewGuid(), methodOperation, idOperation, idGroupBasicoPermission, nameBasicGroup });
                            }
                        }

                        migrationBuilder.InsertData(
                        table: "UserOperations",
                        columns: new[] { "Id", "OperationName", "GlobalOperationId", "UserPermissionId", "GroupName" },
                        values: new object[] { Guid.NewGuid(), methodOperation, idOperation, idUserPermission, nameGroup });

                    }
                }
                // OUTROS USUÁRIOS 
                foreach (var user in usersData)
                {
                    var idUserPermissionNotMaster = Guid.NewGuid();

                    var userIdNotMaster = (Guid)user[4];

                    migrationBuilder.InsertData(
                            table: "UserPermissions",
                            columns: new[]
                            {
                                "Id", "UserId", "GroupMenuId", "GroupId", "GroupName", "MenuId", "MenuName",
                                "MenuOrder", "MenuIcon", "MenuRoute", "CreatedAt", "hasParent", "hasChildren"
                            },
                            values: new object[]
                            {
                                idUserPermissionNotMaster , userIdNotMaster , idGroupPermission, idGroup, nameGroup,
                                idMenu, nameMenu, orderMenu, iconMenu, routeMenu, DateTime.UtcNow, hasParent, hasChildren
                            });

                    if (hasChildren == false)
                    {
                        foreach (var operation in operationData)
                        {
                            var methodOperation = (string)operation[0];
                            var idOperation = (Guid)operation[1];

                            migrationBuilder.InsertData(
                                table: "UserOperations",
                                columns: new[] { "Id", "OperationName", "GlobalOperationId", "UserPermissionId", "GroupName" },
                                values: new object[] { Guid.NewGuid(), methodOperation, idOperation, idUserPermissionNotMaster, nameGroup });
                        }
                    }
                }
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}