using TextAnalyzer.WindowsForms.Services;

namespace TextAnalyzer.WindowsForms.FormElements;

internal class CustomMenuStrip
{
    private MenuStrip menuStrip;
    private string currentFilePath = null; // Храним путь к текущему файлу

    public CustomMenuStrip()
    {
        menuStrip = new MenuStrip();

        // Создание пункта "Файл"
        ToolStripMenuItem fileMenuItem = new ToolStripMenuItem("Файл");
        ToolStripMenuItem helpMenuItem = new ToolStripMenuItem("Справка");

        helpMenuItem.Click += Info_Click;

        // Создание подпунктов
        ToolStripMenuItem loadDictionaryItem = new ToolStripMenuItem("Загрузить словарь");
        ToolStripMenuItem saveItem = new ToolStripMenuItem("Сохранить");
        ToolStripMenuItem saveAsItem = new ToolStripMenuItem("Сохранить как...");
        ToolStripMenuItem createFromPdfItem = new ToolStripMenuItem("Создать словарь из PDF");

        // Добавление обработчиков событий
        loadDictionaryItem.Click += LoadDictionary_Click;
        saveItem.Click += Save_Click;
        saveAsItem.Click += SaveAs_Click;
        createFromPdfItem.Click += CreateFromPdf_Click;

        // Добавление подпунктов в пункт "Файл"
        fileMenuItem.DropDownItems.Add(loadDictionaryItem);
        fileMenuItem.DropDownItems.Add(saveItem);
        fileMenuItem.DropDownItems.Add(saveAsItem);
        fileMenuItem.DropDownItems.Add(createFromPdfItem);

        // Добавление пункта "Файл" в меню
        menuStrip.Items.Add(fileMenuItem);

        menuStrip.Items.Add(helpMenuItem);

        // Изначально кнопка "Сохранить" должна быть недоступна, если словарь не загружен
        saveItem.Enabled = false;
    }

    private void Info_Click(object sender, EventArgs e)
    {
        string text = @"{\rtf1\ansi\ansicpg1251\deff0\nouicompat
{\fonttbl{\f0\fnil\fcharset204 Tahoma;}}
{\colortbl ;\red0\green0\blue255;\red0\green128\blue0;}
\viewkind4\uc1 
\pard\sa200\sl276\slmult1\f0\fs28\b Программа для анализа PDF файлов\b0\par
\pard\sa200\sl276\slmult1\f0\fs24 Данная программа позволяет проанализировать PDF файл с текстом на русском языке. Программа использует скрипт на Python, в котором \i синтаксическая роль слов \i0 и \i морфологическая информация \i0 определяются с помощью библиотеки \b Natasha\b0 . Чтение PDF файлов осуществляется также внутри Python-скрипта. Сформированный словарь можно сохранить в формате \b JSON\b0 , есть возможность загрузить готовый словарь либо отредактировать существующий.\par
В программе реализован поиск по началу слова. Есть возможность отредактировать все поля с информацией о любом слове и сохранить результат. Также можно удалить нужное слово.\par

\b Характеристики и их значения\b0\par

\b Падеж (Case)\b0\par
\tab \i Именительный падеж (Nom)\i0\par
\tab \i Родительный падеж (Gen)\i0\par
\tab \i Дательный падеж (Dat)\i0\par
\tab \i Винительный падеж (Acc)\i0\par
\tab \i Творительный падеж (Ins)\i0\par
\tab \i Предложный падеж (Loc)\i0\par

\b Число (Number)\b0\par
\tab \i Единственное число (Sing)\i0\par
\tab \i Множественное число (Plur)\i0\par

\b Род (Gender)\b0\par
\tab \i Мужской род (Masc)\i0\par
\tab \i Женский род (Fem)\i0\par
\tab \i Средний род (Neut)\i0\par

\b Время (Tense)\b0\par
\tab \i Прошедшее время (Past)\i0\par
\tab \i Настоящее время (Pres)\i0\par
\tab \i Будущее время (Fut)\i0\par

\b Вид (Aspect)\b0\par
\tab \i Несовершенный вид (Imp)\i0\par
\tab \i Совершенный вид (Perf)\i0\par

\b Наклонение (Mood)\b0\par
\tab \i Изъявительное наклонение (Ind)\i0\par
\tab \i Повелительное наклонение (Imp)\i0\par

\b Форма глагола (VerbForm)\b0\par
\tab \i Личная форма (Fin)\i0\par
\tab \i Инфинитив (Inf)\i0\par
\tab \i Причастие (Part)\i0\par
\tab \i Деепричастие (Ger)\i0\par

\b Лицо (Person)\b0\par
\tab \i 1-е лицо (1)\i0\par
\tab \i 2-е лицо (2)\i0\par
\tab \i 3-е лицо (3)\i0\par

\b Одушевленность (Animacy)\b0\par
\tab \i Одушевленный (Anim)\i0\par
\tab \i Неодушевленный (Inan)\i0\par

\b Залог (Voice)\b0\par
\tab \i Действительный залог (Act)\i0\par
\tab \i Страдательный залог (Pass)\i0\par
\tab \i Средний залог (Mid)\i0\par

\b Степень сравнения (Degree)\b0\par
\tab \i Положительная степень (Pos)\i0\par
\tab \i Сравнительная степень (Cmp)\i0\par
\tab \i Превосходная степень (Sup)\i0\par

\b Полярность (Polarity) \b0 
\tab \i Отрицательная полярность (Neg) \b\b0 
}

";
        DialogResult result = CustomMessageBoxForm.Show("Заголовок", text);
    }


    private void LoadDictionary_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "JSON Files (*.json)|*.json";
            openFileDialog.Title = "Выберите JSON файл";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string jsonFilePath = openFileDialog.FileName;
                try
                {
                    JsonFileHandler.LoadFromFile(jsonFilePath);
                    currentFilePath = jsonFilePath; // Запоминаем путь к загруженному файлу
                    EnableSaveButton();
                }
                catch
                {
                    MessageBox.Show($"Данный файл не соответствует требуемому формату JSON: ", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    currentFilePath = null;
                    DisableSaveButton();
                }
            }
            else
            {
                DisableSaveButton();
            }
        }
    }

    private void Save_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(currentFilePath))
        {
            // Если путь к файлу не установлен, вызываем "Сохранить как..."
            SaveAs_Click(sender, e);
        }
        else
        {
            // Сохраняем в текущий файл
            try
            {
                JsonFileHandler.SaveToFile(currentFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении в файл: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void SaveAs_Click(object sender, EventArgs e)
    {
        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
        {
            // Настройка диалога сохранения файла
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.Title = "Сохранить файл как";
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.AddExtension = true;

            // Показать диалог и проверить результат
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Получаем выбранный путь и имя файла
                string filePath = saveFileDialog.FileName;

                try
                {
                    JsonFileHandler.SaveToFile(filePath);
                    currentFilePath = filePath; // Запоминаем новый путь к файлу
                    EnableSaveButton();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении в файл: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DisableSaveButton();
                }
            }
            else
            {
                DisableSaveButton();
            }
        }
    }

    private void CreateFromPdf_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(currentFilePath))
        {
            // Предлагаем сохранить изменения перед перезаписью базы данных
            var result = MessageBox.Show("Вы хотите сохранить изменения перед созданием нового словаря?",
                                          "Подтверждение",
                                          MessageBoxButtons.YesNoCancel,
                                          MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Save_Click(sender, e); // Сохраняем текущий файл
            }
            else if (result == DialogResult.Cancel)
            {
                return; // Отменяем операцию создания словаря из PDF
            }

            // Очищаем имя текущего файла перед перезаписью данных из PDF
            currentFilePath = null;
        }

        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            openFileDialog.Title = "Выберите PDF файл";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string pdfFilePath = openFileDialog.FileName;
                ExecuteText($"\"{pdfFilePath}\"");
                EnableSaveButton(); // Активируем кнопку сохранения после создания нового словаря из PDF
            }
            else
            {
                DisableSaveButton();
            }
        }
    }

    private void ExecuteText(string fullPath)
    {
        var scryptResult = PythonScriptRunner.GetInstance().ExecuteScript(true, fullPath);

        string fileName = Path.GetFileName(fullPath);
        MessageBox.Show($"Скрипт анализа для текста { fileName.Substring(0, fileName.Length - 1) } выполнялся { scryptResult.scriptRunTime} милисекунд", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        string tempPath = "C:\\Users\\Volodya\\Documents\\C#\\LanguageInterfaces\\TextAnalyzer\\TextAnalyzer.WindowsForms\\Python\\temp.json";
        JsonFileHandler.LoadFromFile(tempPath);

        JsonFileHandler.DeleteFile(tempPath);
    }

    public MenuStrip GetMenuStrip()
    {
        return menuStrip;
    }

    private void EnableSaveButton()
    {
        foreach (ToolStripItem item in menuStrip.Items)
        {
            if (item is ToolStripMenuItem menuItem)
            {
                foreach (ToolStripItem dropDownItem in menuItem.DropDownItems)
                {
                    if (dropDownItem is ToolStripMenuItem saveMenuItem && saveMenuItem.Text == "Сохранить")
                    {
                        saveMenuItem.Enabled = true;
                        return;
                    }
                }
            }
        }
    }

    private void DisableSaveButton()
    {
        foreach (ToolStripItem item in menuStrip.Items)
        {
            if (item is ToolStripMenuItem menuItem)
            {
                foreach (ToolStripItem dropDownItem in menuItem.DropDownItems)
                {
                    if (dropDownItem is ToolStripMenuItem saveMenuItem && saveMenuItem.Text == "Сохранить")
                    {
                        saveMenuItem.Enabled = false;
                        return;
                    }
                }
            }
        }
    }
}


