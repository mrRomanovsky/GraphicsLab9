namespace GraphicsLab6
{
    partial class Lab9Task3
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxX1 = new System.Windows.Forms.TextBox();
            this.textBoxY1 = new System.Windows.Forms.TextBox();
            this.textBoxX2 = new System.Windows.Forms.TextBox();
            this.textBoxY2 = new System.Windows.Forms.TextBox();
            this.textBoxStep = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxXXYY = new System.Windows.Forms.CheckBox();
            this.checkBoxSinCos = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBoxAbs = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(410, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Plot";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxX1
            // 
            this.textBoxX1.Location = new System.Drawing.Point(198, 92);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(100, 20);
            this.textBoxX1.TabIndex = 1;
            this.textBoxX1.Text = "-10";
            this.textBoxX1.TextChanged += new System.EventHandler(this.textBoxX1_TextChanged);
            // 
            // textBoxY1
            // 
            this.textBoxY1.Location = new System.Drawing.Point(198, 138);
            this.textBoxY1.Name = "textBoxY1";
            this.textBoxY1.Size = new System.Drawing.Size(100, 20);
            this.textBoxY1.TabIndex = 2;
            this.textBoxY1.Text = "-10";
            // 
            // textBoxX2
            // 
            this.textBoxX2.Location = new System.Drawing.Point(304, 92);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.Size = new System.Drawing.Size(100, 20);
            this.textBoxX2.TabIndex = 3;
            this.textBoxX2.Text = "10";
            // 
            // textBoxY2
            // 
            this.textBoxY2.Location = new System.Drawing.Point(304, 138);
            this.textBoxY2.Name = "textBoxY2";
            this.textBoxY2.Size = new System.Drawing.Size(100, 20);
            this.textBoxY2.TabIndex = 4;
            this.textBoxY2.Text = "10";
            // 
            // textBoxStep
            // 
            this.textBoxStep.Location = new System.Drawing.Point(410, 92);
            this.textBoxStep.Name = "textBoxStep";
            this.textBoxStep.Size = new System.Drawing.Size(100, 20);
            this.textBoxStep.TabIndex = 5;
            this.textBoxStep.Text = "0,2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "X1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Y1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(334, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "X2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(334, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Y2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(442, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Step";
            // 
            // checkBoxXXYY
            // 
            this.checkBoxXXYY.AutoSize = true;
            this.checkBoxXXYY.Location = new System.Drawing.Point(12, 92);
            this.checkBoxXXYY.Name = "checkBoxXXYY";
            this.checkBoxXXYY.Size = new System.Drawing.Size(71, 17);
            this.checkBoxXXYY.TabIndex = 11;
            this.checkBoxXXYY.Text = "X*X +Y*Y";
            this.checkBoxXXYY.UseVisualStyleBackColor = true;
            // 
            // checkBoxSinCos
            // 
            this.checkBoxSinCos.AutoSize = true;
            this.checkBoxSinCos.Location = new System.Drawing.Point(12, 135);
            this.checkBoxSinCos.Name = "checkBoxSinCos";
            this.checkBoxSinCos.Size = new System.Drawing.Size(63, 17);
            this.checkBoxSinCos.TabIndex = 12;
            this.checkBoxSinCos.Text = "Sin*Cos";
            this.checkBoxSinCos.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(103, 135);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(59, 17);
            this.checkBox3.TabIndex = 13;
            this.checkBox3.Text = "Cos(...)";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBoxAbs
            // 
            this.checkBoxAbs.AutoSize = true;
            this.checkBoxAbs.Location = new System.Drawing.Point(103, 92);
            this.checkBoxAbs.Name = "checkBoxAbs";
            this.checkBoxAbs.Size = new System.Drawing.Size(44, 17);
            this.checkBoxAbs.TabIndex = 14;
            this.checkBoxAbs.Text = "Abs";
            this.checkBoxAbs.UseVisualStyleBackColor = true;
            // 
            // Lab9Task3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 237);
            this.Controls.Add(this.checkBoxAbs);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBoxSinCos);
            this.Controls.Add(this.checkBoxXXYY);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxStep);
            this.Controls.Add(this.textBoxY2);
            this.Controls.Add(this.textBoxX2);
            this.Controls.Add(this.textBoxY1);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.button1);
            this.Name = "Lab9Task3";
            this.Text = "Lab9Task3";
            this.Load += new System.EventHandler(this.Lab9Task3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxX1;
        private System.Windows.Forms.TextBox textBoxY1;
        private System.Windows.Forms.TextBox textBoxX2;
        private System.Windows.Forms.TextBox textBoxY2;
        private System.Windows.Forms.TextBox textBoxStep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxXXYY;
        private System.Windows.Forms.CheckBox checkBoxSinCos;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBoxAbs;
    }
}