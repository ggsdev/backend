using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class DropAuxiliariesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Auxiliaries");

            migrationBuilder.CreateTable(
               name: "Auxiliaries",
               columns: table => new
               {
                   Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                   Option = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Route = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Table = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Select = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Auxiliaries", x => x.Id);
               });

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

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "Tramo A",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "Tramo B",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "Tramo C",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "Tramo D",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "Óleo recuperado TOG",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "Volume dreno",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "DOR A",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "DOR B",  DateTime.UtcNow },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "HPFlare",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "LPFlare",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "HighPressureGas",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "LowPressureGas",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ExportGas1",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ExportGas2",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ExportGas3",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ImportGas1",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ImportGas2",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ImportGas3",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "AssistanceGas",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "PilotGas",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "PurgeGas",  DateTime.UtcNow },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Fluid", "Gás",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Fluid", "Óleo",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Fluid", "Água",  DateTime.UtcNow },

                //new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Type", "EC",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Type", "Elemento Primário",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Type", "Elemento Secundário",  DateTime.UtcNow },
                //new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Type", "CV",  DateTime.UtcNow },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Medidor Ultrassônico",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Medidor Coriolis",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Medidor Turbina",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Medidor Magnético",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Medidor Multifásico",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Medidor de Deslocamento Positivo",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Placa de orifício",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Outros",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Secundário", "TypeEquipment", "Transmissor de Pressão Diferencial",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Secundário", "TypeEquipment", "Transmissor de Pressão Estátca",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Secundário", "TypeEquipment", "Transmissor de Temperatura",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Secundário", "TypeEquipment", "Transmissor Multivariável",  DateTime.UtcNow },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Secundário", "TypeEquipment", "Outros",  DateTime.UtcNow },

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

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
