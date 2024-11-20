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

namespace WinFormsApp3
{
    public partial class Form6 : Form
    {

        private List<string> islemGecmisi = new List<string>(); // İşlem geçmişini tutmak için liste
        private MySqlConnection baglanti = new MySqlConnection("Server=192.168.1.212;Database=otomasyon;Uid=Tester74;Pwd=Tester74;");
        private string currentUser; // Uygulamaya giriş yapan kullanıcı

        public Form6(string kullanici)
        {
            InitializeComponent();
            currentUser = kullanici; // Kullanıcı bilgisini al
            this.FormClosed += (s, e) => SaveHistory();
            LoadHistory(); // Önceki işlem geçmişini yükle
        }




        private void SaveHistory()
        {
            string filePath = @"\\Server\ortak arşiv\txt\form6gecmis.txt";

            try
            {
                // İşlem geçmişini dosyaya yaz
                File.WriteAllLines(filePath, islemGecmisi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Geçmiş kaydedilemedi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void LoadHistory()
        {
            string filePath = @"\\Server\ortak arşiv\txt\form6gecmis.txt";

            if (File.Exists(filePath))
            {
                try
                {
                    // Dosyadaki geçmişi oku ve listeye ekle
                    islemGecmisi = File.ReadAllLines(filePath).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Geçmiş yüklenemedi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private void Form6_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string partiNo = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(partiNo))
            {
                MessageBox.Show("Lütfen geçerli bir parti no girin.");
                return;
            }

            // Daha önce eklenip eklenmediğini kontrol et
            string queryCheck = "SELECT COUNT(*) FROM dikim_kesim WHERE parti_no = @partiNo";

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                MySqlCommand cmdCheck = new MySqlCommand(queryCheck, baglanti);
                cmdCheck.Parameters.AddWithValue("@partiNo", partiNo);
                int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Bu sipariş zaten Diki & Kesim'e verilmiş.");
                    return;
                }

                // Sipariş tablosundan veriyi al
                string querySelect = "SELECT urun_adi, urun_id, adet, boyut, bolge, aciklama FROM siparis WHERE parti_no = @partiNo";
                MySqlCommand cmdSelect = new MySqlCommand(querySelect, baglanti);
                cmdSelect.Parameters.AddWithValue("@partiNo", partiNo);
                MySqlDataReader reader = cmdSelect.ExecuteReader();

                if (reader.Read())
                {
                    string urunAdi = reader["urun_adi"].ToString();
                    string urunId = reader["urun_id"].ToString(); // Ürün ID'sini al
                    string adet = reader["adet"].ToString();
                    string boyut = reader["boyut"].ToString();
                    string bolge = reader["bolge"].ToString();
                    string aciklama = reader["aciklama"].ToString();

                    reader.Close();

                    // kalender tablosuna ekleme sorgusu
                    string queryInsert = "INSERT INTO dikim_kesim (urun_adi, parti_no, urun_id, adet, boyut, bolge, aciklama, tarih, kullanici) VALUES (@urunAdi, @partiNo, @urunId, @adet, @boyut, @bolge, @aciklama, @tarih, @kullanici)";

                    MySqlCommand cmdInsert = new MySqlCommand(queryInsert, baglanti);
                    cmdInsert.Parameters.AddWithValue("@urunAdi", urunAdi);
                    cmdInsert.Parameters.AddWithValue("@partiNo", partiNo);
                    cmdInsert.Parameters.AddWithValue("@urunId", urunId);
                    cmdInsert.Parameters.AddWithValue("@adet", adet);
                    cmdInsert.Parameters.AddWithValue("@boyut", boyut);
                    cmdInsert.Parameters.AddWithValue("@bolge", bolge);
                    cmdInsert.Parameters.AddWithValue("@aciklama", aciklama);
                    cmdInsert.Parameters.AddWithValue("@tarih", DateTime.Now);
                    cmdInsert.Parameters.AddWithValue("@kullanici", currentUser);

                    cmdInsert.ExecuteNonQuery();
                    MessageBox.Show("Sipariş başarıyla eklendi.");
                    islemGecmisi.Add($"{DateTime.Now}: {urunId} - {urunAdi}  - {partiNo} - {adet} - {boyut}- {bolge}- {currentUser} -{aciklama} ürünü eklendi.");
                    SaveHistory(); // Geçmişi kaydet


                    // TextBox'ı temizle
                    textBox1.Clear();

                    // DataGridView'i güncelle
                    LoadDataGridView();
                }
                else
                {
                    MessageBox.Show("Bu parti no'ya ait sipariş bulunamadı.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Veritabanı hatası: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
        }
        private void LoadDataGridView()
        {
            // DataGridView'i güncellemek için gereken kodlar
            string querySelectAll = "SELECT urun_id, parti_no, urun_adi, boyut, adet, bolge, aciklama, kullanici, tarih FROM dikim_kesim";

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(querySelectAll, baglanti);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // DataGridView'i ayarla
                dataGridView1.DataSource = dataTable;

                // Kullanıcı ID'sini gizle
                if (dataGridView1.Columns.Contains("id")) // Eğer ID sütunu varsa
                {
                    dataGridView1.Columns["id"].Visible = false;
                }

                // Sütun sıralamasını ayarla
                dataGridView1.Columns["urun_id"].DisplayIndex = 0;
                dataGridView1.Columns["parti_no"].DisplayIndex = 1;
                dataGridView1.Columns["urun_adi"].DisplayIndex = 2;
                dataGridView1.Columns["boyut"].DisplayIndex = 3;
                dataGridView1.Columns["adet"].DisplayIndex = 4;
                dataGridView1.Columns["bolge"].DisplayIndex = 5;
                dataGridView1.Columns["aciklama"].DisplayIndex = 6;
                dataGridView1.Columns["kullanici"].DisplayIndex = 7;
                dataGridView1.Columns["tarih"].DisplayIndex = 8;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Veritabanı hatası: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Seçili satır yoksa uyarı göster
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silmek için bir satır seçin.");
                return;
            }

            // Seçili satırdan urun_id'yi al
            string selectedUrunId = dataGridView1.SelectedRows[0].Cells["urun_id"].Value.ToString();

            // Silme işlemi için onay al
            DialogResult dialogResult = MessageBox.Show("Seçili ürünü silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                string mysqlConnectionString = "Server=192.168.1.212;Database=otomasyon;Uid=Tester74;Pwd=Tester74;";

                using (MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString))
                {
                    try
                    {
                        mysqlConnection.Open();

                        // Ürünün adını al
                        string checkQuery = "SELECT urun_adi FROM dikim_kesim WHERE urun_id = @urun_id";
                        MySqlCommand checkCmd = new MySqlCommand(checkQuery, mysqlConnection);
                        checkCmd.Parameters.AddWithValue("@urun_id", selectedUrunId);


                        // Ürün adını al
                        string urunAdi = checkCmd.ExecuteScalar()?.ToString();

                        if (!string.IsNullOrEmpty(urunAdi))
                        {
                            // Ürün varsa silme işlemini yap
                            string deleteQuery = "DELETE FROM dikim_kesim WHERE urun_id = @urun_id"; // Burayı dijitalbaski ile değiştirin
                            MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, mysqlConnection);
                            deleteCmd.Parameters.AddWithValue("@urun_id", selectedUrunId);

                            int rowsAffected = deleteCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Sipariş Başarıyla Silindi.");
                                islemGecmisi.Add($"{DateTime.Now}: {selectedUrunId} - {urunAdi} - ürünü silindi.");
                                SaveHistory(); // Geçmişi kaydet
                                LoadDataGridView(); // Verileri yenile
                            }
                            else
                            {
                                MessageBox.Show("Silme işlemi başarısız oldu.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Sipariş Kaydı Bulunamadı.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // Seçili satır yoksa uyarı göster
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellemek için bir satır seçin.");
                return;
            }

            // Seçili satırdan urun_id'yi al
            string selectedUrunId = dataGridView1.SelectedRows[0].Cells["urun_id"].Value.ToString();

            // TextBox'lardan alınan değerleri al
            string yeniAdet = textBox3.Text.Trim();
            string yeniAciklama = textBox4.Text.Trim();
            string yeniBoyut = textBox2.Text.Trim();

            // Geçerli değerler kontrolü
            if (string.IsNullOrEmpty(yeniAdet) && string.IsNullOrEmpty(yeniAciklama) && string.IsNullOrEmpty(yeniBoyut))
            {
                MessageBox.Show("Güncellemek için en az bir alanı doldurun.");
                return;
            }

            string mysqlConnectionString = "Server=192.168.1.212;Database=otomasyon;Uid=Tester74;Pwd=Tester74;";

            using (MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString))
            {
                try
                {
                    mysqlConnection.Open();

                    // Güncelleme sorgusu
                    string updateQuery = "UPDATE dikim_kesim SET ";
                    bool hasSetClause = false;

                    if (!string.IsNullOrEmpty(yeniAdet))
                    {
                        updateQuery += "adet = @adet";
                        hasSetClause = true;
                    }

                    if (!string.IsNullOrEmpty(yeniAciklama))
                    {
                        if (hasSetClause) updateQuery += ", ";
                        updateQuery += "aciklama = @aciklama";
                        hasSetClause = true;
                    }

                    if (!string.IsNullOrEmpty(yeniBoyut))
                    {
                        if (hasSetClause) updateQuery += ", ";
                        updateQuery += "boyut = @boyut";
                    }

                    updateQuery += " WHERE urun_id = @urun_id";

                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, mysqlConnection);
                    if (!string.IsNullOrEmpty(yeniAdet)) updateCmd.Parameters.AddWithValue("@adet", yeniAdet);
                    if (!string.IsNullOrEmpty(yeniAciklama)) updateCmd.Parameters.AddWithValue("@aciklama", yeniAciklama);
                    if (!string.IsNullOrEmpty(yeniBoyut)) updateCmd.Parameters.AddWithValue("@boyut", yeniBoyut);
                    updateCmd.Parameters.AddWithValue("@urun_id", selectedUrunId);

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sipariş başarıyla güncellendi.");
                        islemGecmisi.Add($"{DateTime.Now}: {selectedUrunId}, Adet/ {yeniAdet},Boyut/ {yeniBoyut},Aciklama/ {yeniAciklama} - ürünü güncellendi.");
                        SaveHistory(); // Geçmişi kaydet
                        LoadDataGridView(); // Verileri yenile

                        // TextBox'ları temizle
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Güncelleme işlemi başarısız oldu.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            textBox2.Enabled = false; // comboBox1'i gizle
            textBox3.Enabled = false; // comboBox1'i gizle
            textBox4.Enabled = false; // comboBox1'i gizle
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            textBox2.Enabled = true; // comboBox1'i gizle
            textBox3.Enabled = true; // comboBox1'i gizle
            textBox4.Enabled = true; // comboBox1'i gizle
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            textBox2.Enabled = false; // comboBox1'i gizle
            textBox3.Enabled = false; // comboBox1'i gizle
            textBox1.Enabled = false; // comboBox1'i gizle
            textBox4.Enabled = false; // comboBox1'i gizle
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            textBox2.Enabled = true; // comboBox1'i gizle
            textBox3.Enabled = true; // comboBox1'i gizle
            textBox1.Enabled = true; // comboBox1'i gizle
            textBox4.Enabled = true; // comboBox1'i gizle
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            textBox1.Enabled = false; // comboBox1'i gizle
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            textBox1.Enabled = true; // comboBox1'i gizle
        }

        private void button4_Click(object sender, EventArgs e)
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
    }
}
