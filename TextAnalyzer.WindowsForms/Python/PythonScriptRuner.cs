using System.Diagnostics;

namespace TextAnalyzer.WindowsForms;

public class PythonScriptRunner
{
    private readonly string _scriptPath;
    private readonly string _pythonPath;

    public PythonScriptRunner(string scriptPath, string pythonPath)
    {
        _scriptPath = scriptPath;
        _pythonPath = pythonPath;
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
