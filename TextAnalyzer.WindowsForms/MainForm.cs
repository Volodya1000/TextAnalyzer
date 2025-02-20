using TextAnalyzer.WindowsForms.FormElements;
using System.Text.Json;
using TextAnalyzer.WindowsForms.DataBase;

namespace TextAnalyzer.WindowsForms;

public partial class MainForm : Form
{
    private readonly LexemeGridHandler _gridHandler;
    private readonly PythonScriptRunner pythonScriptRunner;
    private WordInfoList wordInfoList;

    public MainForm()
    {
        InitializeComponent();

        // Разворачиваем форму на весь экран
        this.WindowState = FormWindowState.Maximized;

        // Устанавливаем режим автоматического изменения размеров для формы
        this.AutoScaleMode = AutoScaleMode.Font;


        string workingDirectory = Environment.CurrentDirectory;
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        string pythonScriptPath = projectDirectory + "\\Python\\TextAnalizerScript.py";
        string p1 = "C:\\Users\\Volodya\\Documents\\C#\\LanguageInterfaces\\TextAnalyzer\\TextAnalyzer.WindowsForms\\Python\\TextAnalizerScript.py";

        string pythonPath = "C:\\Users\\Volodya\\AppData\\Local\\Programs\\Python\\Python312\\python.exe";// "C:\\Python312\\python.exe";
        pythonScriptRunner = new(pythonScriptPath, pythonPath);

        _gridHandler = new LexemeGridHandler(dataGridViewLexemes);

        //ExecuteText("Маленький мальчик идёт домой со школы");

    }

    private void BTN_SelectPDF_File_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            openFileDialog.Title = "Выберите PDF файл";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string pdfFilePath = openFileDialog.FileName;
                ExecuteText($"\"{pdfFilePath}\"");
            }
        }
    }

    private void ExecuteText(string text)
    {
        string resultJson = pythonScriptRunner.ExecuteScript(true,text);

         wordInfoList = WordInfoList.FromJson(resultJson);

        _gridHandler.LoadData(wordInfoList);
    }

    private void AddWordButton_Click(object sender, EventArgs e)
    {
        string word = NewWordTextBox.Text;
        string resultJson = pythonScriptRunner.ExecuteScript(false,word);
        List<WordInfo> newWordInfo = JsonSerializer.Deserialize<List<WordInfo>>(resultJson);
        wordInfoList.AddWord(newWordInfo[0]);
        _gridHandler.LoadData(wordInfoList);
    }
}
