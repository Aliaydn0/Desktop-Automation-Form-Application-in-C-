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

namespace WinFormsApp3
{
    public partial class Form4 : Form
    {
        private MySqlConnection baglanti = new MySqlConnection("Server=192.168.1.212;Database=otomasyon;Uid=Tester74;Pwd=Tester74;");
        public Form4()
        {
            InitializeComponent();
            dataGridView1.ScrollBars = ScrollBars.Both;



            
        }

        private void Form4_Load(object sender, EventArgs e)
        {

           


            // ComboBox'ı doldur
            comboBox2.Items.Add("Sipariş");
            comboBox2.Items.Add("Dijital Baskı");
            comboBox2.Items.Add("Kalender");
            comboBox2.Items.Add("Dikim & Kesim");
            comboBox2.Items.Add("Paket & Sevkiyat");
            comboBox2.Items.Add("Giden Sevkiyat");

            // Diğer ayarlar
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView2.ScrollBars = ScrollBars.Both;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = comboBox2.SelectedItem.ToString();
            string query = string.Empty;

            // Seçeneklere göre sorguyu belirle
            switch (selectedOption)
            {
                case "Sipariş":
                    query = "SELECT * FROM siparis";
                    break;
                case "Dijital Baskı":
                    query = "SELECT * FROM dijitalbaski";
                    break;
                case "Kalender":
                    query = "SELECT * FROM kalender";
                    break;
                case "Dikim & Kesim":
                    query = "SELECT * FROM dikim_kesim";
                    break;
                case "Paket & Sevkiyat":
                    query = "SELECT * FROM paket_sevkiyat";
                    break;
                case "Giden Sevkiyat":
                    query = "SELECT * FROM giden_sevkiyat";
                    break;
                default:
                    MessageBox.Show("Geçersiz seçim.");
                    return;
            }

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, baglanti);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // DataGridView2'yi güncelle
                dataGridView2.DataSource = dataTable;

                // Veri yoksa kullanıcıya bilgi ver
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Seçilen kriterlere uygun veri bulunamadı.");
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




        private void button1_Click(object sender, EventArgs e)
        {
            bool isVisible = radioButton1.Visible; // radioButton1'in mevcut görünürlüğünü al

            // Tüm radioButton'ların görünürlüğünü değiştir
            radioButton1.Visible = !isVisible;
            radioButton2.Visible = !isVisible;
            radioButton3.Visible = !isVisible;
            radioButton4.Visible = !isVisible;
            radioButton5.Visible = !isVisible;
            radioButton6.Visible = !isVisible;

            // radioButton6 seçili yap
            radioButton6.Checked = true;


        }



        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = false; 

            string searchValue = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Lütfen aramak istediğiniz değeri girin.");
                return;
            }

            string searchColumn = "urun_id"; // Varsayılan olarak urun_id üzerinden arama yap
            if (radioButton1.Checked)
                searchColumn = "urun_adi";
            else if (radioButton2.Checked)
                searchColumn = "parti_no";
            else if (radioButton3.Checked)
                searchColumn = "boyut";
            else if (radioButton4.Checked)
                searchColumn = "adet";
            else if (radioButton6.Checked)
                searchColumn = "urun_id";
            else if (radioButton5.Checked)
                searchColumn = "bolge"; // Eğer bolge'yi kullanacaksanız.

            // Sorguyu güncelle
            string query = $@"
    SELECT urun_id, parti_no, urun_adi, boyut, adet, bolge, aciklama, kullanici, tarih FROM siparis WHERE {searchColumn} LIKE @searchValue
    UNION ALL
    SELECT urun_id, parti_no, urun_adi, boyut, adet, bolge, aciklama, kullanici, tarih FROM dijitalbaski WHERE {searchColumn} LIKE @searchValue
    UNION ALL
    SELECT urun_id, parti_no, urun_adi, boyut, adet, bolge, aciklama, kullanici, tarih FROM kalender WHERE {searchColumn} LIKE @searchValue
    UNION ALL
    SELECT urun_id, parti_no, urun_adi, boyut, adet, bolge, aciklama, kullanici, tarih FROM dikim_kesim WHERE {searchColumn} LIKE @searchValue
    UNION ALL
    SELECT urun_id, parti_no, urun_adi, boyut, adet, bolge, aciklama, kullanici, tarih FROM paket_sevkiyat WHERE {searchColumn} LIKE @searchValue
    UNION ALL
    SELECT urun_id, parti_no, urun_adi, boyut, adet, bolge, aciklama, kullanici, tarih FROM giden_sevkiyat WHERE {searchColumn} LIKE @searchValue";

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                MySqlCommand cmd = new MySqlCommand(query, baglanti);

                // Eğer adet ya da urun_id üzerinden arama yapılacaksa, tam eşleşme kullan, diğerlerinde esnek arama yap.
                if (searchColumn == "adet" || searchColumn == "urun_id")
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                else
                    cmd.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Bu arama ile kayıt bulunamadı.");
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







        private void button3_Click(object sender, EventArgs e)
        {

            dataGridView1.Visible = false;
            dataGridView2.Visible = true;

            string selectedValue = comboBox2.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedValue))
            {
                MessageBox.Show("Lütfen ComboBox'tan bir değer seçin.");
                return;
            }

            string query = string.Empty;

            // Seçeneklere göre sorguyu belirle
            switch (selectedValue)
            {
                case "Sipariş":
                    query = "SELECT urun_id, parti_no, urun_adi, boyut, adet, bolge, aciklama, kullanici, tarih FROM siparis";
                    break;
                case "Dijital Baskı":
                    query = "SELECT urun_id, parti_no, urun_adi, boyut , adet,  bolge, aciklama, kullanici AS kullanici, tarih FROM dijitalbaski";
                    break;
                case "Kalender":
                    query = "SELECT urun_id, parti_no, urun_adi,boyut, adet,  bolge, aciklama, kullanici AS kullanici, tarih FROM kalender";
                    break;
                case "Dikim & Kesim":
                    query = "SELECT urun_id, parti_no, urun_adi, boyut, adet, bolge, aciklama, kullanici AS kullanici, tarih FROM dikim_kesim";
                    break;
                case "Paket & Sevkiyat":
                    query = "SELECT urun_id, parti_no, urun_adi, boyut , adet,  bolge, aciklama, kullanici AS kullanici, tarih FROM paket_sevkiyat";
                    break;
                case "Giden Sevkiyat":
                    query = "SELECT urun_id, parti_no, urun_adi, boyut , adet,  bolge, aciklama,kullanici AS kullanici, tarih FROM giden_sevkiyat";
                    break;
                default:
                    MessageBox.Show("Geçersiz seçim.");
                    return;
            }

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, baglanti);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // DataGridView2'nin sütunlarını otomatik oluştur
                dataGridView2.AutoGenerateColumns = true;

                // DataGridView'e veri kaynağını ata
                dataGridView2.DataSource = dataTable;

                // Veri yoksa kullanıcıya bilgi ver
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Seçilen kriterlere uygun veri bulunamadı.");
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
    }
}
   










