namespace TextAnalyzer.WindowsForms.FormElements;

internal class LexemeGridHandler
{
    private readonly DataGridView _dataGridView;

    public LexemeGridHandler(DataGridView dataGridView)
    {
        _dataGridView = dataGridView;

        // Определяем столбцы и их подписи вверху
        _dataGridView.Columns.Add("word", "Слово");
        _dataGridView.Columns.Add("lemma", "Лексема");
        _dataGridView.Columns.Add("syntax_role", "Член предложения");
        _dataGridView.Columns.Add("morph_info", "Описание");

        // Устанавливаем имена для столбцов
        _dataGridView.Columns["word"].DataPropertyName = "word";
        _dataGridView.Columns["lemma"].DataPropertyName = "lemma";
        _dataGridView.Columns["syntax_role"].DataPropertyName = "syntax_role";
        _dataGridView.Columns["morph_info"].DataPropertyName = "morph_info";
        //_dataGridView.AutoGenerateColumns = false;
        ConfigureUI();
    }

    private void ConfigureUI()
    {
        _dataGridView.AllowUserToResizeRows = false;
        _dataGridView.AllowUserToResizeColumns = false;
        _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        _dataGridView.AllowUserToAddRows = false;
        _dataGridView.AllowUserToDeleteRows = false;
        _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dataGridView.MultiSelect = false;
        _dataGridView.ReadOnly = true;
        _dataGridView.RowHeadersVisible = false;
    }
}
