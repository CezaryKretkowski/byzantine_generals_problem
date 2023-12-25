namespace byzantine_generals_problem
{
    partial class MainWindow
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
            SettingsPanel = new Panel();
            SettingsLabel = new Label();
            Execute = new Button();
            ScriptTitle = new Label();
            SqlInput = new TextBox();
            ServerOutput = new Label();
            ServerOuts = new TextBox();
            dataGridView1 = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            Connection = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel5 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            SuspendLayout();
            // 
            // SettingsPanel
            // 
            SettingsPanel.AutoSize = true;
            SettingsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SettingsPanel.BackColor = SystemColors.ActiveCaption;
            SettingsPanel.Location = new Point(10, 10);
            SettingsPanel.Margin = new Padding(0);
            SettingsPanel.Name = "SettingsPanel";
            SettingsPanel.Size = new Size(0, 0);
            SettingsPanel.TabIndex = 1;
            // 
            // SettingsLabel
            // 
            SettingsLabel.AutoSize = true;
            SettingsLabel.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            SettingsLabel.Location = new Point(3, 0);
            SettingsLabel.Name = "SettingsLabel";
            SettingsLabel.Size = new Size(149, 37);
            SettingsLabel.TabIndex = 0;
            SettingsLabel.Text = "SQL Client";
            // 
            // Execute
            // 
            Execute.Dock = DockStyle.Bottom;
            Execute.Location = new Point(104, 3);
            Execute.Name = "Execute";
            Execute.Size = new Size(95, 23);
            Execute.TabIndex = 8;
            Execute.Text = "Execute";
            Execute.UseVisualStyleBackColor = true;
            Execute.Click += Execute_Click;
            // 
            // ScriptTitle
            // 
            ScriptTitle.AutoSize = true;
            ScriptTitle.Dock = DockStyle.Bottom;
            ScriptTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ScriptTitle.Location = new Point(3, 0);
            ScriptTitle.Name = "ScriptTitle";
            ScriptTitle.Size = new Size(95, 29);
            ScriptTitle.TabIndex = 7;
            ScriptTitle.Text = "SQL Query              ";
            ScriptTitle.TextAlign = ContentAlignment.BottomLeft;
            // 
            // SqlInput
            // 
            SqlInput.Dock = DockStyle.Fill;
            SqlInput.Location = new Point(3, 44);
            SqlInput.Multiline = true;
            SqlInput.Name = "SqlInput";
            SqlInput.Size = new Size(522, 119);
            SqlInput.TabIndex = 4;
            // 
            // ServerOutput
            // 
            ServerOutput.AutoSize = true;
            ServerOutput.Dock = DockStyle.Bottom;
            ServerOutput.Location = new Point(3, 221);
            ServerOutput.Name = "ServerOutput";
            ServerOutput.Size = new Size(528, 15);
            ServerOutput.TabIndex = 6;
            ServerOutput.Text = "Server output";
            // 
            // ServerOuts
            // 
            ServerOuts.BorderStyle = BorderStyle.FixedSingle;
            ServerOuts.Dock = DockStyle.Fill;
            ServerOuts.Location = new Point(3, 239);
            ServerOuts.Multiline = true;
            ServerOuts.Name = "ServerOuts";
            ServerOuts.ReadOnly = true;
            ServerOuts.ScrollBars = ScrollBars.Vertical;
            ServerOuts.Size = new Size(528, 58);
            ServerOuts.TabIndex = 5;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 303);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(528, 126);
            dataGridView1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.BackColor = SystemColors.ActiveCaption;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel1.Controls.Add(ServerOuts, 0, 3);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 4);
            tableLayoutPanel1.Controls.Add(ServerOutput, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(10, 10);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.Size = new Size(534, 432);
            tableLayoutPanel1.TabIndex = 9;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(SettingsLabel, 0, 0);
            tableLayoutPanel3.Controls.Add(Connection, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(528, 37);
            tableLayoutPanel3.TabIndex = 9;
            // 
            // Connection
            // 
            Connection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Connection.Location = new Point(401, 3);
            Connection.Name = "Connection";
            Connection.Size = new Size(124, 23);
            Connection.TabIndex = 2;
            Connection.Text = "Connection";
            Connection.UseVisualStyleBackColor = true;
            Connection.Click += Connection_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel2.Controls.Add(SqlInput, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 46);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            tableLayoutPanel2.Size = new Size(528, 166);
            tableLayoutPanel2.TabIndex = 9;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(522, 35);
            tableLayoutPanel4.TabIndex = 9;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(ScriptTitle, 0, 0);
            tableLayoutPanel5.Controls.Add(Execute, 1, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(202, 29);
            tableLayoutPanel5.TabIndex = 9;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(554, 452);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(SettingsPanel);
            Name = "MainWindow";
            Padding = new Padding(10);
            Text = "Raft SQL CLient";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel SettingsPanel;
        private Label SettingsLabel;
        private Label ServerOutput;
        private TextBox ServerOuts;
        private TextBox SqlInput;
        private Label ScriptTitle;
        private Button Execute;
        public DataGridView dataGridView1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button Connection;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
    }
}