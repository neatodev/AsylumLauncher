namespace AsylumLauncher
{
    partial class InputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputForm));
            label2 = new Label();
            KeybindValueLabel = new Label();
            label3 = new Label();
            label1 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Impact", 24F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(7, 128);
            label2.Name = "label2";
            label2.Size = new Size(130, 39);
            label2.TabIndex = 1;
            label2.Text = "KEYBIND:";
            label2.MouseClick += label4_MouseClick;
            // 
            // KeybindValueLabel
            // 
            KeybindValueLabel.AutoSize = true;
            KeybindValueLabel.Font = new Font("Impact", 24F, FontStyle.Regular, GraphicsUnit.Point);
            KeybindValueLabel.ForeColor = SystemColors.HotTrack;
            KeybindValueLabel.Location = new Point(143, 128);
            KeybindValueLabel.Name = "KeybindValueLabel";
            KeybindValueLabel.Size = new Size(0, 39);
            KeybindValueLabel.TabIndex = 2;
            KeybindValueLabel.MouseClick += label4_MouseClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calibri", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(7, 9);
            label3.Name = "label3";
            label3.Size = new Size(584, 33);
            label3.TabIndex = 3;
            label3.Text = "Press ESCAPE (ESC) to close this window and abort.";
            label3.MouseClick += label4_MouseClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(7, 43);
            label1.Name = "label1";
            label1.Size = new Size(464, 33);
            label1.TabIndex = 4;
            label1.Text = "Press BACKSPACE to delete the Keybind.\r\n";
            label1.MouseClick += label4_MouseClick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calibri", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(7, 77);
            label4.Name = "label4";
            label4.Size = new Size(420, 33);
            label4.TabIndex = 5;
            label4.Text = "Confirm Keybind by pressing ENTER.";
            label4.MouseClick += label4_MouseClick;
            // 
            // InputForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            BackColor = Color.White;
            ClientSize = new Size(587, 188);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(KeybindValueLabel);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "InputForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "InputForm";
            Load += InputForm_Load;
            Paint += InputForm_Paint;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Label KeybindValueLabel;
        private Label label3;
        private Label label1;
        private Label label4;
    }
}