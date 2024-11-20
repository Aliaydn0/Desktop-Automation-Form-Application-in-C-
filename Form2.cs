using System;
using System.Data.SqlClient; // MSSQL için
using MySql.Data.MySqlClient; // MySQL için
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;




namespace WinFormsApp3
{
    public partial class Form2 : Form
    {


        private List<string> islemGecmisi = new List<string>(); // İşlem geçmişini tutmak için liste
        private string currentUser; // Uygulamaya giriş yapan kullanıcı

        public Form2(string kullanici)
        {
            InitializeComponent();



            currentUser = kullanici; // Kullanıcı bilgisini al
            comboBox1.Items.Add("");
            comboBox1.Items.Add("İhracat");
            comboBox1.Items.Add("İç Piyasa");
            LoadData(); // Form yüklendiğinde veri tabanından verileri yükle
            dataGridView1.ScrollBars = ScrollBars.Both;

            this.FormClosed += (s, e) => SaveHistory();



            LoadHistory(); // Önceki işlem geçmişini yükle


            this.FormClosed += (s, e) =>
            {
                if (form4 != null && !form4.IsDisposed && form4.Visible)
                    form4.Close();

                if (form8 != null && !form8.IsDisposed && form8.Visible)
                    form8.Close();
            };



        }

        private void SaveHistory()
        {
            string filePath = @"\\Server\ortak arşiv\txt\form2gecmis.txt";

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
            string filePath = @"\\Server\ortak arşiv\txt\form2gecmis.txt";

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



        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void sip_ekle_btn_Click(object sender, EventArgs e)
        {

            string sipGuid = textBox1.Text.Trim(); // TextBox'tan sip_Guid al
            string selectedBolge = comboBox1.SelectedItem?.ToString(); // ComboBox'dan seçilen değer

            // Validation: Check if sipGuid is empty or selectedBolge is null
            if (string.IsNullOrEmpty(sipGuid) || string.IsNullOrEmpty(selectedBolge))
            {
                MessageBox.Show("Lütfen ID ve Bölge Bilgisi Giriniz");
                return; // İşlemi sonlandır
            }

            // MSSQL Bağlantı Dizesi
            string mssqlConnectionString = "Data Source=192.168.1.212;Initial Catalog=MikroDB_V16_EVA23;User ID=Tester74;Password=Tester74;";

            // MySQL Bağlantı Dizesi
            string mysqlConnectionString = "Server=192.168.1.212;Database=otomasyon;Uid=Tester74;Pwd=Tester74;";

            using (SqlConnection mssqlConnection = new SqlConnection(mssqlConnectionString))
            {
                try
                {
                    mssqlConnection.Open(); // Bağlantıyı aç
                    string query = "SELECT sip_stok_kod, sip_HareketGrupKodu2, sip_HareketGrupKodu1, sip_aciklama, sip_aciklama2, sip_HareketGrupKodu3 FROM SIPARISLER WHERE sip_Guid = @sip_Guid";

                    SqlCommand cmd = new SqlCommand(query, mssqlConnection);
                    cmd.Parameters.AddWithValue("@sip_Guid", sipGuid);

                    using (SqlDataReader reader = cmd.ExecuteReader()) // Senkron sorgu çalıştırma
                    {
                        if (reader.Read())
                        {
                            // Veritabanında ürünün zaten var olup olmadığını kontrol et
                            using (MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString))
                            {
                                mysqlConnection.Open(); // MySQL bağlantısını aç
                                string checkQuery = "SELECT COUNT(*) FROM siparis WHERE urun_id = @urun_id"; // Ürünün varlığını kontrol et
                                MySqlCommand checkCmd = new MySqlCommand(checkQuery, mysqlConnection);
                                checkCmd.Parameters.AddWithValue("@urun_id", sipGuid);

                                int productCount = Convert.ToInt32(checkCmd.ExecuteScalar()); // Ürün sayısını al

                                if (productCount > 0)
                                {
                                    MessageBox.Show("Bu ürün Siparişe verilmiş.");
                                    return; // İşlemi sonlandır
                                }

                                // Ürün yoksa ekleme işlemini yap
                                string sip_stok_kod = reader["sip_stok_kod"].ToString();
                                string sip_HareketGrupKodu2 = reader["sip_HareketGrupKodu2"].ToString();
                                string sip_HareketGrupKodu1 = reader["sip_HareketGrupKodu1"].ToString();
                                string sip_aciklama = reader["sip_aciklama"].ToString();
                                string sip_aciklama2 = reader["sip_aciklama2"].ToString();
                                string sip_HareketGrupKodu3 = reader["sip_HareketGrupKodu3"].ToString();

                                // Açıklamaları birleştirme
                                string combinedDescription = $"{sip_aciklama} {sip_aciklama2} {sip_HareketGrupKodu3}";

                                string insertQuery = "INSERT INTO siparis (urun_id, urun_adi, parti_no, tarih, boyut, adet, bolge, kullanici, aciklama) VALUES (@urun_id, @urun_adi, @parti_no, @tarih, @boyut, @adet, @bolge, @kullanici, @aciklama)";
                                MySqlCommand insertCmd = new MySqlCommand(insertQuery, mysqlConnection);

                                insertCmd.Parameters.AddWithValue("@urun_id", sipGuid); // MSSQL'deki sip_Guid
                                insertCmd.Parameters.AddWithValue("@urun_adi", sip_stok_kod);
                                insertCmd.Parameters.AddWithValue("@parti_no", "8600" + DateTime.Now.Ticks); // Parti no sistemden üretilir
                                insertCmd.Parameters.AddWithValue("@tarih", DateTime.Now); // Zaman sistemden çekilir
                                insertCmd.Parameters.AddWithValue("@boyut", sip_HareketGrupKodu2);
                                insertCmd.Parameters.AddWithValue("@adet", sip_HareketGrupKodu1); // Adet
                                insertCmd.Parameters.AddWithValue("@bolge", selectedBolge); // ComboBox'dan alınan değer
                                insertCmd.Parameters.AddWithValue("@kullanici", currentUser); // Uygulamaya giriş yapan kullanıcı
                                insertCmd.Parameters.AddWithValue("@aciklama", combinedDescription); // Birleşik açıklama

                                insertCmd.ExecuteNonQuery(); // Senkron veriyi ekle


                                MessageBox.Show("Sipariş Başarıyla Eklendi.");
                                textBox1.Clear(); // veya textBox1.Text = "";
                                comboBox1.SelectedIndex = -1; // Seçimi kaldırır

                                islemGecmisi.Add($"{DateTime.Now}: {sipGuid} - {sip_stok_kod}  - {sip_HareketGrupKodu2} - {sip_HareketGrupKodu1} - {selectedBolge}- {currentUser}- {combinedDescription} ürünü eklendi.");
                                SaveHistory(); // Geçmişi kaydet
                                LoadData();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }

        // MySQL'den verileri çekip DataGridView'e yükleyen metot
        private void LoadData()
        {
            string mysqlConnectionString = "Server=192.168.1.212;Database=otomasyon;Uid=Tester74;Pwd=Tester74;";
            using (MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString))
            {
                try
                {
                    mysqlConnection.Open();
                    // Sadece gerekli sütunları seç ve belirli bir sırayla getir
                    string selectQuery = "SELECT urun_id, parti_no, urun_adi, boyut, adet, bolge, aciklama, kullanici, tarih FROM siparis ORDER BY urun_id"; // Sıralama burada yapılır
                    MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, mysqlConnection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // DataGridView'in veri kaynağını ayarla
                    dataGridView1.DataSource = dataTable;

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
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Sipariş no")
            {
                textBox1.Text = "";

                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Sipariş no";

                textBox1.ForeColor = Color.Silver;
            }
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Bölge Seçiniz")
            {
                comboBox1.Text = "";

                comboBox1.ForeColor = Color.Black;
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                comboBox1.Text = "Bölge Seçiniz";

                comboBox1.ForeColor = Color.Silver;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TextBox'taki değeri al
            string idToDelete = textBox1.Text.Trim();

            // Eğer TextBox boş değilse, silme işlemi yap
            if (!string.IsNullOrEmpty(idToDelete))
            {
                // Ürünün veritabanında var olup olmadığını kontrol et ve sil
                CheckAndDeleteRecord(idToDelete);
            }
            else
            {
                MessageBox.Show("Silinecek ID girilmedi.");
            }
        }

        private void CheckAndDeleteRecord(string urunId)
        {
            string mysqlConnectionString = "Server=192.168.1.212;Database=otomasyon;Uid=Tester74;Pwd=Tester74;";

            using (MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString))
            {
                try
                {
                    mysqlConnection.Open();

                    // Ürünün varlığını kontrol et
                    string checkQuery = "SELECT urun_adi FROM siparis WHERE urun_id = @urun_id";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, mysqlConnection);
                    checkCmd.Parameters.AddWithValue("@urun_id", urunId);

                    string urunAdi = checkCmd.ExecuteScalar()?.ToString(); // Ürün adını al

                    if (!string.IsNullOrEmpty(urunAdi))
                    {
                        // Ürün varsa silme işlemini yap
                        string deleteQuery = "DELETE FROM siparis WHERE urun_id = @urun_id";
                        MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, mysqlConnection);
                        deleteCmd.Parameters.AddWithValue("@urun_id", urunId);

                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sipariş Başarıyla Silindi.");
                            islemGecmisi.Add($"{DateTime.Now}: {urunId} - {urunAdi} - ürünü silindi.");
                            SaveHistory(); // Geçmişi kaydet
                            LoadData(); // Verileri yenile

                            textBox1.Clear();


                        }
                    }
                    else
                    {
                        MessageBox.Show(" Sipariş Kaydı Bulunamadı.");
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
            comboBox1.Enabled = false; // comboBox1'i gizle
            textBox2.Enabled = false; // comboBox1'i gizle
            textBox5.Enabled = false; // comboBox1'i gizle
            textBox3.Enabled = false; // comboBox1'i gizle
            textBox4.Enabled = false; // comboBox1'i gizle
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            comboBox1.Enabled = true; // comboBox1'i gizle
            textBox2.Enabled = true; // comboBox1'i gizle
            textBox5.Enabled = true; // comboBox1'i gizle
            textBox3.Enabled = true; // comboBox1'i gizle
            textBox4.Enabled = true; // comboBox1'i gizle
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            textBox1.Enabled = false; // textBox1'i gizle
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            textBox1.Enabled = true; // textBox1'i gizle
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Visible = true; // textBox1'i gizle
            button6.Visible = true; // textBox1'i gizle
            button5.Visible = true; // textBox1'i gizle
            button4.Visible = true; // textBox1'i gizle

        }

        private void sip_ekle_btn_MouseEnter(object sender, EventArgs e)
        {

            textBox2.Enabled = false; // comboBox1'i gizle
            textBox5.Enabled = false; // comboBox1'i gizle
            textBox3.Enabled = false; // comboBox1'i gizle
            textBox4.Enabled = false; // comboBox1'i gizle
        }

        private void sip_ekle_btn_MouseLeave(object sender, EventArgs e)
        {
            textBox2.Enabled = true; // comboBox1'i gizle
            textBox5.Enabled = true; // comboBox1'i gizle
            textBox3.Enabled = true; // comboBox1'i gizle
            textBox4.Enabled = true; // comboBox1'i gizle
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen güncellenecek bir satır seçin.");
                return; // İşlemi sonlandır
            }

            // Seçilen satırdan urun_id'yi al
            string selectedUrunId = dataGridView1.SelectedRows[0].Cells["urun_id"].Value.ToString();

            // Güncelleme için verileri al
            string yeniBolge = comboBox1.SelectedItem?.ToString();
            string yeniUrunAdi = textBox2.Text.Trim();
            string yeniAdet = textBox5.Text.Trim();
            string yeniBoyut = textBox3.Text.Trim();
            string yeniAciklama = textBox4.Text.Trim();

            // MySQL bağlantı dizesi
            string mysqlConnectionString = "Server=192.168.1.212;Database=otomasyon;Uid=Tester74;Pwd=Tester74;";

            using (MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString))
            {
                try
                {
                    mysqlConnection.Open();

                    // Güncelleme sorgusu
                    string updateQuery = "UPDATE siparis SET ";
                    string setClause = "";

                    // Eğer ComboBox1 (bolge) değiştirildiyse
                    if (!string.IsNullOrEmpty(yeniBolge))
                    {
                        setClause += "bolge = @bolge, ";
                    }

                    // Eğer textBox2 (urun_adi) değiştirildiyse
                    if (!string.IsNullOrEmpty(yeniUrunAdi))
                    {
                        setClause += "urun_adi = @urun_adi, ";
                    }

                    // Eğer textBox5 (adet) değiştirildiyse
                    if (!string.IsNullOrEmpty(yeniAdet))
                    {
                        setClause += "adet = @adet, ";
                    }

                    // Eğer textBox3 (boyut) değiştirildiyse
                    if (!string.IsNullOrEmpty(yeniBoyut))
                    {
                        setClause += "boyut = @boyut, ";
                    }

                    // Eğer textBox4 (aciklama) değiştirildiyse
                    if (!string.IsNullOrEmpty(yeniAciklama))
                    {
                        setClause += "aciklama = @aciklama, ";
                    }

                    // Eğer hiç güncellenecek veri yoksa
                    if (string.IsNullOrEmpty(setClause))
                    {
                        MessageBox.Show("Hiçbir veri güncellenmedi. Lütfen en az bir alan girin.");
                        return; // İşlemi sonlandır
                    }

                    // Son virgülü kaldır
                    setClause = setClause.TrimEnd(',', ' ');

                    updateQuery += setClause + " WHERE urun_id = @urun_id";

                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, mysqlConnection);
                    updateCmd.Parameters.AddWithValue("@urun_id", selectedUrunId);

                    // Güncellenen alanlar için parametreler
                    if (!string.IsNullOrEmpty(yeniBolge))
                        updateCmd.Parameters.AddWithValue("@bolge", yeniBolge);
                    if (!string.IsNullOrEmpty(yeniUrunAdi))
                        updateCmd.Parameters.AddWithValue("@urun_adi", yeniUrunAdi);
                    if (!string.IsNullOrEmpty(yeniAdet))
                        updateCmd.Parameters.AddWithValue("@adet", yeniAdet);
                    if (!string.IsNullOrEmpty(yeniBoyut))
                        updateCmd.Parameters.AddWithValue("@boyut", yeniBoyut);
                    if (!string.IsNullOrEmpty(yeniAciklama))
                        updateCmd.Parameters.AddWithValue("@aciklama", yeniAciklama);

                    int rowsAffected = updateCmd.ExecuteNonQuery(); // Güncelleme sorgusunu çalıştır

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Güncelleme başarılı.");
                        // TextBox'ları temizle
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox5.Clear();
                        textBox4.Clear();
                        comboBox1.SelectedIndex = -1; // ComboBox'ı temizle

                        islemGecmisi.Add($"{DateTime.Now}: {selectedUrunId}, Bölge/ {yeniBolge} ,AD/ {yeniUrunAdi},Adet/ {yeniAdet},Boyut/ {yeniBoyut},Aciklama/ {yeniAciklama} - ürünü güncellendi.");
                        SaveHistory(); // Geçmişi kaydet

                        LoadData(); // DataGridView'i yenile
                    }
                    else
                    {
                        MessageBox.Show("Güncelleme sırasında bir sorun oluştu.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);



                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button3.Visible = false; // textBox1'i gizle
            button6.Visible = false; // textBox1'i gizle
            button5.Visible = false; // textBox1'i gizle
            button4.Visible = false; // textBox1'i gizle
        }

        private void button7_Click(object sender, EventArgs e)
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


        // Form4'ün bir örneğini saklamak için alan tanımlayın
        private Form4 form4;


        private void button4_Click(object sender, EventArgs e)
        {
            // Eğer form4 zaten açıksa, onu ön plana çıkar
            if (form4 != null && !form4.IsDisposed)
            {
                form4.BringToFront(); // Formu ön plana getir
                form4.Activate(); // Formu aktif hale getir
            }
            else
            {
                // Form4'ü oluştur ve sakla
                form4 = new Form4();
                form4.Show();
            }
        }

        private Form8 form8 = null; // Form8 nesnesi için bir referans

        private void button5_Click(object sender, EventArgs e)
        {
            // Form8 zaten açıksa, onu öne çıkar
            if (form8 != null && !form8.IsDisposed)
            {
                form8.BringToFront(); // Formu öne getir
                form8.Focus(); // Formu odakla
            }
            else
            {
                // Yeni bir Form8 oluştur ve göster
                form8 = new Form8();
                form8.Show();
            }
        }

        private void araToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Eğer form4 zaten açıksa, onu ön plana çıkar
            if (form4 != null && !form4.IsDisposed)
            {
                form4.BringToFront(); // Formu ön plana getir
                form4.Activate(); // Formu aktif hale getir
            }
            else
            {
                // Form4'ü oluştur ve sakla
                form4 = new Form4();
                form4.Show();
            }
        }

        private void barkodİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Form8 zaten açıksa, onu öne çıkar
            if (form8 != null && !form8.IsDisposed)
            {
                form8.BringToFront(); // Formu öne getir
                form8.Focus(); // Formu odakla
            }
            else
            {
                // Yeni bir Form8 oluştur ve göster
                form8 = new Form8();
                form8.Show();
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




