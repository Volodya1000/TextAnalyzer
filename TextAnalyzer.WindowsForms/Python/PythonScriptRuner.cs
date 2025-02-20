using System.Diagnostics;

namespace TextAnalyzer.WindowsForms;

public class PythonScriptRunner
{
    private static PythonScriptRunner _instance;
    private static readonly object _lock = new object();

    private readonly string _scriptPath;
    private readonly string _pythonPath;

    private PythonScriptRunner()
    {
        string workingDirectory = Environment.CurrentDirectory;
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        string pythonScriptPath = projectDirectory + "\\Python\\TextAnalizerScript.py";

        _scriptPath = pythonScriptPath;

        _pythonPath = "C:\\Users\\Volodya\\AppData\\Local\\Programs\\Python\\Python312\\python.exe";// "C:\\Python312\\python.exe";

    }

    public static PythonScriptRunner GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new PythonScriptRunner();
                }
            }
        }
        return _instance;
    }

    public string ExecuteScript(bool fromPdf,string inputText)
    {
        ProcessStartInfo start = new ProcessStartInfo
        {
            FileName = _pythonPath,
            Arguments = $"{_scriptPath} {(fromPdf?1:2)} {inputText}",
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(start))
        {
           
            // Чтение вывода скрипта
            using (StreamReader reader = process.StandardOutput)
            {
                return reader.ReadToEnd();
            }
        }
    }

    public string GetErrors()
    {
        ProcessStartInfo start = new ProcessStartInfo
        {
            FileName = _pythonPath,
            Arguments = $"{_scriptPath}",
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(start))
        {
            using (StreamReader errorReader = process.StandardError)
            {
                return errorReader.ReadToEnd();
            }
        }
    }
}
