namespace DrawGraph
{
    sealed partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.functionBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DrawBtn = new System.Windows.Forms.Button();
            this.ErrorLbl = new System.Windows.Forms.Label();
            this.gridBox = new System.Windows.Forms.CheckBox();
            this.ticksBox = new System.Windows.Forms.CheckBox();
            this.axesBox = new System.Windows.Forms.CheckBox();
            this.AddBtn = new System.Windows.Forms.Button();
            this.colorBtn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.controlCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // functionBox
            // 
            this.functionBox.BackColor = System.Drawing.SystemColors.Window;
            this.functionBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.functionBox.Location = new System.Drawing.Point(39, 11);
            this.functionBox.Name = "functionBox";
            this.functionBox.Size = new System.Drawing.Size(158, 20);
            this.functionBox.TabIndex = 0;
            this.functionBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EnterPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "y  =";
            // 
            // DrawBtn
            // 
            this.DrawBtn.Location = new System.Drawing.Point(39, 33);
            this.DrawBtn.Name = "DrawBtn";
            this.DrawBtn.Size = new System.Drawing.Size(75, 23);
            this.DrawBtn.TabIndex = 2;
            this.DrawBtn.Text = "Draw";
            this.DrawBtn.UseVisualStyleBackColor = true;
            this.DrawBtn.Click += new System.EventHandler(this.DrawClick);
            // 
            // ErrorLbl
            // 
            this.ErrorLbl.AutoSize = true;
            this.ErrorLbl.Location = new System.Drawing.Point(36, 68);
            this.ErrorLbl.Name = "ErrorLbl";
            this.ErrorLbl.Size = new System.Drawing.Size(0, 13);
            this.ErrorLbl.TabIndex = 4;
            // 
            // gridBox
            // 
            this.gridBox.AutoSize = true;
            this.gridBox.Checked = true;
            this.gridBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gridBox.Location = new System.Drawing.Point(217, 9);
            this.gridBox.Name = "gridBox";
            this.gridBox.Size = new System.Drawing.Size(45, 17);
            this.gridBox.TabIndex = 5;
            this.gridBox.Text = "Grid";
            this.gridBox.UseVisualStyleBackColor = true;
            this.gridBox.CheckedChanged += new System.EventHandler(this.gridBox_CheckedChanged);
            // 
            // ticksBox
            // 
            this.ticksBox.AutoSize = true;
            this.ticksBox.Checked = true;
            this.ticksBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ticksBox.Location = new System.Drawing.Point(217, 33);
            this.ticksBox.Name = "ticksBox";
            this.ticksBox.Size = new System.Drawing.Size(79, 17);
            this.ticksBox.TabIndex = 6;
            this.ticksBox.Text = "Tick Marks";
            this.ticksBox.UseVisualStyleBackColor = true;
            this.ticksBox.CheckedChanged += new System.EventHandler(this.ticksBox_CheckedChanged);
            // 
            // axesBox
            // 
            this.axesBox.AutoSize = true;
            this.axesBox.Checked = true;
            this.axesBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.axesBox.Location = new System.Drawing.Point(217, 57);
            this.axesBox.Name = "axesBox";
            this.axesBox.Size = new System.Drawing.Size(49, 17);
            this.axesBox.TabIndex = 7;
            this.axesBox.Text = "Axes";
            this.axesBox.UseVisualStyleBackColor = true;
            this.axesBox.CheckedChanged += new System.EventHandler(this.axesBox_CheckedChanged);
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(122, 33);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(75, 23);
            this.AddBtn.TabIndex = 3;
            this.AddBtn.Text = "Add";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddClick);
            // 
            // colorBtn
            // 
            this.colorBtn.Location = new System.Drawing.Point(39, 57);
            this.colorBtn.Name = "colorBtn";
            this.colorBtn.Size = new System.Drawing.Size(158, 23);
            this.colorBtn.TabIndex = 8;
            this.colorBtn.Text = "FunctionColor";
            this.colorBtn.UseVisualStyleBackColor = true;
            this.colorBtn.Click += new System.EventHandler(this.colorBtn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(39, 86);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(158, 21);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // controlCheck
            // 
            this.controlCheck.AutoSize = true;
            this.controlCheck.Checked = true;
            this.controlCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.controlCheck.Location = new System.Drawing.Point(678, 732);
            this.controlCheck.Name = "controlCheck";
            this.controlCheck.Size = new System.Drawing.Size(94, 17);
            this.controlCheck.TabIndex = 10;
            this.controlCheck.Text = "Show Controls";
            this.controlCheck.UseVisualStyleBackColor = true;
            this.controlCheck.CheckedChanged += new System.EventHandler(this.controlCheck_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.controlCheck);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.colorBtn);
            this.Controls.Add(this.axesBox);
            this.Controls.Add(this.ticksBox);
            this.Controls.Add(this.gridBox);
            this.Controls.Add(this.ErrorLbl);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.DrawBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.functionBox);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(800, 800);
            this.MinimumSize = new System.Drawing.Size(800, 800);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Zoom);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox functionBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DrawBtn;
        private System.Windows.Forms.Label ErrorLbl;
        private System.Windows.Forms.CheckBox gridBox;
        private System.Windows.Forms.CheckBox ticksBox;
        private System.Windows.Forms.CheckBox axesBox;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Button colorBtn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox controlCheck;

    }
}

