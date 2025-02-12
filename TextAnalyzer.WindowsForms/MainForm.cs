using TextAnalyzer.Lib;
using TextAnalyzer.WindowsForms.FormElements;

namespace TextAnalyzer.WindowsForms;

public partial class MainForm : Form
{
    private readonly LexemeGridHandler _gridHandler;


    public MainForm()
    {
        InitializeComponent();

        TextAnalyzerFacade.Initialize();

        _gridHandler = new LexemeGridHandler(dataGridViewLexemes);

        LoadLexemes();

    }

    private void BTN_SelectPDF_File_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            openFileDialog.Title = "�������� PDF ����";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string pdfFilePath = openFileDialog.FileName;
                string pdfText = PdfReaderService.ExtractTextFromPdf(pdfFilePath);
                MessageBox.Show(pdfText, "���������� PDF");
            }
        }
    }

    private void LoadLexemes()
    {
        IList<Lexeme> lexemes = TextAnalyzerFacade.Execute("��������� ������� ��� �����");
        _gridHandler.LoadData(lexemes);
    }
}
