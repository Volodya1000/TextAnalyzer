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

    public (string result,long scriptRunTime)ExecuteScript(bool fromPdf, string inputText)
    {
        // Создаем экземпляр Stopwatch для замера времени
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); // Запускаем таймер

        ProcessStartInfo start = new ProcessStartInfo
        {
            FileName = _pythonPath,
            Arguments = $"{_scriptPath} {(fromPdf ? 1 : 2)} {inputText}",
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
                string output = reader.ReadToEnd();
                stopwatch.Stop(); // Останавливаем таймер


                return (output, stopwatch.ElapsedMilliseconds);
            }
        }
    }
}