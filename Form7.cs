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
    public partial class Form7 : Form
    {
        private List<string> islemGecmisi = new List<string>(); // İşlem geçmişini tutmak için liste
        private MySqlConnection baglanti = new MySqlConnection("Server=192.168.1.212;Database=otomasyon;Uid=Tester74;Pwd=Tester74;");
        private string currentUser; // Uygulamaya giriş yapan kullanıcı
        public Form7(string kullanici)
        {
            InitializeComponent();
            currentUser = kullanici; // Kullanıcı bilgisini al
            dataGridView1.ScrollBars = ScrollBars.Both;
            this.FormClosed += (s, e) => SaveHistory();
            LoadHistory(); // Önceki işlem geçmişini yükle
        }




        private void SaveHistory()
        {
            string filePath = @"\\Server\ortak arşiv\txt\form7gecmis.txt";

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
            string filePath = @"\\Server\ortak arşiv\txt\form7gecmis.txt";

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














        private void Form7_Load(object sender, EventArgs e)
        {
            LoadDataGridView(); // Bu satırın burada olduğundan emin olun
            LoadDataGridViewGiden(); // Giden sevkiyatları yükle
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string partiNo = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(partiNo))
            {
                MessageBox.Show("Lütfen geçerli bir parti no girin.");
                return;
            }

            // Sorgular
            string queryCheckPaket = "SELECT COUNT(*) FROM paket_sevkiyat WHERE parti_no = @partiNo";
            string queryCheckGiden = "SELECT COUNT(*) FROM giden_sevkiyat WHERE parti_no = @partiNo";
            string querySelect = "SELECT urun_adi, urun_id, adet, boyut, bolge, aciklama FROM siparis WHERE parti_no = @partiNo";

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                // Kontrolleri yap
                MySqlCommand cmdCheckPaket = new MySqlCommand(queryCheckPaket, baglanti);
                cmdCheckPaket.Parameters.AddWithValue("@partiNo", partiNo);
                int countPaket = Convert.ToInt32(cmdCheckPaket.ExecuteScalar());

                MySqlCommand cmdCheckGiden = new MySqlCommand(queryCheckGiden, baglanti);
                cmdCheckGiden.Parameters.AddWithValue("@partiNo", partiNo);
                int countGiden = Convert.ToInt32(cmdCheckGiden.ExecuteScalar());

                if (countGiden > 0)
                {
                    MessageBox.Show("Bu ürün zaten sevk edilenler arasında.");
                    return;
                }

                MySqlCommand cmdSelect = new MySqlCommand(querySelect, baglanti);
                cmdSelect.Parameters.AddWithValue("@partiNo", partiNo);
                MySqlDataReader reader = cmdSelect.ExecuteReader();

                if (reader.Read())
                {
                    string urunAdi = reader["urun_adi"].ToString();
                    string urunId = reader["urun_id"].ToString();
                    string adet = reader["adet"].ToString();
                    string boyut = reader["boyut"].ToString();
                    string bolge = reader["bolge"].ToString();
                    string aciklama = reader["aciklama"].ToString();

                    reader.Close();

                    if (countPaket > 0)
                    {
                        // Ürün daha önce paket_sevkiyat tablosuna eklenmişse, kullanıcıdan onay iste.
                        DialogResult result = MessageBox.Show(
                            $"Bu ürün daha önce paketlenmiş olarak bulundu. \n\n" +
                            $"Ürün Adı: {urunAdi}\nÜrün ID: {urunId}\nAdet: {adet}\nBoyut: {boyut}\nBölge: {bolge}\nAçıklama: {aciklama}\n\n" +
                            "Bu ürünü sevk edilenler arasına eklemek istiyor musunuz?",
                            "Ürün Zaten Paketlenmiş",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            // Onay verilirse giden_sevkiyat'a ekle
                            string queryInsertGiden = "INSERT INTO giden_sevkiyat(urun_adi, parti_no, urun_id, adet, boyut, bolge, aciklama, tarih, kullanici) VALUES (@urunAdi, @partiNo, @urunId, @adet, @boyut, @bolge, @aciklama, @tarih, @kullanici)";
                            MySqlCommand cmdInsertGiden = new MySqlCommand(queryInsertGiden, baglanti);
                            cmdInsertGiden.Parameters.AddWithValue("@urunAdi", urunAdi);
                            cmdInsertGiden.Parameters.AddWithValue("@partiNo", partiNo);
                            cmdInsertGiden.Parameters.AddWithValue("@urunId", urunId);
                            cmdInsertGiden.Parameters.AddWithValue("@adet", adet);
                            cmdInsertGiden.Parameters.AddWithValue("@boyut", boyut);
                            cmdInsertGiden.Parameters.AddWithValue("@bolge", bolge);
                            cmdInsertGiden.Parameters.AddWithValue("@aciklama", aciklama);
                            cmdInsertGiden.Parameters.AddWithValue("@tarih", DateTime.Now);
                            cmdInsertGiden.Parameters.AddWithValue("@kullanici", currentUser);

                            cmdInsertGiden.ExecuteNonQuery();
                            MessageBox.Show("Ürün Giden Sevkiyat tablosuna başarıyla eklendi.");
                            islemGecmisi.Add($"{DateTime.Now}: {urunId} - {urunAdi}  - {partiNo} - {adet} - {boyut}- {bolge}- {currentUser} -{aciklama} ürünü eklendi.");
                            SaveHistory(); // Geçmişi kaydet

                        }
                    }
                    else
                    {
                        // Ürün paket_sevkiyat tablosuna eklenmemişse, önce oraya ekleyelim.
                        string queryInsertPaket = "INSERT INTO paket_sevkiyat(urun_adi, parti_no, urun_id, adet, boyut, bolge, aciklama, tarih, kullanici) VALUES (@urunAdi, @partiNo, @urunId, @adet, @boyut, @bolge, @aciklama, @tarih, @kullanici)";
                        MySqlCommand cmdInsertPaket = new MySqlCommand(queryInsertPaket, baglanti);
                        cmdInsertPaket.Parameters.AddWithValue("@urunAdi", urunAdi);
                        cmdInsertPaket.Parameters.AddWithValue("@partiNo", partiNo);
                        cmdInsertPaket.Parameters.AddWithValue("@urunId", urunId);
                        cmdInsertPaket.Parameters.AddWithValue("@adet", adet);
                        cmdInsertPaket.Parameters.AddWithValue("@boyut", boyut);
                        cmdInsertPaket.Parameters.AddWithValue("@bolge", bolge);
                        cmdInsertPaket.Parameters.AddWithValue("@aciklama", aciklama);
                        cmdInsertPaket.Parameters.AddWithValue("@tarih", DateTime.Now);
                        cmdInsertPaket.Parameters.AddWithValue("@kullanici", currentUser);

                        cmdInsertPaket.ExecuteNonQuery();
                        MessageBox.Show("Ürün Paket Sevkiyat tablosuna başarıyla eklendi.");
                        islemGecmisi.Add($"{DateTime.Now}: {urunId} - {urunAdi}  - {partiNo} - {adet} - {boyut}- {bolge}- {currentUser} -{aciklama} ürünü eklendi.");
                        SaveHistory(); // Geçmişi kaydet

                    }

                    LoadDataGridView();
                    LoadDataGridViewGiden(); // Giden sevkiyatları güncelle
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Bu parti no'ya ait sipariş bulunamadı.");
                }

                textBox1.Clear();
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
            string querySelect = "SELECT urun_id, parti_no, urun_adi, adet, boyut, bolge, aciklama, tarih, kullanici FROM paket_sevkiyat";

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(querySelect, baglanti);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Tabloda veri yok.");
                }

                dataGridView1.DataSource = dataTable;
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


        private void LoadDataGridViewGiden()
        {
            string querySelect = "SELECT urun_id, parti_no, urun_adi, adet, boyut, bolge, aciklama, tarih, kullanici FROM giden_sevkiyat";

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(querySelect, baglanti);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView2.DataSource = dataTable;

                dataGridView2.Columns["urun_id"].DisplayIndex = 0;
                dataGridView2.Columns["parti_no"].DisplayIndex = 1;
                dataGridView2.Columns["urun_adi"].DisplayIndex = 2;
                dataGridView2.Columns["adet"].DisplayIndex = 3;
                dataGridView2.Columns["boyut"].DisplayIndex = 4;
                dataGridView2.Columns["bolge"].DisplayIndex = 5;
                dataGridView2.Columns["aciklama"].DisplayIndex = 6;
                dataGridView2.Columns["tarih"].DisplayIndex = 7;
                dataGridView2.Columns["kullanici"].DisplayIndex = 8;
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

            // İki DataGridView'den de seçim yapılmışsa hata ver
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView2.SelectedRows.Count > 0)
            {
                MessageBox.Show("Her iki tablo için de bir ürün seçemezsiniz. Lütfen sadece bir tablo seçin.");
                return;
            }

            // Seçili satır yoksa uyarı göster
            if (dataGridView1.SelectedRows.Count == 0 && dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silmek için önce bir satır seçin.");
                return;
            }

            // Silmek için onay iste
            var result = MessageBox.Show("Seçilen ürünü silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string urunId = null;
                string urunAdi = null;
                string queryDeleteFromPaket = "DELETE FROM paket_sevkiyat WHERE urun_id = @urunId";
                string queryDeleteFromGiden = "DELETE FROM giden_sevkiyat WHERE urun_id = @urunId";

                try
                {
                    if (baglanti.State == ConnectionState.Closed)
                        baglanti.Open(); // Bağlantıyı aç

                    if (dataGridView1.SelectedRows.Count > 0) // paket_sevkiyat'tan silme
                    {
                        urunId = dataGridView1.SelectedRows[0].Cells["urun_id"].Value.ToString();

                        // Ürün adını al
                        MySqlCommand cmdGetUrunAdi = new MySqlCommand("SELECT urun_adi FROM paket_sevkiyat WHERE urun_id = @urunId", baglanti);
                        cmdGetUrunAdi.Parameters.AddWithValue("@urunId", urunId);
                        urunAdi = cmdGetUrunAdi.ExecuteScalar()?.ToString();

                        // Öncelikle gidensevkiyat'ta varsa sil
                        MySqlCommand cmdCheckGiden = new MySqlCommand("SELECT COUNT(*) FROM giden_sevkiyat WHERE urun_id = @urunId", baglanti);
                        cmdCheckGiden.Parameters.AddWithValue("@urunId", urunId);
                        int gidenCount = Convert.ToInt32(cmdCheckGiden.ExecuteScalar());

                        // Eğer gidensevkiyat'ta varsa sil
                        if (gidenCount > 0)
                        {
                            MySqlCommand cmdDeleteFromGiden = new MySqlCommand(queryDeleteFromGiden, baglanti);
                            cmdDeleteFromGiden.Parameters.AddWithValue("@urunId", urunId);
                            cmdDeleteFromGiden.ExecuteNonQuery();
                            MessageBox.Show($"Ürün Giden Sevkiyat tablosundan silindi: {urunAdi}");
                        }

                        // Paket sevkiyat tablosundan sil
                        MySqlCommand cmdDeleteFromPaket = new MySqlCommand(queryDeleteFromPaket, baglanti);
                        cmdDeleteFromPaket.Parameters.AddWithValue("@urunId", urunId);
                        cmdDeleteFromPaket.ExecuteNonQuery();

                        MessageBox.Show($"Ürün Paket Sevkiyat tablosundan silindi: {urunAdi}");

                        // İşlem geçmişine ekle ve kaydet
                        islemGecmisi.Add($"{DateTime.Now}: {urunId} - {urunAdi} - ürünü silindi.");
                        SaveHistory(); // Geçmişi kaydet
                    }
                    else if (dataGridView2.SelectedRows.Count > 0) // gidensevkiyat'tan silme
                    {
                        urunId = dataGridView2.SelectedRows[0].Cells["urun_id"].Value.ToString();

                        // Ürün adını al
                        MySqlCommand cmdGetUrunAdi = new MySqlCommand("SELECT urun_adi FROM giden_sevkiyat WHERE urun_id = @urunId", baglanti);
                        cmdGetUrunAdi.Parameters.AddWithValue("@urunId", urunId);
                        urunAdi = cmdGetUrunAdi.ExecuteScalar()?.ToString();

                        // Gidensevkiyat tablosundan sil
                        MySqlCommand cmdDeleteFromGiden = new MySqlCommand(queryDeleteFromGiden, baglanti);
                        cmdDeleteFromGiden.Parameters.AddWithValue("@urunId", urunId);
                        cmdDeleteFromGiden.ExecuteNonQuery();

                        MessageBox.Show($"Ürün Giden Sevkiyat tablosundan silindi: {urunAdi}");

                        // İşlem geçmişine ekle ve kaydet
                        islemGecmisi.Add($"{DateTime.Now}: {urunId} - {urunAdi} - ürünü silindi.");
                        SaveHistory(); // Geçmişi kaydet
                    }

                    // Her iki DataGridView'i güncelle
                    LoadDataGridView(); // Paket Sevkiyat verilerini yükle
                    LoadDataGridViewGiden(); // Giden Sevkiyat verilerini yükle
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    if (baglanti.State == ConnectionState.Open)
                        baglanti.Close(); // Bağlantıyı kapat
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // DataGridView'de bir satırın seçili olup olmadığını kontrol et.
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçili satırın indexini al.
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                // Seçili satırdaki urun_id'yi al.
                string urunId = dataGridView1.Rows[selectedIndex].Cells["urun_id"].Value.ToString();

                // Güncelleme sorgusu için gerekli alanlar.
                string queryUpdateSevkiyat = "UPDATE paket_sevkiyat SET ";
                string queryUpdateGidenSevkiyat = "UPDATE giden_sevkiyat SET ";

                // Değişiklik olup olmadığını kontrol eden bayrak.
                bool hasChanges = false;

                // TextBox değerlerini al ve islemGecmisi için değişkenlere ata.
                string yeniBoyut = textBox2.Text;
                string yeniAdet = textBox3.Text;
                string yeniAciklama = textBox4.Text;

                // Boyut bilgisi güncellenmek isteniyor mu?
                if (!string.IsNullOrEmpty(yeniBoyut))
                {
                    queryUpdateSevkiyat += "boyut = @boyut, ";
                    queryUpdateGidenSevkiyat += "boyut = @boyut, ";
                    hasChanges = true;
                }

                // Adet bilgisi güncellenmek isteniyor mu?
                if (!string.IsNullOrEmpty(yeniAdet))
                {
                    queryUpdateSevkiyat += "adet = @adet, ";
                    queryUpdateGidenSevkiyat += "adet = @adet, ";
                    hasChanges = true;
                }

                // Açıklama bilgisi güncellenmek isteniyor mu?
                if (!string.IsNullOrEmpty(yeniAciklama))
                {
                    queryUpdateSevkiyat += "aciklama = @aciklama, ";
                    queryUpdateGidenSevkiyat += "aciklama = @aciklama, ";
                    hasChanges = true;
                }

                // Eğer hiçbir değişiklik yoksa güncellemeyi yapma.
                if (!hasChanges)
                {
                    MessageBox.Show("Lütfen en az bir alanı doldurun.");
                    return;
                }

                // Son virgülleri kaldır ve WHERE koşulunu ekle.
                queryUpdateSevkiyat = queryUpdateSevkiyat.TrimEnd(',', ' ') + " WHERE urun_id = @urunId";
                queryUpdateGidenSevkiyat = queryUpdateGidenSevkiyat.TrimEnd(',', ' ') + " WHERE urun_id = @urunId";

                try
                {
                    if (baglanti.State == ConnectionState.Closed)
                        baglanti.Open();

                    // Sevkiyat tablosu için güncelleme komutu.
                    MySqlCommand cmdUpdateSevkiyat = new MySqlCommand(queryUpdateSevkiyat, baglanti);
                    cmdUpdateSevkiyat.Parameters.AddWithValue("@urunId", urunId);
                    if (!string.IsNullOrEmpty(yeniBoyut))
                        cmdUpdateSevkiyat.Parameters.AddWithValue("@boyut", yeniBoyut);
                    if (!string.IsNullOrEmpty(yeniAdet))
                        cmdUpdateSevkiyat.Parameters.AddWithValue("@adet", yeniAdet);
                    if (!string.IsNullOrEmpty(yeniAciklama))
                        cmdUpdateSevkiyat.Parameters.AddWithValue("@aciklama", yeniAciklama);

                    // Giden sevkiyat tablosu için güncelleme komutu.
                    MySqlCommand cmdUpdateGidenSevkiyat = new MySqlCommand(queryUpdateGidenSevkiyat, baglanti);
                    cmdUpdateGidenSevkiyat.Parameters.AddWithValue("@urunId", urunId);
                    if (!string.IsNullOrEmpty(yeniBoyut))
                        cmdUpdateGidenSevkiyat.Parameters.AddWithValue("@boyut", yeniBoyut);
                    if (!string.IsNullOrEmpty(yeniAdet))
                        cmdUpdateGidenSevkiyat.Parameters.AddWithValue("@adet", yeniAdet);
                    if (!string.IsNullOrEmpty(yeniAciklama))
                        cmdUpdateGidenSevkiyat.Parameters.AddWithValue("@aciklama", yeniAciklama);

                    // Her iki güncelleme sorgusunu çalıştır.
                    cmdUpdateSevkiyat.ExecuteNonQuery();
                    cmdUpdateGidenSevkiyat.ExecuteNonQuery();
                    MessageBox.Show("Ürün bilgileri başarıyla güncellendi.");

                    // İşlem geçmişine ekle ve kaydet.
                    islemGecmisi.Add($"{DateTime.Now}: {urunId}, Adet/ {yeniAdet},Boyut/ {yeniBoyut},Aciklama/ {yeniAciklama} - ürünü güncellendi.");
                    SaveHistory(); // Geçmişi kaydet

                    // DataGridView'i yenile.
                    LoadDataGridView();
                    LoadDataGridViewGiden();

                    // TextBox'ları temizle.
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
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
            else
            {
                MessageBox.Show("Lütfen güncellemek için bir satır seçin.");
            }
        }




        private void button1_MouseEnter_1(object sender, EventArgs e)
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
