using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class InsertingValuesInAuxiliary : Migration
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
            var data = new List<object[]>
                {
                    new object[] { "/cadastrosBasicos", "Wells", "CategoryAnp" },
                    new object[] { "/cadastrosBasicos", "Wells", "CategoryReclassification" },
                    new object[] { "/cadastrosBasicos", "Wells", "WellProfile" },
                    new object[] { "/cadastrosBasicos", "Wells", "CategoryOperator" },
                    new object[] { "/cadastrosBasicos", "Wells", "ArtificialLift" }
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
