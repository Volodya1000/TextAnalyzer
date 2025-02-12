using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace TextAnalyzer.WindowsForms;

public class PdfReaderService
{
    public static string ExtractTextFromPdf(string pdfFilePath)
    {
        string result = string.Empty;

        using (PdfReader pdfReader = new PdfReader(pdfFilePath))
        using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
        {
            for (int page = 1; page <= pdfDocument.GetNumberOfPages(); page++)
            {
                result += PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(page)) + Environment.NewLine;
            }
        }

        return result;
    }
}
