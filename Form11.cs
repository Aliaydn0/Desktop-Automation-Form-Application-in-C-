using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // MySQL bağlantısı için eklemelisiniz

namespace WinFormsApp3
{
    public partial class Form11 : Form
    {
        private bool radioButtonsVisible = false; // Radio button'ların görünüm durumunu tutacak değişken


        private string connectionString = "server= 192.168.1.212;database=stoklar;uid=Tester74;pwd=Tester74;"; // Veritabanı bağlantı dizesi
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            label2.Text = "Toplam Kayıt Sayısı: 0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Radio button'ların görünüm durumunu tersine çevir
            radioButtonsVisible = !radioButtonsVisible;

            // Radio button'ların görünümünü ayarla
            radioButton1.Visible = radioButtonsVisible;
            radioButton2.Visible = radioButtonsVisible;
            radioButton3.Visible = radioButtonsVisible;
            radioButton4.Visible = radioButtonsVisible;
            radioButton5.Visible = radioButtonsVisible;
            radioButton6.Visible = radioButtonsVisible;

        }







        private void button2_Click(object sender, EventArgs e)
        {
            string query = "";
            string searchValue = textBox1.Text;

            // Seçili olan radioButton'a göre sorguyu oluştur
            if (radioButton1.Checked)
            {
                query = "SELECT * FROM stok WHERE aciklama LIKE @searchValue";
            }
            else if (radioButton2.Checked)
            {
                query = "SELECT * FROM stok WHERE aciklama2 LIKE @searchValue";
            }
            else if (radioButton4.Checked)
            {
                query = "SELECT * FROM stok WHERE aciklama3 LIKE @searchValue";
            }
            else if (radioButton6.Checked)
            {
                query = "SELECT * FROM stok WHERE partino LIKE @searchValue";
            }
            else if (radioButton3.Checked)
            {
                query = "SELECT * FROM stok WHERE aciklama4 LIKE @searchValue";
            }
            else if (radioButton5.Checked) // stokid ile arama yapacak
            {
                query = "SELECT * FROM stok WHERE stokid = @searchValue"; // Arama ifadesi değiştirildi
            }
            else
            {
                MessageBox.Show("Lütfen bir Filtre seçin.");
                return;
            }

            // Veritabanı bağlantısını kur ve sorguyu çalıştır
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Eğer stokid ile arama yapılıyorsa, sadece tam eşleşme için değer ayarlanır
                        if (radioButton5.Checked)
                        {
                            cmd.Parameters.AddWithValue("@searchValue", searchValue); // Sadece stokid eşleşmesini kontrol etmek için
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%"); // Diğerleri için LIKE kullanılıyor
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable); // Verileri DataTable'a doldur

                            // DataGridView'ı verilerle doldur
                            dataGridView1.DataSource = dataTable;

                            // Toplam kayıt sayısını Label'a yazdır
                            label2.Text = "Toplam Kayıt Sayısı: " + dataTable.Rows.Count;

                            // Kolonları sırala
                            dataGridView1.Columns["stokid"].DisplayIndex = 0; // İlk olarak stokid
                            dataGridView1.Columns["partino"].DisplayIndex = 1; // İkinci olarak partino
                            dataGridView1.Columns["aciklama"].HeaderText = "Stok Tür/Türevi"; // Stok türü için başlık ayarlama
                            dataGridView1.Columns["aciklama"].DisplayIndex = 2; // Üçüncü olarak stok türü
                            dataGridView1.Columns["aciklama2"].HeaderText = "Stok Boyutu"; // Stok boyutu için başlık ayarlama
                            dataGridView1.Columns["aciklama2"].DisplayIndex = 3; // Dördüncü olarak stok boyutu
                            dataGridView1.Columns["aciklama3"].HeaderText = "Stok Ağırlığı/Miktarı"; // Stok ağırlığı/miktarı için başlık ayarlama
                            dataGridView1.Columns["aciklama3"].DisplayIndex = 4; // Beşinci olarak stok ağırlığı/miktarı
                            dataGridView1.Columns["aciklama4"].HeaderText = "Stok Yeri/Konumu"; // Stok yeri için başlık ayarlama
                            dataGridView1.Columns["aciklama4"].DisplayIndex = 5; // Altıncı olarak stok yeri
                            dataGridView1.Columns["tarih"].DisplayIndex = 6; // Son olarak tarih
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
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
    }
}
  