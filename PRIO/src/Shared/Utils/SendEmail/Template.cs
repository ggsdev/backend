using PRIO.src.Modules.FileImport.XML.Measuring.Dtos;
using System.Text;

namespace PRIO.src.Shared.Utils.SendEmail
{
    public static class Template
    {
        public static string GenerateNotificationEmail(Client039DTO nfsm)
        {
            var builder = new StringBuilder();

            builder.AppendLine(@"<!DOCTYPE html>");
            builder.AppendLine(@"<html>");
            builder.AppendLine(@"<head>");
            builder.AppendLine(@"<title>Correção de Valores - Notificação</title>");
            builder.AppendLine(@"</head>");
            builder.AppendLine(@"<body>");
            builder.AppendLine(@"<h2>Notificação de Falha</h2>");
            builder.AppendLine(@"<ul>");
            builder.AppendFormat("<li>Ponto de medição: {0}</li>", nfsm.COD_TAG_PONTO_MEDICAO_039);
            builder.AppendFormat("<li>Equipamento: {0}</li>", nfsm.COD_TAG_EQUIPAMENTO_039);
            builder.AppendFormat("<li>Tipo de Falha: {0} – Código e descrição do tipo de falha conforme tabela TIPO FALHA</li>", nfsm.DSC_TIPO_FALHA_039);
            builder.AppendFormat("<li>Código da Falha: {0} – Código interno da falha gerada.</li>", nfsm.COD_FALHA_039);
            builder.AppendFormat("<li>Data da ocorrência: {0} – Data e hora em que a falha ocorreu</li>", nfsm.DHA_OCORRENCIA_039);
            builder.AppendFormat("<li>Data da detecção: {0} – Data e hora que a falha foi detectada</li>", nfsm.DHA_DETECCAO_039);
            builder.AppendFormat("<li>Data do retorno: {0} – Data e hora em que a falha foi corrigida</li>", nfsm.DHA_RETORNO_039);
            builder.AppendFormat("<li>Descrição da Falha: {0} Descrição da falha ocorrida</li>", nfsm.DHA_DSC_FALHA_039);
            builder.AppendFormat("<li>Ação: {0} Ações tomadas</li>", nfsm.DHA_DSC_ACAO_039);
            builder.AppendFormat("<li>Metodologia: {0} Descrição da metodologia aplicada.</li>", nfsm.DHA_DSC_METODOLOGIA_039);
            builder.AppendLine(@"</ul>");
            builder.AppendLine(@"</body>");
            builder.AppendLine(@"</html>");

            return builder.ToString();
        }
    }
}
