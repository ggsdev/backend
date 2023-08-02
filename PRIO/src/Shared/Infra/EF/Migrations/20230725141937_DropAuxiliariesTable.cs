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

            var dataUF = new List<object[]>
                {
                   new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "AC",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "AL",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "AP",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "AM",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "BA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "DF",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "CE",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "ES",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "GO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "MA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "MT",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "MS",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "MG",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PB",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PE",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PI",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RS",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RN",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RJ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "SC",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "SP",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "SE",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "TO",  DateTime.UtcNow },

                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ACRE",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "AFOGADOS DA INGAZEIRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ÁGUA BONITA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ALAGOAS MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ALAGOAS TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ALMADA MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ALMADA TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ALTO TAPAJÓS",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "AMAPÁ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "AMAZONAS",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ARARIPE",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BANANAL",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BARREIRINHAS MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BARREIRINHAS TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BARRO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BETÂNIA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BOM NOME",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BRAGANÇA-VIZEU",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CAMAMU MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CAMAMU TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CAMPOS MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CAMPOS TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CEARÁ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CEDRO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CUMURUXATIBA MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CUMURUXATIBA TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CURITIBA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ESPÍRITO SANTO MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ESPÍRITO SANTO TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "FOZ DO AMAZONAS",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ICÓ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "IGUATU",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "IRECÊ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ITABERABA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ITABORAÍ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "JACUÍPE",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "JATOBÁ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "JEQUITINHONHA MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "JEQUITINHONHA TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "LIMA CAMPOS",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "MADRE DE DIOS",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "MALHADO VERMELHO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "MARAJÓ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "MIRANDIBA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "MUCURI MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "MUCURI TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PANTANAL",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PARÁ-MARANHÃO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PARANÁ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PARECIS-ALTO XINGU",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PARNAÍBA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PELOTAS MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PELOTAS TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PERNAMBUCO-PARAÍBA MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PERNAMBUCO-PARAÍBA TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "POMBAL",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "POTIGUAR MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "POTIGUAR TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "RECÔNCAVO MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "RECÔNCAVO TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "RESENDE",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "RIO DO PEIXE",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SANTOS",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SÃO FRANCISCO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SÃO JOSÉ DO BELMONTE",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SÃO LUÍS",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SÃO PAULO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SERGIPE MAR",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SERGIPE TERRA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SERRA DO INÁCIO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SOLIMÕES",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SOUZA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TACUTU",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TAUBATÉ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TRIUNFO (SERRA DOS FRADES)",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TUCANO CENTRAL",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TUCANO NORTE",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TUCANO SUL",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TUPANACI",  DateTime.UtcNow }
                };

            foreach (var item in dataUF)
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
