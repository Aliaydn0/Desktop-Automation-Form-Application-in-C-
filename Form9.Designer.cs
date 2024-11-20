namespace WinFormsApp3
{
    partial class Form9
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form9));
            dataGridView1 = new DataGridView();
            ekle_btn = new Button();
            stok_turev_txt = new TextBox();
            stokaciklama_txt = new TextBox();
            textBox3 = new TextBox();
            textBox1 = new TextBox();
            button1 = new Button();
            textBox2 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            menuStrip1 = new MenuStrip();
            diğerToolStripMenuItem = new ToolStripMenuItem();
            stokAraToolStripMenuItem = new ToolStripMenuItem();
            barkodİşlemleriToolStripMenuItem1 = new ToolStripMenuItem();
            kaydetToolStripMenuItem = new ToolStripMenuItem();
            cvsDosyasıOlarakKaydetToolStripMenuItem = new ToolStripMenuItem();
            sayfaYenileToolStripMenuItem = new ToolStripMenuItem();
            yardımToolStripMenuItem = new ToolStripMenuItem();
            hakkındaToolStripMenuItem = new ToolStripMenuItem();
            lisansToolStripMenuItem = new ToolStripMenuItem();
            işlemGeçmişiToolStripMenuItem = new ToolStripMenuItem();
            çıkışToolStripMenuItem = new ToolStripMenuItem();
            numericUpDown1 = new NumericUpDown();
            button6 = new Button();
            saveFileDialog1 = new SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Bottom;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 495);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1902, 538);
            dataGridView1.TabIndex = 11;
            // 
            // ekle_btn
            // 
            ekle_btn.BackColor = Color.Green;
            ekle_btn.FlatStyle = FlatStyle.Popup;
            ekle_btn.Location = new Point(25, 339);
            ekle_btn.Name = "ekle_btn";
            ekle_btn.Size = new Size(162, 48);
            ekle_btn.TabIndex = 1;
            ekle_btn.Text = "Ekle";
            ekle_btn.UseVisualStyleBackColor = false;
            ekle_btn.Click += ekle_btn_Click;
            ekle_btn.MouseEnter += ekle_btn_MouseEnter;
            ekle_btn.MouseLeave += ekle_btn_MouseLeave;
            // 
            // stok_turev_txt
            // 
            stok_turev_txt.Location = new Point(25, 107);
            stok_turev_txt.Name = "stok_turev_txt";
            stok_turev_txt.PlaceholderText = "Stok Tür / Türevi";
            stok_turev_txt.Size = new Size(543, 27);
            stok_turev_txt.TabIndex = 2;
            // 
            // stokaciklama_txt
            // 
            stokaciklama_txt.Location = new Point(25, 150);
            stokaciklama_txt.Name = "stokaciklama_txt";
            stokaciklama_txt.PlaceholderText = "Boyut";
            stokaciklama_txt.Size = new Size(543, 27);
            stokaciklama_txt.TabIndex = 3;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(25, 192);
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Ağırlık ";
            textBox3.Size = new Size(543, 27);
            textBox3.TabIndex = 4;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(25, 236);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Konum";
            textBox1.Size = new Size(543, 27);
            textBox1.TabIndex = 5;
            // 
            // button1
            // 
            button1.BackColor = Color.Red;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(216, 339);
            button1.Name = "button1";
            button1.Size = new Size(162, 48);
            button1.TabIndex = 6;
            button1.Text = "Sil";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            button1.MouseEnter += button1_MouseEnter;
            button1.MouseLeave += button1_MouseLeave;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(25, 65);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Stok ID";
            textBox2.Size = new Size(543, 27);
            textBox2.TabIndex = 7;
            // 
            // button2
            // 
            button2.BackColor = Color.Yellow;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Location = new Point(406, 339);
            button2.Name = "button2";
            button2.Size = new Size(162, 48);
            button2.TabIndex = 8;
            button2.Text = "Güncelle";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            button2.MouseEnter += button2_MouseEnter;
            button2.MouseLeave += button2_MouseLeave;
            // 
            // button3
            // 
            button3.Location = new Point(1126, 231);
            button3.Name = "button3";
            button3.Size = new Size(162, 48);
            button3.TabIndex = 9;
            button3.Text = "Stok Ara";
            button3.UseVisualStyleBackColor = true;
            button3.Visible = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(1126, 171);
            button4.Name = "button4";
            button4.Size = new Size(162, 48);
            button4.TabIndex = 10;
            button4.Text = "Barkod İşlemleri";
            button4.UseVisualStyleBackColor = true;
            button4.Visible = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.Red;
            button5.Location = new Point(1294, 249);
            button5.Name = "button5";
            button5.Size = new Size(116, 30);
            button5.TabIndex = 17;
            button5.Text = "Çıkış";
            button5.UseVisualStyleBackColor = false;
            button5.Visible = false;
            button5.Click += button5_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ActiveCaptionText;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { diğerToolStripMenuItem, yardımToolStripMenuItem, çıkışToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1902, 28);
            menuStrip1.TabIndex = 18;
            menuStrip1.Text = "menuStrip1";
            // 
            // diğerToolStripMenuItem
            // 
            diğerToolStripMenuItem.BackColor = SystemColors.HighlightText;
            diğerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { stokAraToolStripMenuItem, barkodİşlemleriToolStripMenuItem1, kaydetToolStripMenuItem, sayfaYenileToolStripMenuItem });
            diğerToolStripMenuItem.Margin = new Padding(0, 0, 5, 0);
            diğerToolStripMenuItem.Name = "diğerToolStripMenuItem";
            diğerToolStripMenuItem.Size = new Size(60, 24);
            diğerToolStripMenuItem.Text = "Diğer";
            // 
            // stokAraToolStripMenuItem
            // 
            stokAraToolStripMenuItem.Name = "stokAraToolStripMenuItem";
            stokAraToolStripMenuItem.Size = new Size(199, 26);
            stokAraToolStripMenuItem.Text = "Stok Ara";
            stokAraToolStripMenuItem.Click += stokAraToolStripMenuItem_Click;
            // 
            // barkodİşlemleriToolStripMenuItem1
            // 
            barkodİşlemleriToolStripMenuItem1.Name = "barkodİşlemleriToolStripMenuItem1";
            barkodİşlemleriToolStripMenuItem1.Size = new Size(199, 26);
            barkodİşlemleriToolStripMenuItem1.Text = "Barkod İşlemleri";
            barkodİşlemleriToolStripMenuItem1.Click += barkodİşlemleriToolStripMenuItem1_Click;
            // 
            // kaydetToolStripMenuItem
            // 
            kaydetToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cvsDosyasıOlarakKaydetToolStripMenuItem });
            kaydetToolStripMenuItem.Name = "kaydetToolStripMenuItem";
            kaydetToolStripMenuItem.Size = new Size(199, 26);
            kaydetToolStripMenuItem.Text = "Kaydet";
            // 
            // cvsDosyasıOlarakKaydetToolStripMenuItem
            // 
            cvsDosyasıOlarakKaydetToolStripMenuItem.Name = "cvsDosyasıOlarakKaydetToolStripMenuItem";
            cvsDosyasıOlarakKaydetToolStripMenuItem.Size = new Size(266, 26);
            cvsDosyasıOlarakKaydetToolStripMenuItem.Text = "Cvs Dosyası Olarak Kaydet";
            cvsDosyasıOlarakKaydetToolStripMenuItem.Click += cvsDosyasıOlarakKaydetToolStripMenuItem_Click;
            // 
            // sayfaYenileToolStripMenuItem
            // 
            sayfaYenileToolStripMenuItem.Name = "sayfaYenileToolStripMenuItem";
            sayfaYenileToolStripMenuItem.Size = new Size(199, 26);
            sayfaYenileToolStripMenuItem.Text = "Sayfayı Yenile";
            sayfaYenileToolStripMenuItem.Click += sayfaYenileToolStripMenuItem_Click;
            // 
            // yardımToolStripMenuItem
            // 
            yardımToolStripMenuItem.BackColor = SystemColors.HighlightText;
            yardımToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { hakkındaToolStripMenuItem, işlemGeçmişiToolStripMenuItem });
            yardımToolStripMenuItem.Margin = new Padding(0, 0, 5, 0);
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
            // çıkışToolStripMenuItem
            // 
            çıkışToolStripMenuItem.BackColor = Color.Red;
            çıkışToolStripMenuItem.ForeColor = SystemColors.ActiveCaptionText;
            çıkışToolStripMenuItem.Name = "çıkışToolStripMenuItem";
            çıkışToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
            çıkışToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
            çıkışToolStripMenuItem.Size = new Size(53, 24);
            çıkışToolStripMenuItem.Text = "Çıkış";
            çıkışToolStripMenuItem.TextDirection = ToolStripTextDirection.Horizontal;
            çıkışToolStripMenuItem.Click += çıkışToolStripMenuItem_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(25, 284);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(543, 27);
            numericUpDown1.TabIndex = 19;
            // 
            // button6
            // 
            button6.Location = new Point(1294, 167);
            button6.Name = "button6";
            button6.Size = new Size(148, 52);
            button6.TabIndex = 20;
            button6.Text = "button6";
            button6.UseVisualStyleBackColor = true;
            button6.Visible = false;
            button6.Click += button6_Click;
            // 
            // Form9
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1033);
            Controls.Add(button6);
            Controls.Add(numericUpDown1);
            Controls.Add(menuStrip1);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(textBox2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(textBox3);
            Controls.Add(stokaciklama_txt);
            Controls.Add(stok_turev_txt);
            Controls.Add(ekle_btn);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(1920, 1080);
            MinimumSize = new Size(1918, 1018);
            Name = "Form9";
            Text = "Eva Life Otomasyon - Stok";
            Load += Form9_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button ekle_btn;
        private TextBox stok_turev_txt;
        private TextBox stokaciklama_txt;
        private TextBox textBox3;
        private TextBox textBox1;
        private Button button1;
        private TextBox textBox2;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem diğerToolStripMenuItem;
        private ToolStripMenuItem stokAraToolStripMenuItem;
        private ToolStripMenuItem barkodİşlemleriToolStripMenuItem1;
        private ToolStripMenuItem yardımToolStripMenuItem;
        private ToolStripMenuItem hakkındaToolStripMenuItem;
        private ToolStripMenuItem lisansToolStripMenuItem;
        private ToolStripMenuItem çıkışToolStripMenuItem;
        private ToolStripMenuItem işlemGeçmişiToolStripMenuItem;
        private NumericUpDown numericUpDown1;
        private Button button6;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem kaydetToolStripMenuItem;
        private ToolStripMenuItem cvsDosyasıOlarakKaydetToolStripMenuItem;
        private ToolStripMenuItem sayfaYenileToolStripMenuItem;
    }
}