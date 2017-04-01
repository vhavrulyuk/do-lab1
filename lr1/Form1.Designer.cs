namespace lr1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.xCount = new System.Windows.Forms.NumericUpDown();
            this.lCount = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.limitationsGB = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.stepByStep = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.xCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lCount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.limitationsGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 379);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Розвязок свого варіанту";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(126, 62);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Задати обмеження";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Кількість видів продукції:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Кількість видів ресурсів:";
            // 
            // xCount
            // 
            this.xCount.Location = new System.Drawing.Point(185, 14);
            this.xCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.xCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.xCount.Name = "xCount";
            this.xCount.Size = new System.Drawing.Size(66, 20);
            this.xCount.TabIndex = 4;
            this.xCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.xCount.ValueChanged += new System.EventHandler(this.xCount_ValueChanged);
            // 
            // lCount
            // 
            this.lCount.Location = new System.Drawing.Point(185, 36);
            this.lCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.lCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.lCount.Name = "lCount";
            this.lCount.Size = new System.Drawing.Size(66, 20);
            this.lCount.TabIndex = 5;
            this.lCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lCount);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.xCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 98);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Дані";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // limitationsGB
            // 
            this.limitationsGB.Controls.Add(this.label3);
            this.limitationsGB.Location = new System.Drawing.Point(278, 12);
            this.limitationsGB.Name = "limitationsGB";
            this.limitationsGB.Size = new System.Drawing.Size(489, 399);
            this.limitationsGB.TabIndex = 7;
            this.limitationsGB.TabStop = false;
            this.limitationsGB.Text = "Обмеження";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Функція мети";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 116);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(165, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Розвязок";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // stepByStep
            // 
            this.stepByStep.AutoSize = true;
            this.stepByStep.Location = new System.Drawing.Point(12, 145);
            this.stepByStep.Name = "stepByStep";
            this.stepByStep.Size = new System.Drawing.Size(158, 17);
            this.stepByStep.TabIndex = 9;
            this.stepByStep.Text = "Покрокове відображення.";
            this.stepByStep.UseVisualStyleBackColor = true;
            this.stepByStep.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 432);
            this.Controls.Add(this.stepByStep);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.limitationsGB);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lCount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.limitationsGB.ResumeLayout(false);
            this.limitationsGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown xCount;
        private System.Windows.Forms.NumericUpDown lCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.GroupBox limitationsGB;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox stepByStep;
    }
}

