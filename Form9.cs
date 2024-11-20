using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style; // Excel stilleri için gerekli


namespace WinFormsApp3
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
            dataGridView1.ScrollBars = ScrollBars.Both;





            LoadHistory();

            // Form kapandığında geçmişi kaydet
            this.FormClosed += (s, e) => SaveHistory();


            this.FormClosed += (s, e) =>
            {
                if (form11 != null && !form11.IsDisposed && form11.Visible)
                    form11.Close();

                if (form10 != null && !form10.IsDisposed && form10.Visible)
                    form10.Close();
            };


        }





        private void Form9_Load(object sender, EventArgs e)
        {
            LoadData(); // Form yüklendiğinde verileri yükle

        }



        private void SaveHistory()
        {
            string directoryPath = @"C:\txt"; // Klasör yolu
            string filePath = Path.Combine(directoryPath, "form9gecmis.txt"); // Dosya yolu

            try
            {
                // Klasör yoksa oluştur
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Dosya yoksa oluştur
                if (!File.Exists(filePath))
                {
                    using (File.Create(filePath)) { }
                }

                // İşlem geçmişini dosyaya yaz
                File.WriteAllLines(filePath, islemGecmisi);
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"Giriş/Çıkış hatası: {ioEx.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException authEx)
            {
                MessageBox.Show($"Erişim hatası: {authEx.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Geçmiş kaydedilemedi: {ex.Message}\nHata Kodu: {ex.GetType()}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void LoadHistory()
        {
            string filePath = @"C:\txt\form9gecmis.txt"; // Dosya yolu

            if (File.Exists(filePath))
            {
                try
                {
                    // Dosyadaki geçmişi oku ve listeye ekle
                    islemGecmisi = File.ReadAllLines(filePath).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Geçmiş yüklenemedi: {ex.Message}\nHata Kodu: {ex.GetType()}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Geçmiş dosyası bulunamadı. Yeni bir dosya oluşturulacak.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    using (File.Create(filePath)) { }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Dosya oluşturulurken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        private void LoadData()
        {
            string connectionString = "Server= 192.168.1.212;Database=stoklar;Uid=Tester74;Pwd=Tester74;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Add ORDER BY clause to sort by tarih descending
                    string query = "SELECT stokid, partino, aciklama AS 'Stok Tür/Türevi', aciklama2 AS 'Stok Boyutu', aciklama3 AS 'Stok Ağırlığı/Miktarı', aciklama4 AS 'Stok Yeri/Konumu', tarih FROM stok ORDER BY tarih DESC"; // Sort by tarih descending

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable); // Verileri DataTable'a doldur

                        // DataGridView'e ata
                        dataGridView1.DataSource = dataTable;

                        // Kolonları sırala
                        dataGridView1.Columns["stokid"].DisplayIndex = 0; // İlk olarak stokid
                        dataGridView1.Columns["partino"].DisplayIndex = 1; // İkinci olarak partino
                        dataGridView1.Columns["Stok Tür/Türevi"].DisplayIndex = 2; // Üçüncü olarak stok türü
                        dataGridView1.Columns["Stok Boyutu"].DisplayIndex = 3; // Dördüncü olarak stok boyutu
                        dataGridView1.Columns["Stok Ağırlığı/Miktarı"].DisplayIndex = 4; // Beşinci olarak stok ağırlığı/miktarı
                        dataGridView1.Columns["Stok Yeri/Konumu"].DisplayIndex = 5; // Altıncı olarak stok yeri
                        dataGridView1.Columns["tarih"].DisplayIndex = 6; // Son olarak tarih
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri yükleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ekle_btn_Click(object sender, EventArgs e)
        {
            // Textbox'lardan değerleri al
            string stokTurev = stok_turev_txt.Text.Trim();
            string stokAciklama = stokaciklama_txt.Text.Trim();
            string aciklama3 = textBox3.Text.Trim();
            string aciklama4 = textBox1.Text.Trim();
            int miktar = (int)numericUpDown1.Value; // Kullanıcının belirttiği miktar

            // Boş değer kontrolü
            if (string.IsNullOrWhiteSpace(stokTurev) || string.IsNullOrWhiteSpace(stokAciklama) ||
                string.IsNullOrWhiteSpace(aciklama3) || string.IsNullOrWhiteSpace(aciklama4) || miktar <= 0)
            {
                MessageBox.Show("Eksik veya hatalı veri girişi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Hatalı giriş varsa işlemi durdur
            }

            // Partino'yu 8600 ile başlayan bir değer olarak oluştur
            string partino = "8600" + DateTime.Now.Ticks.ToString(); // Benzersiz bir partino oluşturmak için zamanı kullanıyoruz

            // MySQL bağlantı bilgilerini girin
            string connectionString = "Server=192.168.1.212;Database=stoklar;Uid=Tester74;Pwd=Tester74;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // SQL komutu ile verileri ekleyin
                    string query = "INSERT INTO stok (aciklama, aciklama2, aciklama3, aciklama4, partino, tarih) " +
                                   "VALUES (@aciklama, @aciklama2, @aciklama3, @aciklama4, @partino, @tarih)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Belirtilen miktar kadar ekleme işlemi gerçekleştirin
                        for (int i = 0; i < miktar; i++)
                        {
                            // Parametreleri ekleyin
                            cmd.Parameters.Clear(); // Parametreleri temizle
                            cmd.Parameters.AddWithValue("@aciklama", stokTurev);
                            cmd.Parameters.AddWithValue("@aciklama2", stokAciklama);
                            cmd.Parameters.AddWithValue("@aciklama3", aciklama3);
                            cmd.Parameters.AddWithValue("@aciklama4", aciklama4);
                            cmd.Parameters.AddWithValue("@partino", partino + "_" + i); // Her bir ürün için farklı partino
                            cmd.Parameters.AddWithValue("@tarih", DateTime.Now);

                            // Sorguyu çalıştırın
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show($"{miktar} adet stok başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // Stok eklendikten sonra verileri yükle
                LoadData();

                // TextBox'ları temizle
                stok_turev_txt.Clear();
                stokaciklama_txt.Clear();
                textBox3.Clear();
                textBox1.Clear();
                numericUpDown1.Value = 1; // Miktarı sıfırlayın

                islemGecmisi.Add($"Stok Eklendi: {miktar} adet, {stokTurev}, {stokAciklama}, {aciklama3}, {aciklama4}, {DateTime.Now}");
                SaveHistory();
            }
        }



        private List<string> islemGecmisi = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            // TextBox2'den stokid değerini al
            string stokIdStr = textBox2.Text.Trim();

            // Boş değer kontrolü
            if (string.IsNullOrWhiteSpace(stokIdStr))
            {
                MessageBox.Show("Lütfen bir stok ID'si girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Hatalı giriş varsa işlemi durdur
            }

            // MySQL bağlantı bilgilerini girin
            string connectionString = "Server= 192.168.1.212;Database=stoklar;Uid=Tester74;Pwd=Tester74;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // SQL komutu ile ürünü silin
                    string query = "DELETE FROM stok WHERE stokid = @stokid";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Parametreleri ekleyin
                        cmd.Parameters.AddWithValue("@stokid", stokIdStr);

                        // Sorguyu çalıştırın
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Stok başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox2.Clear(); // TextBox'u temizle
                            islemGecmisi.Add($"Stok Silindi: StokID = {stokIdStr}, Silme Tarihi = {DateTime.Now}");
                            SaveHistory();
                        }
                        else
                        {
                            MessageBox.Show("Silinecek Stok bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // Stok silindikten sonra verileri yükle
                LoadData();



            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // DataGridView'de seçili satır kontrolü
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçili satırı al
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Seçili stokid'i al
                int stokId = Convert.ToInt32(selectedRow.Cells["stokid"].Value);

                // TextBox'lardan değerleri al
                string aciklama = stok_turev_txt.Text.Trim();
                string stokAciklama2 = stokaciklama_txt.Text.Trim();
                string aciklama3 = textBox3.Text.Trim();
                string aciklama4 = textBox1.Text.Trim();

                // MySQL bağlantı bilgilerini girin
                string connectionString = "Server= 192.168.1.212;Database=stoklar;Uid=Tester74;Pwd=Tester74;";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Güncelleme sorgusu
                        string query = "UPDATE stok SET ";

                        // Güncellenecek alanlar için liste
                        List<string> updates = new List<string>();

                        // Boş olmayan TextBox'ları kontrol et ve güncelle

                        if (!string.IsNullOrWhiteSpace(aciklama))
                            updates.Add("aciklama = @aciklama");


                        if (!string.IsNullOrWhiteSpace(stokAciklama2))
                            updates.Add("aciklama2 = @aciklama2");

                        if (!string.IsNullOrWhiteSpace(aciklama3))
                            updates.Add("aciklama3 = @aciklama3");

                        if (!string.IsNullOrWhiteSpace(aciklama4))
                            updates.Add("aciklama4 = @aciklama4");

                        // Eğer güncellenecek alan varsa
                        if (updates.Count > 0)
                        {
                            query += string.Join(", ", updates) + " WHERE stokid = @stokid"; // Güncelleme sorgusunu tamamla

                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                // Parametreleri ekle

                                if (!string.IsNullOrWhiteSpace(aciklama))
                                    cmd.Parameters.AddWithValue("@aciklama", aciklama);

                                if (!string.IsNullOrWhiteSpace(stokAciklama2))
                                    cmd.Parameters.AddWithValue("@aciklama2", stokAciklama2);

                                if (!string.IsNullOrWhiteSpace(aciklama3))
                                    cmd.Parameters.AddWithValue("@aciklama3", aciklama3);

                                if (!string.IsNullOrWhiteSpace(aciklama4))
                                    cmd.Parameters.AddWithValue("@aciklama4", aciklama4);

                                cmd.Parameters.AddWithValue("@stokid", stokId); // stokid parametresi

                                // Sorguyu çalıştır
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Stok başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    islemGecmisi.Add($"Stok Güncellendi: StokID = {stokId}, Yeni Değerler: {aciklama}, {stokAciklama2}, {aciklama3}, {aciklama4}, Güncelleme Tarihi = {DateTime.Now}");
                                    SaveHistory();
                                }
                                else
                                {
                                    MessageBox.Show("Güncellenecek stok bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hiçbir değer güncellenmedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veritabanı bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Güncellemeden sonra verileri yükle
                    LoadData();
                }

                // TextBox'ları temizle
                stok_turev_txt.Clear();
                stokaciklama_txt.Clear();
                textBox3.Clear();
                textBox1.Clear();


            }
            else
            {
                MessageBox.Show("Lütfen güncellemek için bir stok seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ekle_btn_MouseEnter(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
        }

        private void ekle_btn_MouseLeave(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            stok_turev_txt.Enabled = false;
            stokaciklama_txt.Enabled = false;
            textBox3.Enabled = false;
            textBox1.Enabled = false;
            numericUpDown1.Enabled = false;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            stok_turev_txt.Enabled = true;
            stokaciklama_txt.Enabled = true;
            textBox3.Enabled = true;
            textBox1.Enabled = true;
            numericUpDown1.Enabled = true;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            numericUpDown1.Enabled = false;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
            numericUpDown1.Enabled = true;
        }

        private Form11 form11;

        private void button3_Click(object sender, EventArgs e)
        {
            // Eğer form11 zaten açıksa, onu ön plana çıkar
            if (form11 != null && !form11.IsDisposed)
            {
                form11.BringToFront(); // Formu ön plana getir
                form11.Activate(); // Formu aktif hale getir
            }
            else
            {
                // Form11'i oluştur ve sakla
                form11 = new Form11();
                form11.Show();
            }
        }




        private Form10 form10;
        private void button4_Click(object sender, EventArgs e)
        {
            // Eğer form11 zaten açıksa, onu ön plana çıkar
            if (form10 != null && !form10.IsDisposed)
            {
                form10.BringToFront(); // Formu ön plana getir
                form10.Activate(); // Formu aktif hale getir
            }
            else
            {
                // Form11'i oluştur ve sakla
                form10 = new Form10();
                form10.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Uyarı mesajını göster
            DialogResult dialogResult = MessageBox.Show("Oturumunuz Sonlandırılacak. Emin misiniz?",
                "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                // Eğer kullanıcı "Evet" butonuna tıklarsa
                this.Close(); // Mevcut Form'u gizle
                Form1 form1 = new Form1(); // Form1'i oluştur
                form1.Show(); // Form1'i göster


            }
        }

        private void barkodİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void stokAraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Eğer form11 zaten açıksa, onu ön plana çıkar
            if (form11 != null && !form11.IsDisposed)
            {
                form11.BringToFront(); // Formu ön plana getir
                form11.Activate(); // Formu aktif hale getir
            }
            else
            {
                // Form11'i oluştur ve sakla
                form11 = new Form11();
                form11.Show();
            }
        }

        private void barkodİşlemleriToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Eğer form11 zaten açıksa, onu ön plana çıkar
            if (form10 != null && !form10.IsDisposed)
            {
                form10.BringToFront(); // Formu ön plana getir
                form10.Activate(); // Formu aktif hale getir
            }
            else
            {
                // Form11'i oluştur ve sakla
                form10 = new Form10();
                form10.Show();
            }
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Uyarı mesajını göster
            DialogResult dialogResult = MessageBox.Show("Oturumunuz Sonlandırılacak. Emin misiniz?",
                "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                // Eğer kullanıcı "Evet" butonuna tıklarsa
                this.Close(); // Mevcut Form'u gizle
                Form1 form1 = new Form1(); // Form1'i oluştur
                form1.Show(); // Form1'i göster


            }
        }

        private void işlemGeçmişiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // İşlem geçmişini birleştir ve her bir kaydı yeni satırda göster
            string history = string.Join(Environment.NewLine, islemGecmisi);

            // İşlem geçmişini göstermek için yeni bir form oluştur
            Form historyForm = new Form();
            historyForm.Text = "İşlem Geçmişi";
            historyForm.Size = new Size(400, 300);

            // RichTextBox ekleyin ve işlem geçmişini ayarlayın
            RichTextBox richTextBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Text = history
            };

            historyForm.Controls.Add(richTextBox);
            historyForm.ShowDialog();
        }

        private void lisansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Form12'yi oluştur ve göster
            Form12 form12 = new Form12();
            form12.Show(); // Form12'yi aç
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV Dosyası|*.csv|Tüm Dosyalar|*.*";
                saveFileDialog.Title = "CSV Dosyası Olarak Kaydet";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // UTF-8 kodlaması ile StreamWriter oluştur
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, new UTF8Encoding(true)))
                        {
                            // DataGridView başlıklarını yaz
                            var columnCount = dataGridView1.Columns.Count;
                            for (int i = 0; i < columnCount; i++)
                            {
                                writer.Write("\"" + dataGridView1.Columns[i].HeaderText + "\"");
                                if (i < columnCount - 1)
                                    writer.Write(","); // Virgül ile ayır
                            }
                            writer.WriteLine();

                            // DataGridView verilerini yaz
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                for (int i = 0; i < columnCount; i++)
                                {
                                    // Hücre değerini al
                                    string cellValue;

                                    // Tarih kontrolü yap
                                    if (row.Cells[i].Value is DateTime dateTimeValue)
                                    {
                                        // Tarihi istediğiniz formatta yazın (örneğin: "dd.MM.yyyy")
                                        cellValue = dateTimeValue.ToString("dd.MM.yyyy");
                                    }
                                    else
                                    {
                                        cellValue = row.Cells[i].Value?.ToString() ?? string.Empty;
                                    }

                                    // Tırnak içinde yaz ve içteki tırnakları kaçır
                                    writer.Write("\"" + cellValue.Replace("\"", "\"\"") + "\"");
                                    if (i < columnCount - 1)
                                        writer.Write(","); // Virgül ile ayır
                                }
                                writer.WriteLine();
                            }
                        }

                        MessageBox.Show("Veri başarıyla CSV dosyasına aktarıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veri kaydetme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cvsDosyasıOlarakKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV Dosyası|*.csv|Tüm Dosyalar|*.*";
                saveFileDialog.Title = "CSV Dosyası Olarak Kaydet";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // UTF-8 kodlaması ile StreamWriter oluştur
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, new UTF8Encoding(true)))
                        {
                            // DataGridView başlıklarını yaz
                            var columnCount = dataGridView1.Columns.Count;
                            for (int i = 0; i < columnCount; i++)
                            {
                                writer.Write("\"" + dataGridView1.Columns[i].HeaderText + "\"");
                                if (i < columnCount - 1)
                                    writer.Write(","); // Virgül ile ayır
                            }
                            writer.WriteLine();

                            // DataGridView verilerini yaz
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                for (int i = 0; i < columnCount; i++)
                                {
                                    // Hücre değerini al
                                    string cellValue;

                                    // Tarih kontrolü yap
                                    if (row.Cells[i].Value is DateTime dateTimeValue)
                                    {
                                        // Tarihi istediğiniz formatta yazın (örneğin: "dd.MM.yyyy")
                                        cellValue = dateTimeValue.ToString("dd.MM.yyyy");
                                    }
                                    else
                                    {
                                        cellValue = row.Cells[i].Value?.ToString() ?? string.Empty;
                                    }

                                    // Tırnak içinde yaz ve içteki tırnakları kaçır
                                    writer.Write("\"" + cellValue.Replace("\"", "\"\"") + "\"");
                                    if (i < columnCount - 1)
                                        writer.Write(","); // Virgül ile ayır
                                }
                                writer.WriteLine();
                            } 
                        }

                        MessageBox.Show("Tablo başarıyla CSV dosyasına aktarıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Tablo kaydetme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void sayfaYenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Formu yenilemek için:
            this.Refresh(); // Formu veya kontrolü yeniden çiz
        }

    
    }
}



















