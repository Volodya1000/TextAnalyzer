namespace TextAnalyzer.WindowsForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BTN_SelectPDF_File = new Button();
            dataGridViewLexemes = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLexemes).BeginInit();
            SuspendLayout();
            // 
            // BTN_SelectPDF_File
            // 
            BTN_SelectPDF_File.Location = new Point(12, 12);
            BTN_SelectPDF_File.Name = "BTN_SelectPDF_File";
            BTN_SelectPDF_File.Size = new Size(185, 29);
            BTN_SelectPDF_File.TabIndex = 0;
            BTN_SelectPDF_File.Text = "Выбрать pdf файл";
            BTN_SelectPDF_File.UseVisualStyleBackColor = true;
            BTN_SelectPDF_File.Click += BTN_SelectPDF_File_Click;
            // 
            // dataGridViewLexemes
            // 
            dataGridViewLexemes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewLexemes.Location = new Point(12, 47);
            dataGridViewLexemes.Name = "dataGridViewLexemes";
            dataGridViewLexemes.RowHeadersWidth = 51;
            dataGridViewLexemes.Size = new Size(300, 188);
            dataGridViewLexemes.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridViewLexemes);
            Controls.Add(BTN_SelectPDF_File);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridViewLexemes).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button BTN_SelectPDF_File;
        private DataGridView dataGridViewLexemes;
    }
}
