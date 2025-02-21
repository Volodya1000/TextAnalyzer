using System.Windows.Forms;

namespace TextAnalyzer.WindowsForms;
public  class CustomMessageBoxForm : Form
{
    private RichTextBox richTextBox1;

    public CustomMessageBoxForm(string title, string message)
    {
        

        // Устанавливаем состояние окна на Maximized
        this.WindowState = FormWindowState.Maximized;
        // Устанавливаем заголовок формы
        this.Text = title;
        // Инициализируем RichTextBox
        richTextBox1 = new RichTextBox
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            BackColor = Color.White,
            BorderStyle = BorderStyle.None
        };
        this.Controls.Add(richTextBox1);

        // Устанавливаем RTF текст
        richTextBox1.Rtf = message; // предполагаем что message - это RTF форматированный текст

        // Создаем кнопку "OK"
        Button okButton = new Button
        {
            Text = "OK",
            DialogResult = DialogResult.OK,
            Dock = DockStyle.Bottom
        };
        okButton.Click += (sender, e) => this.Close();
        this.Controls.Add(okButton);
    }

    public static DialogResult Show(string title, string message)
    {
        CustomMessageBoxForm form = new CustomMessageBoxForm(title, message);
        return form.ShowDialog();
    }
}

