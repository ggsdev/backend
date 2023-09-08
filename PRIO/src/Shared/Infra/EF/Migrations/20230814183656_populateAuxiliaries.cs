using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class populateAuxiliaries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var data = new List<object[]>
            {
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryAnp", "Desenvolvimento",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryAnp", "Especial",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryAnp", "Extensão",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryAnp", "Injeção",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryAnp", "Jazida Mais Profunda",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryAnp", "Pioneiro Adjacente",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Reclassificacao",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Abandonado por acidente mecânico",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Extensão para petróleo",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Abandonado por outras razões",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Observação",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Indefinido",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Produtor comercial de petróleo",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Injeção de água",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Descobridor de nova jazida petróleo",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Descobridor de nova jazida petróleo e gás natural",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Portador de petróleo",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Extensão para gás natural",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Produtor subcomercial de petróleo",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Abandonado por perda circulação",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Abandonado por objetivo/alvo não atingido",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryReclassification", "Experimental",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "WellProfile", "Vertical",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "WellProfile", "Horizontal",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "WellProfile", "Direcional",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryOperator", "Produtor",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryOperator", "Injetor de água",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "CategoryOperator", "Injetor de gás",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "N/A",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Gas Lift",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Bombeio Mecânico (BM)",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Bombeio de Cavidades Progressivas (BCP)",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Bombeio Centrífugo Submerso (BCS)",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Surgente",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Jet Pump",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Plunger",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "ArtificialLift", "Pistoneio",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "TypeBaseCoordinate", "Definitiva",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Wells", "TypeBaseCoordinate", "Provisória",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Location", "Terra",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Location", "Mar",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "Tramo A",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "Tramo B",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "Tramo C",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "Tramo D",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "Óleo recuperado TOG",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "Volume dreno",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "DOR A",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "DOR B",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "HPFlare",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "LPFlare",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "HighPressureGas",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "LowPressureGas",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ExportGas1",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ExportGas2",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ExportGas3",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ImportGas1",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ImportGas2",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ImportGas3",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "AssistanceGas",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "PilotGas",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "PurgeGas",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Fluid", "Gás",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Fluid", "Óleo",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Fluid", "Água",  DateTime.UtcNow.AddHours(-3) },

                //new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Type", "EC",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Type", "Elemento Primário",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Type", "Elemento Secundário",  DateTime.UtcNow.AddHours(-3) },
                //new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "Type", "CV",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Medidor Ultrassônico",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Medidor Coriolis",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Medidor Turbina",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Medidor Magnético",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Medidor Multifásico",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Medidor de Deslocamento Positivo",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Placa de orifício",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Primário", "TypeEquipment", "Outros",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Secundário", "TypeEquipment", "Transmissor de Pressão Diferencial",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Secundário", "TypeEquipment", "Transmissor de Pressão Estátca",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Secundário", "TypeEquipment", "Transmissor de Temperatura",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Secundário", "TypeEquipment", "Transmissor Multivariável",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Elemento Secundário", "TypeEquipment", "Outros",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypePoint", "Operacional",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypePoint", "Medição Fiscal",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringEquipments", "TypePoint", "Transferência de custódia",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "T = Separador de teste",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "M = Medição Multifásica",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "G = Teste Simplificado de Poço de Gás",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "S = Teste Simplificado por Sonolog",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "R = Reinterpretação de Teste",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "A = Abertura de poço",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/importarDadosTestePoco", "Teste", "Tipo de Teste", "F= Fechamento de poço",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Sistema Relacionado", "Topside",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Sistema Relacionado", "Submarino",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Sistema Relacionado", "Estratégia",  DateTime.UtcNow.AddHours(-3) },


                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Abandonado permanentemente",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Abandonado temporariamente com monitoramento",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Abandonado temporariamente sem monitoramento",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Arrasado",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Produzindo",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Injetando",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Retirando gás natural estocado",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Injetando para estocagem",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Equipado aguardando início de operação",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Fechado",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Em observação",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Em perfuração",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Em avaliação",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Em completação",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Em intervenção",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Operando para captação de água",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Cedido para captação de água",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Operando para descarte",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "WellEvents", "Status ANP", "Produzindo e injetando",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/eventosPoco", "Equipado aguardando início de operação", "Status ANP", "Poço-coluna novo",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "Produzindo", "Status ANP", "Operação normal",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "Produzindo", "Status ANP", "Operação com deficiência",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/eventosPoco", "Injetando", "Status ANP", "Operação com deficiência",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "Injetando", "Status ANP", "Operação normal",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/eventosPoco", "Produzindo e injetando", "Status ANP", "Operação com deficiência",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "Produzindo e injetando", "Status ANP", "Operação normal",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/eventosPoco", "Retirando gás natural estocado", "Status ANP", "Operação com deficiência",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "Retirando gás natural estocado", "Status ANP", "Operação normal",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/eventosPoco", "Injetando para estocagem", "Status ANP", "Operação com deficiência",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "Injetando para estocagem", "Status ANP", "Operação normal",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/eventosPoco", "Operando para captação de água", "Status ANP", "Operação com deficiência",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "Operando para captação de água", "Status ANP", "Operação normal",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/eventosPoco", "Fechado", "Status ANP", "Fechado por perda",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/eventosPoco", "Fechado", "Status ANP", "Fechado por estratégia",  DateTime.UtcNow.AddHours(-3) },

                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "ACRE",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "ALAGOAS",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "AMAPÁ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "AMAZONAS",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "BAHIA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "DISTRITO FEDERAL",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "CEARÁ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "ESPÍRITO SANTO",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "GOIÁS",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "MARANHÃO",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "MATO GROSSO",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "MATO GROSSO DO SUL",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "MINAS GERAIS",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PARÁ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PARAÍBA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PARANÁ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PERNAMBUCO",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "PIAUÍ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RIO GRANDE DO SUL",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RIO GRANDE DO NORTE",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RIO DE JANEIRO",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RONDÔNIA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "RORAIMA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "SANTA CATARINA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "SÃO PAULO",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "SERGIPE",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "UF", "TOCANTINS",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ACRE",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "AFOGADOS DA INGAZEIRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ÁGUA BONITA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ALAGOAS MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ALAGOAS TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ALMADA MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ALMADA TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ALTO TAPAJÓS",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "AMAPÁ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "AMAZONAS",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ARARIPE",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BANANAL",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BARREIRINHAS MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BARREIRINHAS TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BARRO",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BETÂNIA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BOM NOME",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "BRAGANÇA-VIZEU",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CAMAMU MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CAMAMU TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CAMPOS MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CAMPOS TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CEARÁ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CEDRO",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CUMURUXATIBA MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CUMURUXATIBA TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "CURITIBA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ESPÍRITO SANTO MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ESPÍRITO SANTO TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "FOZ DO AMAZONAS",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ICÓ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "IGUATU",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "IRECÊ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ITABERABA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "ITABORAÍ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "JACUÍPE",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "JATOBÁ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "JEQUITINHONHA MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "JEQUITINHONHA TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "LIMA CAMPOS",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "MADRE DE DIOS",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "MALHADO VERMELHO",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "MARAJÓ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "MIRANDIBA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "MUCURI MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "MUCURI TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PANTANAL",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PARÁ-MARANHÃO",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PARANÁ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PARECIS-ALTO XINGU",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PARNAÍBA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PELOTAS MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PELOTAS TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PERNAMBUCO-PARAÍBA MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "PERNAMBUCO-PARAÍBA TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "POMBAL",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "POTIGUAR MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "POTIGUAR TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "RECÔNCAVO MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "RECÔNCAVO TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "RESENDE",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "RIO DO PEIXE",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SANTOS",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SÃO FRANCISCO",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SÃO JOSÉ DO BELMONTE",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SÃO LUÍS",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SÃO PAULO",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SERGIPE MAR",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SERGIPE TERRA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SERRA DO INÁCIO",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SOLIMÕES",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "SOUZA",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TACUTU",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TAUBATÉ",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TRIUNFO (SERRA DOS FRADES)",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TUCANO CENTRAL",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TUCANO NORTE",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TUCANO SUL",  DateTime.UtcNow.AddHours(-3) },
                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "Fields", "Basin", "TUPANACI",  DateTime.UtcNow.AddHours(-3) }
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
