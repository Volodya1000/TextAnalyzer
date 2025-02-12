namespace TextAnalyzer.WindowsForms.FormElements;
using TextAnalyzer.Lib;

internal class LexemeGridHandler
{
    private readonly DataGridView _dataGridView;

    public LexemeGridHandler(DataGridView dataGridView)
    {
        _dataGridView = dataGridView;
        

    }

    public void LoadData(IList<Lexeme> lexemes)
    {
        // Преобразуем данные для пользовательского отображения
        var displayData = lexemes.Select(lexeme => new
        {
            Value = lexeme.Value,
            SentenceMember = lexeme.SentenceMember?.GetRussianTranslation() ?? "Неизвестно",
            PartOfSpeech = lexeme.PartOfSpeech.GetRussianTranslation(),
            FormDescription = lexeme.FormDescription
        }).ToList();

        // Привязываем данные к DataGridView
        _dataGridView.DataSource = displayData;

        // Установка заголовков
        _dataGridView.Columns["Value"].HeaderText = "Лексема";
        _dataGridView.Columns["SentenceMember"].HeaderText = "Член предложения";
        _dataGridView.Columns["PartOfSpeech"].HeaderText = "Часть речи";
        _dataGridView.Columns["FormDescription"].HeaderText = "Описание";
        ConfigureUI();
    }


    private void ConfigureUI()
    {
        _dataGridView.AllowUserToResizeRows = false;
        _dataGridView.AllowUserToResizeColumns = false;
        _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dataGridView.AllowUserToAddRows = false;
        _dataGridView.AllowUserToDeleteRows = false;
        _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dataGridView.MultiSelect = false;
        _dataGridView.ReadOnly = true;
        _dataGridView.RowHeadersVisible = false;

    }

}
