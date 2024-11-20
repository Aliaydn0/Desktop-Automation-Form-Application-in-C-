namespace WinFormsApp3
{
    partial class Form11
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form11));
            button1 = new Button();
            textBox1 = new TextBox();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton5 = new RadioButton();
            radioButton6 = new RadioButton();
            button2 = new Button();
            dataGridView1 = new DataGridView();
            menuStrip1 = new MenuStrip();
            diğerToolStripMenuItem = new ToolStripMenuItem();
            kaydetToolStripMenuItem = new ToolStripMenuItem();
            cvsDosyasıOlarakKaydetToolStripMenuItem = new ToolStripMenuItem();
            saveFileDialog1 = new SaveFileDialog();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(173, 149);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "Filtrele";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(173, 103);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Veri Giriniz";
            textBox1.Size = new Size(589, 27);
            textBox1.TabIndex = 1;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(457, 154);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(51, 24);
            radioButton1.TabIndex = 4;
            radioButton1.TabStop = true;
            radioButton1.Text = "Tür";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.Visible = false;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(514, 154);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(68, 24);
            radioButton2.TabIndex = 5;
            radioButton2.TabStop = true;
            radioButton2.Text = "Boyut";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.Visible = false;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(667, 154);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(77, 24);
            radioButton3.TabIndex = 7;
            radioButton3.TabStop = true;
            radioButton3.Text = "Konum";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.Visible = false;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(588, 154);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(73, 24);
            radioButton4.TabIndex = 6;
            radioButton4.TabStop = true;
            radioButton4.Text = "Ağırlık";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.Visible = false;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(284, 154);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(78, 24);
            radioButton5.TabIndex = 2;
            radioButton5.TabStop = true;
            radioButton5.Text = "Stok ID";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.Visible = false;
            // 
            // radioButton6
            // 
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(368, 154);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(83, 24);
            radioButton6.TabIndex = 3;
            radioButton6.TabStop = true;
            radioButton6.Text = "Parti No";
            radioButton6.UseVisualStyleBackColor = true;
            radioButton6.Visible = false;
            // 
            // button2
            // 
            button2.Location = new Point(173, 198);
            button2.Name = "button2";
            button2.Size = new Size(162, 48);
            button2.TabIndex = 8;
            button2.Text = "Stok Ara";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Bottom;
            dataGridView1.Location = new Point(0, 262);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1032, 557);
            dataGridView1.TabIndex = 9;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { diğerToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1032, 28);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // diğerToolStripMenuItem
            // 
            diğerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { kaydetToolStripMenuItem });
            diğerToolStripMenuItem.Name = "diğerToolStripMenuItem";
            diğerToolStripMenuItem.Size = new Size(60, 24);
            diğerToolStripMenuItem.Text = "Diğer";
            // 
            // kaydetToolStripMenuItem
            // 
            kaydetToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cvsDosyasıOlarakKaydetToolStripMenuItem });
            kaydetToolStripMenuItem.Name = "kaydetToolStripMenuItem";
            kaydetToolStripMenuItem.Size = new Size(138, 26);
            kaydetToolStripMenuItem.Text = "Kaydet";
            // 
            // cvsDosyasıOlarakKaydetToolStripMenuItem
            // 
            cvsDosyasıOlarakKaydetToolStripMenuItem.Name = "cvsDosyasıOlarakKaydetToolStripMenuItem";
            cvsDosyasıOlarakKaydetToolStripMenuItem.Size = new Size(266, 26);
            cvsDosyasıOlarakKaydetToolStripMenuItem.Text = "Cvs Dosyası Olarak Kaydet";
            cvsDosyasıOlarakKaydetToolStripMenuItem.Click += cvsDosyasıOlarakKaydetToolStripMenuItem_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 239);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 11;
            label2.Text = "label2";
            // 
            // Form11
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1032, 819);
            Controls.Add(label2);
            Controls.Add(dataGridView1);
            Controls.Add(button2);
            Controls.Add(radioButton6);
            Controls.Add(radioButton5);
            Controls.Add(radioButton4);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(1050, 866);
            MinimumSize = new Size(1050, 866);
            Name = "Form11";
            Text = "Eva Life Otomasyon - Stok";
            Load += Form11_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private RadioButton radioButton5;
        private RadioButton radioButton6;
        private Button button2;
        private DataGridView dataGridView1;
        private Label label1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem diğerToolStripMenuItem;
        private ToolStripMenuItem kaydetToolStripMenuItem;
        private ToolStripMenuItem cvsDosyasıOlarakKaydetToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private Label label2;
    }
}