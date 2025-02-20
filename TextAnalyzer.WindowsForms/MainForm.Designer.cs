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

        private void InitializeComponent()
        {
            BTN_SelectPDF_File = new Button();
            dataGridViewLexemes = new DataGridView();
            flowLayoutPanel1 = new FlowLayoutPanel();
            AddWordButton = new Button();
            NewWordTextBox = new TextBox();
            MorfInfoLabel = new Label();
            SyntaxRoleLabel = new Label();
            MorfInfoTextBox = new TextBox();
            SyntaxRoleListBox = new ListView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLexemes).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // BTN_SelectPDF_File
            // 
            BTN_SelectPDF_File.Location = new Point(3, 3);
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
            dataGridViewLexemes.Dock = DockStyle.Fill;
            dataGridViewLexemes.Location = new Point(215, 0);
            dataGridViewLexemes.Name = "dataGridViewLexemes";
            dataGridViewLexemes.RowHeadersWidth = 51;
            dataGridViewLexemes.Size = new Size(585, 450);
            dataGridViewLexemes.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(BTN_SelectPDF_File);
            flowLayoutPanel1.Controls.Add(AddWordButton);
            flowLayoutPanel1.Controls.Add(NewWordTextBox);
            flowLayoutPanel1.Controls.Add(MorfInfoLabel);
            flowLayoutPanel1.Controls.Add(MorfInfoTextBox);
            flowLayoutPanel1.Controls.Add(SyntaxRoleLabel);
            flowLayoutPanel1.Controls.Add(SyntaxRoleListBox);
            flowLayoutPanel1.Dock = DockStyle.Left;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(215, 450);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // AddWordButton
            // 
            AddWordButton.Location = new Point(3, 38);
            AddWordButton.Name = "AddWordButton";
            AddWordButton.Size = new Size(94, 29);
            AddWordButton.TabIndex = 1;
            AddWordButton.Text = "Добавить";
            AddWordButton.UseVisualStyleBackColor = true;
            AddWordButton.Click += AddWordButton_Click;
            // 
            // NewWordTextBox
            // 
            NewWordTextBox.Location = new Point(3, 73);
            NewWordTextBox.Name = "NewWordTextBox";
            NewWordTextBox.Size = new Size(125, 27);
            NewWordTextBox.TabIndex = 2;
            // 
            // MorfInfoLabel
            // 
            MorfInfoLabel.AutoSize = true;
            MorfInfoLabel.Location = new Point(3, 103);
            MorfInfoLabel.Name = "MorfInfoLabel";
            MorfInfoLabel.Size = new Size(141, 40);
            MorfInfoLabel.TabIndex = 4;
            MorfInfoLabel.Text = "Морфологическая информация";
            // 
            // SyntaxRoleLabel
            // 
            SyntaxRoleLabel.AutoSize = true;
            SyntaxRoleLabel.Location = new Point(3, 176);
            SyntaxRoleLabel.Name = "SyntaxRoleLabel";
            SyntaxRoleLabel.Size = new Size(157, 20);
            SyntaxRoleLabel.TabIndex = 5;
            SyntaxRoleLabel.Text = "Синтаксическая роль";
            // 
            // MorfInfoTextBox
            // 
            MorfInfoTextBox.Location = new Point(3, 146);
            MorfInfoTextBox.Name = "MorfInfoTextBox";
            MorfInfoTextBox.Size = new Size(125, 27);
            MorfInfoTextBox.TabIndex = 6;
            // 
            // SyntaxRoleListBox
            // 
            SyntaxRoleListBox.Location = new Point(3, 199);
            SyntaxRoleListBox.Name = "SyntaxRoleListBox";
            SyntaxRoleListBox.Size = new Size(151, 121);
            SyntaxRoleListBox.TabIndex = 8;
            SyntaxRoleListBox.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridViewLexemes);
            Controls.Add(flowLayoutPanel1);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridViewLexemes).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button BTN_SelectPDF_File;
        private DataGridView dataGridViewLexemes;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button AddWordButton;
        private TextBox NewWordTextBox;
        private Label MorfInfoLabel;
        private Label SyntaxRoleLabel;
        private TextBox MorfInfoTextBox;
        private ListView SyntaxRoleListBox;
    }
}
