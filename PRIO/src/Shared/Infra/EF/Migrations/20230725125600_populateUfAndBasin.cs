using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class populateUfAndBasin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var data = new List<object[]>
                {
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "ACRE",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "ALAGOAS",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "AMAPÁ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "AMAZONAS",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "BAHIA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "DISTRITO FEDERAL",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "CEARÁ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "ESPÍRITO SANTO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "GOIÁS",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "MARANHÃO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "MATO GROSSO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "MATO GROSSO DO SUL",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "MINAS GERAIS",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PARÁ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PARAÍBA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PARANÁ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PERNAMBUCO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PIAUÍ",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RIO GRANDE DO SUL",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RIO GRANDE DO NORTE",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RIO DE JANEIRO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RONDÔNIA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RORAIMA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "SANTA CATARINA",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "SÃO PAULO",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "SERGIPE",  DateTime.UtcNow },
                    new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "TOCANTINS",  DateTime.UtcNow },
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
