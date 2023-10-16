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
            GeneralPanel = new Panel();
            Atack = new Button();
            Retreat = new Button();
            None = new Button();
            Title = new Label();
            SettingsPanel = new Panel();
            SettingsLabel = new Label();
            HostList = new ListBox();
            Connection = new Button();
            Decision = new Label();
            GeneralPanel.SuspendLayout();
            SettingsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.Controls.Add(Decision);
            GeneralPanel.Controls.Add(Title);
            GeneralPanel.Controls.Add(None);
            GeneralPanel.Controls.Add(Retreat);
            GeneralPanel.Controls.Add(Atack);
            GeneralPanel.Location = new Point(1, 2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(265, 443);
            GeneralPanel.TabIndex = 0;
            // 
            // Atack
            // 
            Atack.Location = new Point(41, 121);
            Atack.Name = "Atack";
            Atack.Size = new Size(167, 34);
            Atack.TabIndex = 0;
            Atack.Text = "Atack";
            Atack.UseVisualStyleBackColor = true;
            // 
            // Retreat
            // 
            Retreat.Location = new Point(41, 161);
            Retreat.Name = "Retreat";
            Retreat.Size = new Size(167, 34);
            Retreat.TabIndex = 1;
            Retreat.Text = "Retreat";
            Retreat.UseVisualStyleBackColor = true;
            // 
            // None
            // 
            None.Location = new Point(41, 201);
            None.Name = "None";
            None.Size = new Size(167, 34);
            None.TabIndex = 2;
            None.Text = "None";
            None.UseVisualStyleBackColor = true;
            // 
            // Title
            // 
            Title.AutoSize = true;
            Title.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            Title.Location = new Point(11, 7);
            Title.Name = "Title";
            Title.Size = new Size(155, 40);
            Title.TabIndex = 3;
            Title.Text = "General nr.";
            // 
            // SettingsPanel
            // 
            SettingsPanel.Controls.Add(Connection);
            SettingsPanel.Controls.Add(HostList);
            SettingsPanel.Controls.Add(SettingsLabel);
            SettingsPanel.Location = new Point(266, 2);
            SettingsPanel.Name = "SettingsPanel";
            SettingsPanel.Size = new Size(295, 443);
            SettingsPanel.TabIndex = 1;
            // 
            // SettingsLabel
            // 
            SettingsLabel.AutoSize = true;
            SettingsLabel.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            SettingsLabel.Location = new Point(3, 7);
            SettingsLabel.Name = "SettingsLabel";
            SettingsLabel.Size = new Size(119, 40);
            SettingsLabel.TabIndex = 0;
            SettingsLabel.Text = "Settings";
            // 
            // HostList
            // 
            HostList.FormattingEnabled = true;
            HostList.ItemHeight = 15;
            HostList.Location = new Point(19, 102);
            HostList.Name = "HostList";
            HostList.Size = new Size(255, 319);
            HostList.TabIndex = 1;
            // 
            // Connection
            // 
            Connection.Location = new Point(150, 23);
            Connection.Name = "Connection";
            Connection.Size = new Size(124, 23);
            Connection.TabIndex = 2;
            Connection.Text = "Connection";
            Connection.UseVisualStyleBackColor = true;
            // 
            // Decision
            // 
            Decision.AutoSize = true;
            Decision.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            Decision.Location = new Point(11, 258);
            Decision.Name = "Decision";
            Decision.Size = new Size(138, 40);
            Decision.TabIndex = 4;
            Decision.Text = "Decision :";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(564, 450);
            Controls.Add(SettingsPanel);
            Controls.Add(GeneralPanel);
            Name = "MainWindow";
            Text = "Byzantine generals problem";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            SettingsPanel.ResumeLayout(false);
            SettingsPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Button Atack;
        private Label Title;
        private Button None;
        private Button Retreat;
        private Panel SettingsPanel;
        private ListBox HostList;
        private Label SettingsLabel;
        private Button Connection;
        private Label Decision;
    }
}