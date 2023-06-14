using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class initialPopulate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var menuData = new List<object[]>
            {
                new object[] { "Administração", "icon_administracao", "1", "/administracao" },
                new object[] { "Cadastro básico", "icon_cadastro", "1.1", "/cadastro-basico" },
                new object[] { "Grupo", "icon_grupo", "1.2", "/grupo" },
                new object[] { "Usuário", "icon_usuario", "1.3", "/usuario" },
                new object[] { "Configurar cálculos", "icon_configuracao", "1.4", "/configurar-calculos" },
                new object[] { "Dados de produção", "icon_dados_producao", "2", "/dados-producao" },
                new object[] { "Gestão da produção", "icon_gestao_producao", "3", "/gestao-producao" },
                new object[] { "Identificação de falhas", "icon_falhas", "4", "/identificacao-falhas" },
                new object[] { "Teste de poços", "icon_teste_pocos", "5", "/teste-pocos" },
                new object[] { "Importar dados teste de Poço", "icon_importar_dados", "5.1", "/importar-dados" },
                new object[] { "Geração de arquivos", "icon_geracao_arquivos", "6", "/geracao-arquivos" },
                new object[] { "Relatórios", "icon_relatorios", "7", "/relatorios" }
            };

            foreach (var menu in menuData)
            {
                var name = (string)menu[0];
                var icon = (string)menu[1];
                var order = (string)menu[2];
                var route = (string)menu[3];

                migrationBuilder.InsertData(
                    table: "Menus",
                    columns: new[] { "Name", "Icon", "Order", "Route", "ParentId", "IsActive", "Id", "CreatedAt", "UpdatedAt" },
                    values: new object[] { name, icon, order, route, null, true, Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow });

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

