namespace WinFormsApp3
{
    partial class Form8
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form8));
            textBox1 = new TextBox();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            button2 = new Button();
            textBox2 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(133, 57);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Parti No Giriniz";
            textBox1.Size = new Size(466, 27);
            textBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(133, 133);
            button1.Name = "button1";
            button1.Size = new Size(133, 38);
            button1.TabIndex = 0;
            button1.Text = "Barkod Oluştur";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(133, 195);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(639, 497);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Popup;
            button2.Location = new Point(133, 687);
            button2.Name = "button2";
            button2.Size = new Size(133, 38);
            button2.TabIndex = 3;
            button2.Text = "Yazdır";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(133, 90);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Müşteri Adı";
            textBox2.Size = new Size(466, 27);
            textBox2.TabIndex = 2;
            textBox2.Visible = false;
            // 
            // Form8
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(762, 834);
            Controls.Add(textBox2);
            Controls.Add(button2);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(780, 881);
            MinimumSize = new Size(780, 881);
            Name = "Form8";
            Text = "Eva Life Otomasyon ";
            Load += Form8_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
        private PictureBox pictureBox1;
        private Button button2;
        private TextBox textBox2;
        private Label label1;
    }
}