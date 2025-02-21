using System.Windows.Forms;

namespace TextAnalyzer.WindowsForms.FormElements;

public delegate void WordSelectedEventHandler( string word, string lemma, string morphInfo, string syntaxRole);


internal class LexemeGridHandler
{
    private readonly DataGridView _dataGridView;

    public event WordSelectedEventHandler WordSelected;

    public LexemeGridHandler(DataGridView dataGridView)
    {
        _dataGridView = dataGridView;


        _dataGridView.AllowUserToResizeRows = false;
        _dataGridView.AllowUserToResizeColumns = false;
        _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        _dataGridView.AllowUserToAddRows = false;
        _dataGridView.AllowUserToDeleteRows = false;
        _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dataGridView.MultiSelect = false;
        _dataGridView.ReadOnly = true;
        _dataGridView.RowHeadersVisible = false;

        _dataGridView.CellClick += CellClick;

    }

    public  void SubscribeToWordSelected(WordSelectedEventHandler handler)
    {
        WordSelected += handler;
    }

    public void ConfigureColumsHeaders()
    {
        // Устанавливаем имена для столбцов
        _dataGridView.Columns["word"].HeaderText = "Слово";
        _dataGridView.Columns["lemma"].HeaderText = "Лексема";
        _dataGridView.Columns["syntax_role"].HeaderText = "Член предложения";
        _dataGridView.Columns["morph_info"].HeaderText = "Описание";
    }

    private void CellClick(object sender, DataGridViewCellEventArgs e)
    {
        // Получаем выделенную строку
        if (_dataGridView.SelectedRows.Count > 0)
        {
            DataGridViewRow selectedRow = _dataGridView.SelectedRows[0];

            // Получаем данные из строки
            string word = selectedRow.Cells["word"].Value.ToString();
            string lemma = selectedRow.Cells["lemma"].Value.ToString();
            string syntaxRole = selectedRow.Cells["syntax_role"].Value.ToString();
            string mordInfo = selectedRow.Cells["morph_info"].Value.ToString();



            WordSelected?.Invoke( word, lemma, mordInfo, syntaxRole);
           
        }
    }
}
