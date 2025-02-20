namespace TextAnalyzer.WindowsForms;

using System.Text.Json;
using System.Windows.Forms;


public class RowAdder : UserControl
{
    private TextBox txtWord;
    private TextBox txtMorphInfo;
    private TextBox txtSyntaxRole;
    private Button btnAdd;
    private FlowLayoutPanel flowPanel;

    public RowAdder()
    {
        // Инициализация элементов управления TextBox
        txtWord = new TextBox { Width = 190 };
        txtMorphInfo = new TextBox { Width = 190 };
        txtSyntaxRole = new TextBox { Width = 190 };

        // Создание Label для каждого TextBox
        Label lblWord = new Label { Text = "Слово:                       ", AutoSize = true };
        Label lblMorphInfo = new Label { Text = "Морфологическая информация:", AutoSize = true };
        Label lblSyntaxRole = new Label { Text = "Синтаксическая роль:", AutoSize = true };

        // Инициализация кнопки
        btnAdd = new Button { Text = "Добавить", AutoSize = true };
        btnAdd.Click += BtnAdd_Click;

        // Создание FlowLayoutPanel для управления расположением элементов
        flowPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            FlowDirection = FlowDirection.TopDown // Размещение элементов сверху вниз
        };

        FlowLayoutPanel wordPanel = new FlowLayoutPanel { AutoSize = true };
        wordPanel.Controls.Add(lblWord);
        wordPanel.Controls.Add(txtWord);

        FlowLayoutPanel morphInfoPanel = new FlowLayoutPanel { AutoSize = true };
        morphInfoPanel.Controls.Add(lblMorphInfo);
        morphInfoPanel.Controls.Add(txtMorphInfo);

        FlowLayoutPanel syntaxRolePanel = new FlowLayoutPanel { AutoSize = true };
        syntaxRolePanel.Controls.Add(lblSyntaxRole);
        syntaxRolePanel.Controls.Add(txtSyntaxRole);

        // Добавление элементов на панель
        flowPanel.Controls.Add(wordPanel);
        flowPanel.Controls.Add(morphInfoPanel);
        flowPanel.Controls.Add(syntaxRolePanel);
        flowPanel.Controls.Add(btnAdd);

        // Добавление панели на UserControl
        this.Controls.Add(flowPanel);

    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
        // Проверка на пустые значения с указанием поля
        if (!ValidateField(txtWord, "Слово")) return;
        if (!ValidateField(txtMorphInfo, "Морфологическая информация")) return;
        if (!ValidateField(txtSyntaxRole, "Синтаксическая роль")) return;

        WordInfo newWord=new WordInfo(txtWord.Text, GetWordInfo().lemma, txtMorphInfo.Text, txtSyntaxRole.Text);
        WordInfoDataBase.GetInstance().AddWord(newWord);






        // Очистка полей ввода после добавления
        txtWord.Clear();
        txtMorphInfo.Clear();
        txtSyntaxRole.Clear();
    }

    private WordInfo GetWordInfo()
    {
        string word = txtWord.Text;
        string resultJson = PythonScriptRunner.GetInstance().ExecuteScript(false, word);
        return JsonSerializer.Deserialize<List<WordInfo>>(resultJson)[0];
    }
    


    private bool ValidateField(TextBox textBox, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            MessageBox.Show($"Поле '{fieldName}' не должно быть пустым.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }

}
