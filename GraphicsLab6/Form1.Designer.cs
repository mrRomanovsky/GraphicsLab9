namespace GraphicsLab6
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.task1Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.lab8Task1Button = new System.Windows.Forms.Button();
            this.lab8Task3Button = new System.Windows.Forms.Button();
            this.checkBoxLab8task2 = new System.Windows.Forms.CheckBox();
            this.lab9Task1Button = new System.Windows.Forms.Button();
            this.lab9Task2Button = new System.Windows.Forms.Button();
            this.lab9Task3Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(595, 469);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Тетраэдр",
            "Гексаэдр",
            "Октаэдр",
            "Икосаэдр",
            "Додекаэдр"});
            this.listBox1.Location = new System.Drawing.Point(622, 24);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(65, 69);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(703, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Нарисовать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Items.AddRange(new object[] {
            "Задание 2",
            "Задание 3",
            "Задание 4",
            "Задание 5",
            "Задание 6",
            "Задание 7",
            "Задание 8",
            "Задание 9 XOY",
            "Задание 9 YOZ",
            "Задание 9 XOZ"});
            this.listBox2.Location = new System.Drawing.Point(622, 111);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(120, 134);
            this.listBox2.TabIndex = 3;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(657, 458);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Очистить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(717, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Радиус";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(703, 44);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(85, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "50";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // task1Button
            // 
            this.task1Button.Location = new System.Drawing.Point(50, 487);
            this.task1Button.Name = "task1Button";
            this.task1Button.Size = new System.Drawing.Size(75, 23);
            this.task1Button.TabIndex = 8;
            this.task1Button.Text = "task1";
            this.task1Button.UseVisualStyleBackColor = true;
            this.task1Button.Click += new System.EventHandler(this.task1Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 441);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Лабораторная 7";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(274, 487);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "task 2";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lab8Task1Button
            // 
            this.lab8Task1Button.Location = new System.Drawing.Point(1093, 44);
            this.lab8Task1Button.Name = "lab8Task1Button";
            this.lab8Task1Button.Size = new System.Drawing.Size(75, 23);
            this.lab8Task1Button.TabIndex = 11;
            this.lab8Task1Button.Text = "lab8Task1";
            this.lab8Task1Button.UseVisualStyleBackColor = true;
            this.lab8Task1Button.Click += new System.EventHandler(this.lab8Task1Button_Click);
            // 
            // lab8Task3Button
            // 
            this.lab8Task3Button.Location = new System.Drawing.Point(1093, 409);
            this.lab8Task3Button.Name = "lab8Task3Button";
            this.lab8Task3Button.Size = new System.Drawing.Size(75, 23);
            this.lab8Task3Button.TabIndex = 13;
            this.lab8Task3Button.Text = "lab8Task3";
            this.lab8Task3Button.UseVisualStyleBackColor = true;
            this.lab8Task3Button.Click += new System.EventHandler(this.lab8Task3Button_Click);
            // 
            // checkBoxLab8task2
            // 
            this.checkBoxLab8task2.AutoSize = true;
            this.checkBoxLab8task2.Location = new System.Drawing.Point(1094, 227);
            this.checkBoxLab8task2.Name = "checkBoxLab8task2";
            this.checkBoxLab8task2.Size = new System.Drawing.Size(63, 17);
            this.checkBoxLab8task2.TabIndex = 14;
            this.checkBoxLab8task2.Text = "Z-buffer";
            this.checkBoxLab8task2.UseVisualStyleBackColor = true;
            this.checkBoxLab8task2.CheckedChanged += new System.EventHandler(this.checkBoxLab8task2_CheckedChanged);
            // 
            // lab9Task1Button
            // 
            this.lab9Task1Button.Location = new System.Drawing.Point(887, 56);
            this.lab9Task1Button.Name = "lab9Task1Button";
            this.lab9Task1Button.Size = new System.Drawing.Size(75, 52);
            this.lab9Task1Button.TabIndex = 15;
            this.lab9Task1Button.Text = "lab9Task1";
            this.lab9Task1Button.UseVisualStyleBackColor = true;
            this.lab9Task1Button.Click += new System.EventHandler(this.lab9Task1Button_Click);
            // 
            // lab9Task2Button
            // 
            this.lab9Task2Button.Location = new System.Drawing.Point(887, 208);
            this.lab9Task2Button.Name = "lab9Task2Button";
            this.lab9Task2Button.Size = new System.Drawing.Size(75, 52);
            this.lab9Task2Button.TabIndex = 16;
            this.lab9Task2Button.Text = "lab9Task2";
            this.lab9Task2Button.UseVisualStyleBackColor = true;
            // 
            // lab9Task3Button
            // 
            this.lab9Task3Button.Location = new System.Drawing.Point(887, 380);
            this.lab9Task3Button.Name = "lab9Task3Button";
            this.lab9Task3Button.Size = new System.Drawing.Size(75, 52);
            this.lab9Task3Button.TabIndex = 17;
            this.lab9Task3Button.Text = "lab9Task3";
            this.lab9Task3Button.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 514);
            this.Controls.Add(this.lab9Task3Button);
            this.Controls.Add(this.lab9Task2Button);
            this.Controls.Add(this.lab9Task1Button);
            this.Controls.Add(this.checkBoxLab8task2);
            this.Controls.Add(this.lab8Task3Button);
            this.Controls.Add(this.lab8Task1Button);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.task1Button);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button task1Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button lab8Task1Button;
        private System.Windows.Forms.Button lab8Task3Button;
        private System.Windows.Forms.CheckBox checkBoxLab8task2;
        private System.Windows.Forms.Button lab9Task1Button;
        private System.Windows.Forms.Button lab9Task2Button;
        private System.Windows.Forms.Button lab9Task3Button;
    }
}

