using System;
using System.Windows.Forms;

namespace TextAnalyzer.WindowsForms.FormElements;

internal class CustomMenuStrip
{
    private MenuStrip menuStrip;

    public CustomMenuStrip()
    {
        menuStrip = new MenuStrip();

        // Создание пункта "Файл"
        ToolStripMenuItem fileMenuItem = new ToolStripMenuItem("Файл");

        // Создание подпунктов
        ToolStripMenuItem loadDictionaryItem = new ("Загрузить словарь");
        ToolStripMenuItem saveItem = new ("Сохранить");
        ToolStripMenuItem createFromPdfItem = new ("Создать словарь из PDF");

        // Добавление обработчиков событий (пока пустые методы)
        loadDictionaryItem.Click += LoadDictionary_Click;
        saveItem.Click += Save_Click;
        createFromPdfItem.Click += CreateFromPdf_Click;

        // Добавление подпунктов в пункт "Файл"
        fileMenuItem.DropDownItems.Add(loadDictionaryItem);
        fileMenuItem.DropDownItems.Add(saveItem);
        fileMenuItem.DropDownItems.Add(createFromPdfItem);

        // Добавление пункта "Файл" в меню
        menuStrip.Items.Add(fileMenuItem);
    }

    private void LoadDictionary_Click(object sender, EventArgs e)
    {
        // Логика загрузки словаря
    }

    private void Save_Click(object sender, EventArgs e)
    {
        // Логика сохранения
    }

    private void CreateFromPdf_Click(object sender, EventArgs e)
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
        string resultJson = PythonScriptRunner.GetInstance().ExecuteScript(true, text);
        WordInfoDataBase.FromJson(resultJson);
    }

    public MenuStrip GetMenuStrip()
    {
        return menuStrip;
    }
}
