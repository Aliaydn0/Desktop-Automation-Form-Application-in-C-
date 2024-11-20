namespace WinFormsApp3
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            textBox1 = new TextBox();
            sip_ekle_btn = new Button();
            comboBox1 = new ComboBox();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            button6 = new Button();
            button7 = new Button();
            menuStrip1 = new MenuStrip();
            diğerToolStripMenuItem = new ToolStripMenuItem();
            araToolStripMenuItem = new ToolStripMenuItem();
            barkodİşlemleriToolStripMenuItem = new ToolStripMenuItem();
            yardımToolStripMenuItem = new ToolStripMenuItem();
            hakkındaToolStripMenuItem = new ToolStripMenuItem();
            lisansToolStripMenuItem = new ToolStripMenuItem();
            işlemGeçmişiToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.ForeColor = SystemColors.WindowText;
            textBox1.Location = new Point(80, 96);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Sipariş no";
            textBox1.Size = new Size(448, 27);
            textBox1.TabIndex = 1;
            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;
            // 
            // sip_ekle_btn
            // 
            sip_ekle_btn.BackColor = Color.Green;
            sip_ekle_btn.FlatStyle = FlatStyle.Popup;
            sip_ekle_btn.Location = new Point(52, 426);
            sip_ekle_btn.Name = "sip_ekle_btn";
            sip_ekle_btn.Size = new Size(133, 38);
            sip_ekle_btn.TabIndex = 0;
            sip_ekle_btn.Text = "Ekle";
            sip_ekle_btn.UseVisualStyleBackColor = false;
            sip_ekle_btn.Click += sip_ekle_btn_Click;
            sip_ekle_btn.MouseEnter += sip_ekle_btn_MouseEnter;
            sip_ekle_btn.MouseLeave += sip_ekle_btn_MouseLeave;
            // 
            // comboBox1
            // 
            comboBox1.ForeColor = SystemColors.WindowFrame;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(80, 146);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(448, 28);
            comboBox1.TabIndex = 2;
            comboBox1.Text = "Bölge Seçiniz";
            comboBox1.Enter += comboBox1_Enter;
            comboBox1.Leave += comboBox1_Leave;
            // 
            // button1
            // 
            button1.BackColor = Color.Red;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(232, 426);
            button1.Name = "button1";
            button1.Size = new Size(133, 38);
            button1.TabIndex = 7;
            button1.Text = "Sil";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            button1.MouseEnter += button1_MouseEnter;
            button1.MouseLeave += button1_MouseLeave;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Right;
            dataGridView1.Location = new Point(627, 28);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1275, 1005);
            dataGridView1.TabIndex = 14;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Popup;
            button2.Location = new Point(423, 426);
            button2.Name = "button2";
            button2.Size = new Size(133, 38);
            button2.TabIndex = 8;
            button2.Text = "Diğer";
            button2.UseVisualStyleBackColor = true;
            button2.Visible = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Yellow;
            button3.FlatStyle = FlatStyle.Popup;
            button3.Location = new Point(423, 426);
            button3.Name = "button3";
            button3.Size = new Size(133, 38);
            button3.TabIndex = 9;
            button3.Text = "Güncelle";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            button3.MouseEnter += button3_MouseEnter;
            button3.MouseLeave += button3_MouseLeave;
            // 
            // button4
            // 
            button4.FlatStyle = FlatStyle.Popup;
            button4.Location = new Point(52, 506);
            button4.Name = "button4";
            button4.Size = new Size(133, 38);
            button4.TabIndex = 11;
            button4.Text = "Ara";
            button4.UseVisualStyleBackColor = true;
            button4.Visible = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.FlatStyle = FlatStyle.Popup;
            button5.Location = new Point(232, 506);
            button5.Name = "button5";
            button5.Size = new Size(133, 38);
            button5.TabIndex = 12;
            button5.Text = "Barkod İşlemleri";
            button5.UseVisualStyleBackColor = true;
            button5.Visible = false;
            button5.Click += button5_Click;
            // 
            // textBox2
            // 
            textBox2.ForeColor = SystemColors.WindowText;
            textBox2.Location = new Point(80, 193);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Ürün Adı";
            textBox2.Size = new Size(448, 27);
            textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            textBox3.ForeColor = SystemColors.WindowText;
            textBox3.Location = new Point(80, 243);
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Boyut";
            textBox3.Size = new Size(448, 27);
            textBox3.TabIndex = 4;
            // 
            // textBox4
            // 
            textBox4.ForeColor = SystemColors.WindowText;
            textBox4.Location = new Point(80, 337);
            textBox4.Name = "textBox4";
            textBox4.PlaceholderText = "Açıklama";
            textBox4.Size = new Size(448, 27);
            textBox4.TabIndex = 6;
            // 
            // textBox5
            // 
            textBox5.ForeColor = SystemColors.WindowText;
            textBox5.Location = new Point(80, 289);
            textBox5.Name = "textBox5";
            textBox5.PlaceholderText = "Adet";
            textBox5.Size = new Size(448, 27);
            textBox5.TabIndex = 5;
            // 
            // button6
            // 
            button6.FlatStyle = FlatStyle.Popup;
            button6.Location = new Point(423, 506);
            button6.Name = "button6";
            button6.Size = new Size(133, 38);
            button6.TabIndex = 13;
            button6.Text = "Kapat";
            button6.UseVisualStyleBackColor = true;
            button6.Visible = false;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.BackColor = Color.Red;
            button7.Dock = DockStyle.Bottom;
            button7.Location = new Point(0, 999);
            button7.Name = "button7";
            button7.Size = new Size(627, 34);
            button7.TabIndex = 10;
            button7.Text = "Çıkış";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ActiveCaptionText;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { diğerToolStripMenuItem, yardımToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1902, 28);
            menuStrip1.TabIndex = 15;
            menuStrip1.Text = "menuStrip1";
            // 
            // diğerToolStripMenuItem
            // 
            diğerToolStripMenuItem.BackColor = SystemColors.HighlightText;
            diğerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { araToolStripMenuItem, barkodİşlemleriToolStripMenuItem });
            diğerToolStripMenuItem.Margin = new Padding(0, 0, 5, 0);
            diğerToolStripMenuItem.Name = "diğerToolStripMenuItem";
            diğerToolStripMenuItem.Padding = new Padding(10, 0, 10, 0);
            diğerToolStripMenuItem.Size = new Size(70, 24);
            diğerToolStripMenuItem.Text = "Diğer";
            // 
            // araToolStripMenuItem
            // 
            araToolStripMenuItem.Name = "araToolStripMenuItem";
            araToolStripMenuItem.Size = new Size(199, 26);
            araToolStripMenuItem.Text = "Ara";
            araToolStripMenuItem.Click += araToolStripMenuItem_Click;
            // 
            // barkodİşlemleriToolStripMenuItem
            // 
            barkodİşlemleriToolStripMenuItem.Name = "barkodİşlemleriToolStripMenuItem";
            barkodİşlemleriToolStripMenuItem.Size = new Size(199, 26);
            barkodİşlemleriToolStripMenuItem.Text = "Barkod İşlemleri";
            barkodİşlemleriToolStripMenuItem.Click += barkodİşlemleriToolStripMenuItem_Click;
            // 
            // yardımToolStripMenuItem
            // 
            yardımToolStripMenuItem.BackColor = SystemColors.HighlightText;
            yardımToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { hakkındaToolStripMenuItem, işlemGeçmişiToolStripMenuItem });
            yardımToolStripMenuItem.Name = "yardımToolStripMenuItem";
            yardımToolStripMenuItem.Size = new Size(69, 24);
            yardımToolStripMenuItem.Text = "Yardım";
            // 
            // hakkındaToolStripMenuItem
            // 
            hakkındaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lisansToolStripMenuItem });
            hakkındaToolStripMenuItem.Name = "hakkındaToolStripMenuItem";
            hakkındaToolStripMenuItem.Size = new Size(183, 26);
            hakkındaToolStripMenuItem.Text = "Hakkında";
            // 
            // lisansToolStripMenuItem
            // 
            lisansToolStripMenuItem.Name = "lisansToolStripMenuItem";
            lisansToolStripMenuItem.Size = new Size(131, 26);
            lisansToolStripMenuItem.Text = "Lisans";
            lisansToolStripMenuItem.Click += lisansToolStripMenuItem_Click;
            // 
            // işlemGeçmişiToolStripMenuItem
            // 
            işlemGeçmişiToolStripMenuItem.Name = "işlemGeçmişiToolStripMenuItem";
            işlemGeçmişiToolStripMenuItem.Size = new Size(183, 26);
            işlemGeçmişiToolStripMenuItem.Text = "İşlem Geçmişi";
            işlemGeçmişiToolStripMenuItem.Click += işlemGeçmişiToolStripMenuItem_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1033);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(sip_ekle_btn);
            Controls.Add(textBox1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(1920, 1080);
            MinimumSize = new Size(1918, 1018);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Eva Life Otomasyon ";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button sip_ekle_btn;
        private ComboBox comboBox1;
        private Button button1;
        private DataGridView dataGridView1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private Button button6;
        private Button button7;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem yardımToolStripMenuItem;
        private ToolStripMenuItem hakkındaToolStripMenuItem;
        private ToolStripMenuItem lisansToolStripMenuItem;
        private ToolStripMenuItem işlemGeçmişiToolStripMenuItem;
        private ToolStripMenuItem diğerToolStripMenuItem;
        private ToolStripMenuItem araToolStripMenuItem;
        private ToolStripMenuItem barkodİşlemleriToolStripMenuItem;
    }
}