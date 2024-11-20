namespace WinFormsApp3
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            textBox1 = new TextBox();
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            button1 = new Button();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton5 = new RadioButton();
            button2 = new Button();
            comboBox2 = new ComboBox();
            button3 = new Button();
            radioButton6 = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(528, 67);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "ID Giriniz Veya Filtre Yapınız";
            textBox1.Size = new Size(705, 27);
            textBox1.TabIndex = 3;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 168);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1943, 826);
            dataGridView1.TabIndex = 2;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(0, 168);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(1943, 826);
            dataGridView2.TabIndex = 11;
            dataGridView2.Visible = false;
            // 
            // button1
            // 
            button1.Location = new Point(667, 109);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 5;
            button1.Text = "Filtrele";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(822, 111);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(88, 24);
            radioButton1.TabIndex = 6;
            radioButton1.TabStop = true;
            radioButton1.Text = "Ürün Adı";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.Visible = false;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(916, 111);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(83, 24);
            radioButton2.TabIndex = 7;
            radioButton2.TabStop = true;
            radioButton2.Text = "Parti No";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.Visible = false;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(1005, 111);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(68, 24);
            radioButton3.TabIndex = 8;
            radioButton3.TabStop = true;
            radioButton3.Text = "Boyut";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.Visible = false;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(1079, 111);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(62, 24);
            radioButton4.TabIndex = 9;
            radioButton4.TabStop = true;
            radioButton4.Text = "Adet";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.Visible = false;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(1147, 111);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(69, 24);
            radioButton5.TabIndex = 10;
            radioButton5.TabStop = true;
            radioButton5.Text = "Bölge";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.Visible = false;
            // 
            // button2
            // 
            button2.Location = new Point(528, 100);
            button2.Name = "button2";
            button2.Size = new Size(133, 38);
            button2.TabIndex = 1;
            button2.Text = "Ara";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // comboBox2
            // 
            comboBox2.ForeColor = SystemColors.WindowFrame;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(528, 33);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(705, 28);
            comboBox2.TabIndex = 2;
            comboBox2.Text = "Departman Seçiniz";
            // 
            // button3
            // 
            button3.Location = new Point(1239, 27);
            button3.Name = "button3";
            button3.Size = new Size(133, 38);
            button3.TabIndex = 4;
            button3.Text = "Ara";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // radioButton6
            // 
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(771, 111);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(45, 24);
            radioButton6.TabIndex = 16;
            radioButton6.TabStop = true;
            radioButton6.Text = "ID";
            radioButton6.UseVisualStyleBackColor = true;
            radioButton6.Visible = false;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1900, 971);
            Controls.Add(radioButton6);
            Controls.Add(button3);
            Controls.Add(comboBox2);
            Controls.Add(button2);
            Controls.Add(radioButton5);
            Controls.Add(radioButton4);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(button1);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Controls.Add(textBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(1920, 1080);
            MinimumSize = new Size(1918, 1018);
            Name = "Form4";
            Text = "Eva Life Otomasyon ";
            Load += Form4_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Button button1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private RadioButton radioButton5;
        private Button button2;
        private ComboBox comboBox2;
        private Button button3;
        private RadioButton radioButton6;
    }
}