namespace TextAnalyzer.WindowsForms;

using System.Text.Json;
using System.Windows.Forms;
using static iText.IO.Codec.TiffWriter;


public class LeftPanelController : UserControl
{
    private TextBox txtWord;
    private TextBox txtLemma;
    private TextBox txtMorphInfo;
    private TextBox txtSyntaxRole;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnRemove;
    private Button btnStrictSearch;
    private TextBox txtSearch;
    private Button btnSearch;

    private FlowLayoutPanel flowPanel;

    private WordInfo currentWordInfo;


    public LeftPanelController()
    {

        // Инициализация элементов управления TextBox
        txtWord = new TextBox { Width = 190 };
        txtLemma = new TextBox { Width = 190 };
        txtMorphInfo = new TextBox { Width = 190 };
        txtSyntaxRole = new TextBox { Width = 190 };
        txtSearch = new TextBox { Width = 190 };
        txtSearch.TextChanged += SearchTextBox_TextChanged;

        // Создание Label для каждого TextBox
        Label lblWord = new Label { Text = "Слово:                       ", AutoSize = true };
        Label lblLemma = new Label { Text = "Лемма:                       ", AutoSize = true };
        Label lblMorphInfo = new Label { Text = "Морфологическая информация:", AutoSize = true };
        Label lblSyntaxRole = new Label { Text = "Синтаксическая роль:", AutoSize = true };
        Label lblSearch = new Label { Text = "Фильтрация по слову:", AutoSize = true };



        // Инициализация кнопки
        btnAdd = new Button { Text = "Добавить", AutoSize = true };
        btnAdd.Click += BtnAdd_Click;

        btnEdit = new Button { Text = "Изменить", AutoSize = true };
        btnEdit.Click += BtnEdit_Click;

        btnRemove = new Button { Text = "Удалить", AutoSize = true };
        btnRemove.Click += BtnDelete_Click;

        btnSearch = new Button { Text = "Фильтрация", AutoSize = true };
        btnSearch.Click += BtnSearch_Click;

        btnStrictSearch = new Button { Text = "Поиск (полное совпадение слова)", AutoSize = true };
        btnStrictSearch.Click += BtnStrictSearch_Click;

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

        FlowLayoutPanel lemmaPanel = new FlowLayoutPanel { AutoSize = true };
        wordPanel.Controls.Add(lblLemma);
        wordPanel.Controls.Add(txtLemma);

        FlowLayoutPanel morphInfoPanel = new FlowLayoutPanel { AutoSize = true };
        morphInfoPanel.Controls.Add(lblMorphInfo);
        morphInfoPanel.Controls.Add(txtMorphInfo);

        FlowLayoutPanel syntaxRolePanel = new FlowLayoutPanel { AutoSize = true };
        syntaxRolePanel.Controls.Add(lblSyntaxRole);
        syntaxRolePanel.Controls.Add(txtSyntaxRole);

        FlowLayoutPanel searchPanel = new FlowLayoutPanel { AutoSize = true };
        searchPanel.Controls.Add(lblSearch);
        searchPanel.Controls.Add(txtSearch);
        searchPanel.Controls.Add(btnSearch);


        // Добавление элементов на панель
        flowPanel.Controls.Add(wordPanel);
        flowPanel.Controls.Add(morphInfoPanel);
        flowPanel.Controls.Add(syntaxRolePanel);

        flowPanel.Controls.Add(btnAdd);
        flowPanel.Controls.Add(btnEdit);
        flowPanel.Controls.Add(btnRemove);
        flowPanel.Controls.Add(btnStrictSearch);

        flowPanel.Controls.Add(searchPanel);

        
        // Добавление панели на UserControl
        this.Controls.Add(flowPanel);

    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
        // Проверка на пустые значения с указанием поля
        if (!ValidateField(txtWord, "Слово")) return;
        if (!ValidateField(txtMorphInfo, "Морфологическая информация")) return;
        if (!ValidateField(txtSyntaxRole, "Синтаксическая роль")) return;

        string lemma=txtLemma.Text!=""?txtLemma.Text: GetWordInfo().lemma;

        WordInfo newWord=new WordInfo(txtWord.Text, lemma, txtMorphInfo.Text, txtSyntaxRole.Text);
        WordInfoStorage.GetInstance().AddWord(newWord);

        // Очистка полей ввода после добавления
        txtWord.Clear();
        txtLemma.Clear();
        txtMorphInfo.Clear();
        txtSyntaxRole.Clear();
    }

    private WordInfo GetWordInfo()
    {
        string word = txtWord.Text;
        string resultJson = PythonScriptRunner.GetInstance().ExecuteScript(false, word).result;
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

    public void SetTextBoxes(string word, string lemma, string morphInfo, string syntaxRole)
    {
        txtWord.Text = word;
        txtLemma.Text=lemma;
        txtMorphInfo.Text = morphInfo;
        txtSyntaxRole.Text = syntaxRole;
        currentWordInfo = new WordInfo(word, lemma, morphInfo, syntaxRole);
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        // Проверка на пустые значения с указанием поля
        if (!ValidateField(txtWord, "Слово")) return;
        if (!ValidateField(txtLemma, "Лексема")) return;
        if (!ValidateField(txtMorphInfo, "Морфологическая информация")) return;
        if (!ValidateField(txtSyntaxRole, "Синтаксическая роль")) return;

        WordInfo newWord = new WordInfo(txtWord.Text, txtLemma.Text, txtMorphInfo.Text, txtSyntaxRole.Text);

        if (newWord.Equals( currentWordInfo))
        {
            MessageBox.Show($"Данные не были изменены.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        WordInfoStorage.GetInstance().UpdateWord(currentWordInfo,newWord);

        // Очистка полей ввода после добавления
        txtWord.Clear();
        txtLemma.Clear();
        txtMorphInfo.Clear();
        txtSyntaxRole.Clear();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        // Удаляем данные из базы данных
        WordInfoStorage.GetInstance().RemoveWord(txtWord.Text, txtLemma.Text, txtMorphInfo.Text, txtSyntaxRole.Text);

        txtWord.Clear();
        txtLemma.Clear();
        txtMorphInfo.Clear();
        txtSyntaxRole.Clear();
    }

    private void BtnSearch_Click(object sender, EventArgs e)
    {

        WordInfo filterWord = new WordInfo(txtWord.Text, txtLemma.Text, txtMorphInfo.Text, txtSyntaxRole.Text);
        WordInfoStorage.GetInstance().FilterWord = filterWord;
    }


    private void SearchTextBox_TextChanged(object sender, EventArgs e)
    {

        WordInfoStorage.GetInstance().SearchKey = txtSearch.Text;

    }


    private void BtnStrictSearch_Click(object sender, EventArgs e)
    {
        WordInfoStorage.GetInstance().StrictSearch = true;



    }
    

}
