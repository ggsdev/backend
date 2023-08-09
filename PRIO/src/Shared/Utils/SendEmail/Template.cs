using PRIO.src.Modules.FileImport.XML.Dtos;

namespace PRIO.src.Shared.Utils.SendEmail
{
    public static class Template
    {
        public static string GenerateNotificationEmail(Client039DTO nfsm)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Correção de Valores - Notificação</title>
                </head>
                <body>
                    <h2>Notificação de Falha</h2>
                  
                    <ul>
                        <li>Ponto de medição: {nfsm.COD_TAG_PONTO_MEDICAO_039}</li>
                        <li>Equipamento: {nfsm.COD_TAG_EQUIPAMENTO_039}</li>
                        <li>Tipo de Falha: {nfsm.DSC_TIPO_FALHA_039} – Código e descrição do tipo de falha conforme tabela TIPO FALHA</li>
                        <li>Código da Falha: {nfsm.COD_FALHA_039} – Código interno da falha gerada.</li>
                        <li>Data da ocorrência: {nfsm.DHA_OCORRENCIA_039} – Data e hora em que a falha ocorreu</li>
                        <li>Data da detecção: {nfsm.DHA_DETECCAO_039} – Data e hora que a falha foi detectada</li>
                        <li>Data do retorno: {nfsm.DHA_RETORNO_039} – Data e hora em que a falha foi corrigida</li>
                        <li>Descrição da Falha: {nfsm.DHA_DSC_FALHA_039} Descrição da falha ocorrida</li>
                        <li>Ação: {nfsm.DHA_DSC_ACAO_039} Ações tomadas</li>
                        <li>Metodologia: {nfsm.DHA_DSC_METODOLOGIA_039} Descrição da metodologia aplicada.</li>
                    </ul>
                </body>
                </html>
            ";
        }
    }
}
