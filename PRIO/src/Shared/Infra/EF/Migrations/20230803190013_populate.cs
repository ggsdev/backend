using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class populate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var data = new List<object[]>
                {
                    new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "T = Separador de teste",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "M = Medição Multifásica",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "G = Teste Simplificado de Poço de Gás",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "S = Teste Simplificado por Sonolog",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "R = Reinterpretação de Teste",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "A = Abertura de poço",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "F= Fechamento de poço",  DateTime.UtcNow },
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
