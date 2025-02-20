using System.Windows.Forms;
using TextAnalyzer.WindowsForms.FormElements;
namespace TextAnalyzer.WindowsForms;

public partial class WordsTableForm : Form
{
    private DataGridView dataGridViewLexemes;
    private RowAdder rowAdder;
    private CustomMenuStrip customMenuStrip;

    private readonly LexemeGridHandler _gridHandler;

    public WordsTableForm()
    {
        InitializeComponent();

        this.WindowState = FormWindowState.Maximized;

        // Устанавливаем режим автоматического изменения размеров для формы
        this.AutoScaleMode = AutoScaleMode.Font;

        // Инициализация DataGridView
        dataGridViewLexemes = new DataGridView();
        _gridHandler = new LexemeGridHandler(dataGridViewLexemes);
        dataGridViewLexemes.Dock = DockStyle.Fill; // Растягиваем на оставшуюся часть формы

        // Инициализация RowAdder и установка DataGridView
        rowAdder = new RowAdder ();
        rowAdder.Dock = DockStyle.Fill; // Растягиваем на оставшуюся часть своей панели

        // Создаем TableLayoutPanel для размещения RowAdder и DataGridView
        TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
        tableLayoutPanel.Dock = DockStyle.Fill;
        tableLayoutPanel.ColumnCount = 2; // Две колонки
        tableLayoutPanel.RowCount = 1; // Одна строка

        // Устанавливаем ширину колонок
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11F)); // 30% ширины для RowAdder
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 89F)); // 70% ширины для DataGridView

        // Добавляем элементы в TableLayoutPanel
        tableLayoutPanel.Controls.Add(rowAdder, 0, 0); // RowAdder в первой колонке
        tableLayoutPanel.Controls.Add(dataGridViewLexemes, 1, 0); // DataGridView во второй колонке

        // Добавляем TableLayoutPanel на форму
        this.Controls.Add(tableLayoutPanel);


        customMenuStrip = new CustomMenuStrip();

        // Добавляем MenuStrip на форму
        this.MainMenuStrip = customMenuStrip.GetMenuStrip();
        this.Controls.Add(customMenuStrip.GetMenuStrip());



        WordInfoDataBase.SubscribeToWordsChanged(UpdateDataGridView);
    }

    private void UpdateDataGridView(object sender, EventArgs e)
    {
        // Получаем отсортированный список слов и отображаем его в DataGridView
        dataGridViewLexemes.DataSource = null; // Сбрасываем источник данных
        dataGridViewLexemes.DataSource = WordInfoDataBase.GetInstance().GetSortedWords(); // Получаем данные из Singleton
    }
}
