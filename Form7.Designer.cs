namespace WinFormsApp3
{
    partial class Form7
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form7));
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            dataGridView2 = new DataGridView();
            button4 = new Button();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            hakkındaToolStripMenuItem = new ToolStripMenuItem();
            hakkındaToolStripMenuItem1 = new ToolStripMenuItem();
            lisansToolStripMenuItem = new ToolStripMenuItem();
            işlemGeçmişiToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 71);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Parti No";
            textBox1.Size = new Size(448, 27);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 115);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Boyut";
            textBox2.Size = new Size(448, 27);
            textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(12, 157);
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Adet";
            textBox3.Size = new Size(448, 27);
            textBox3.TabIndex = 4;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(12, 201);
            textBox4.Name = "textBox4";
            textBox4.PlaceholderText = "Açıklama";
            textBox4.Size = new Size(448, 27);
            textBox4.TabIndex = 5;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(466, 1);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1510, 440);
            dataGridView1.TabIndex = 8;
            // 
            // button1
            // 
            button1.BackColor = Color.Green;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(12, 293);
            button1.Name = "button1";
            button1.Size = new Size(133, 38);
            button1.TabIndex = 1;
            button1.Text = "Ekle";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            button1.MouseEnter += button1_MouseEnter_1;
            button1.MouseLeave += button1_MouseLeave;
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Location = new Point(162, 293);
            button2.Name = "button2";
            button2.Size = new Size(133, 38);
            button2.TabIndex = 6;
            button2.Text = "Sil";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            button2.MouseEnter += button2_MouseEnter;
            button2.MouseLeave += button2_MouseLeave;
            // 
            // button3
            // 
            button3.BackColor = Color.Yellow;
            button3.FlatStyle = FlatStyle.Popup;
            button3.Location = new Point(310, 293);
            button3.Name = "button3";
            button3.Size = new Size(133, 38);
            button3.TabIndex = 7;
            button3.Text = "Güncelle";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            button3.MouseEnter += button3_MouseEnter;
            button3.MouseLeave += button3_MouseLeave;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(-2, 447);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(1944, 544);
            dataGridView2.TabIndex = 9;
            // 
            // button4
            // 
            button4.BackColor = Color.Red;
            button4.Location = new Point(-2, 412);
            button4.Name = "button4";
            button4.Size = new Size(468, 29);
            button4.TabIndex = 10;
            button4.Text = "Çıkış";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, hakkındaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1902, 28);
            menuStrip1.TabIndex = 11;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(14, 24);
            // 
            // hakkındaToolStripMenuItem
            // 
            hakkındaToolStripMenuItem.BackColor = SystemColors.HighlightText;
            hakkındaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { hakkındaToolStripMenuItem1, işlemGeçmişiToolStripMenuItem });
            hakkındaToolStripMenuItem.Name = "hakkındaToolStripMenuItem";
            hakkındaToolStripMenuItem.Size = new Size(69, 24);
            hakkındaToolStripMenuItem.Text = "Yardım";
            // 
            // hakkındaToolStripMenuItem1
            // 
            hakkındaToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { lisansToolStripMenuItem });
            hakkındaToolStripMenuItem1.Name = "hakkındaToolStripMenuItem1";
            hakkındaToolStripMenuItem1.Size = new Size(183, 26);
            hakkındaToolStripMenuItem1.Text = "Hakkında";
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
            // Form7
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1033);
            Controls.Add(button4);
            Controls.Add(dataGridView2);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form7";
            Text = "Eva Life Otomasyon ";
            Load += Form7_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private DataGridView dataGridView1;
        private Button button1;
        private Button button2;
        private Button button3;
        private DataGridView dataGridView2;
        private Button button4;
        private Label label1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem hakkındaToolStripMenuItem;
        private ToolStripMenuItem hakkındaToolStripMenuItem1;
        private ToolStripMenuItem lisansToolStripMenuItem;
        private ToolStripMenuItem işlemGeçmişiToolStripMenuItem;
    }
}