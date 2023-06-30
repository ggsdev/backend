using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class PopulateControlAcessAndAuxiliaries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var data = new List<object[]>
            {
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryAnp", "Desenvolvimento",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryAnp", "Especial",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryAnp", "Extensão",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryAnp", "Injeção",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryAnp", "Jazida Mais Profunda",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryAnp", "Pioneiro Adjacente",  DateTime.UtcNow },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Reclassificacao",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Abandonado por acidente mecânico",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Extensão para petróleo",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Abandonado por outras razões",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Observação",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Indefinido",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Produtor comercial de petróleo",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Injeção de água",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Descobridor de nova jazida petróleo",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Descobridor de nova jazida petróleo e gás natural",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Portador de petróleo",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Extensão para gás natural",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Produtor subcomercial de petróleo",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Abandonado por perda circulação",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Abandonado por objetivo/alvo não atingido",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Experimental",  DateTime.UtcNow },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "WellProfile", "Vertical",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "WellProfile", "Horizontal",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "WellProfile", "Direcional",  DateTime.UtcNow },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryOperator", "Produtor",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryOperator", "Injetor de água",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryOperator", "Injetor de gás",  DateTime.UtcNow },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "N/A",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Gas Lift",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Bombeio Mecânico (BM)",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Bombeio de Cavidades Progressivas (BCP)",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Bombeio Centrífugo Submerso (BCS)",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Surgente",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Jet Pump",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Plunger",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Pistoneio",  DateTime.UtcNow },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "TypeBaseCoordinate", "Definitiva",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "TypeBaseCoordinate", "Provisória",  DateTime.UtcNow },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Fluid", "Gás",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Fluid", "Óleo",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Fluid", "Água",  DateTime.UtcNow },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Type", "EC",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Type", "ES",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Type", "EP",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Type", "CV",  DateTime.UtcNow },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypeEquipment", "Medidor ultrassônico",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypeEquipment", "Analisador de BSW",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypeEquipment", "Medidor coriolis",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypeEquipment", "Medidor Turbina",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypeEquipment", "Medidor magnético",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypeEquipment", "Apropriação ao poço",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypeEquipment", "Placa de orifício",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypeEquipment", "Proving",  DateTime.UtcNow },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypePoint", "Operacional",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypePoint", "Medição Fiscal",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypePoint", "Transferência de custódia",  DateTime.UtcNow },
            };

            foreach (var item in data)
            {
                var id = item[0];
                var route = item[1];
                var table = item[2];
                var select = item[3];
                var option = item[4];
                var updatedAt = item[5];


                migrationBuilder.InsertData(
                   table: "Auxiliaries",
                   columns: new[] { "Id", "Route", "Table", "Select", "Option", "UpdatedAt" },
                   values: new object[] { id, route, table, select, option, updatedAt }

               );
            }

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
             columns: new[] { "Name", "Email", "Password", "Username", "IsActive", "Id", "CreatedAt", "UpdatedAt", "GroupId", "Type", "IsPermissionDefault" },
             values: new object[] { nameUser, emailUser, BCrypt.Net.BCrypt.HashPassword(passwordUser), usernameUser, true, idUser, DateTime.UtcNow, DateTime.UtcNow, idGroup, nameGroup, true });

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
                new object[] { "Dados de Produção", "FileArrowUp", "2", "/dadosProducao", Guid.NewGuid(), false, false  },
                new object[] { "Gestão da Produção", "Gear", "3", "/gestaoProducao", Guid.NewGuid(), false, false   },
                new object[] { "Notificação de Falhas", "WarningOctagon", "4", "/notificacaoFalhas", Guid.NewGuid(), false, false   },
                new object[] { "Testes de Poços", "ClipboardText", "5", "/testePocos", Guid.NewGuid(), true, false  },
                new object[] { "Importar Dados Teste de Poço", "icon_importar_dados", "5.1", "/importarDadosTestePoco", Guid.NewGuid(), false, true  },
                new object[] { "Geração de Arquivos", "File", "6", "/geracaoArquivos", Guid.NewGuid(), false, false  },
                new object[] { "Relatórios", "Files", "7", "/relatorios", Guid.NewGuid(), false, false }
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
        /// <inheritdoc />
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

            var data = new List<object[]>
                {
                    new object[] { "/cadastrosBasicos", "Wells", "CategoryAnp" },
                    new object[] { "/cadastrosBasicos", "Wells", "CategoryReclassification" },
                    new object[] { "/cadastrosBasicos", "Wells", "WellProfile" },
                    new object[] { "/cadastrosBasicos", "Wells", "CategoryOperator" },
                    new object[] { "/cadastrosBasicos", "Wells", "ArtificialLift" },
                    new object[] { "/cadastrosBasicos", "Wells", "TypeBaseCoordinate" },
                    new object[] { "/cadastrosBasicos", "MeasuringEquipments", "Fluid" },
                    new object[] { "/cadastrosBasicos", "MeasuringEquipments", "TypePoint" }
                };

            foreach (var item in data)
            {
                var route = item[0];
                var table = item[1];
                var select = item[2];

                migrationBuilder.DeleteData(
                    table: "Auxiliaries",
                    keyColumns: new[] { "Route", "Table", "Select" },
                    keyValues: new object[] { route, table, select }
                );
            }
        }
    }
}
