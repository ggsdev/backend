using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using PRIO.src.Modules.FileImport.XML.Dtos;

namespace PRIO.src.Shared.Utils
{
    public static class Download
    {
        public static FileContentResponse DownloadErrors(List<string> errors)
        {

            using var memoryStream = new MemoryStream();

            var pdfDoc = new PdfDocument(new PdfWriter(memoryStream));

            var document = new Document(pdfDoc);

            var titleParagraph = new Paragraph("< Erros Importação >")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);
            document.Add(titleParagraph);

            foreach (string error in errors)
            {
                var listItem = new ListItem("=> " + error)
                    .SetMarginBottom(10);
                document.Add(listItem);
            }

            document.Close();

            byte[] pdfBytes = memoryStream.ToArray();

            var response = new FileContentResponse
            {
                ContentBase64 = Convert.ToBase64String(pdfBytes)
            };

            return response;
        }
    }
}
