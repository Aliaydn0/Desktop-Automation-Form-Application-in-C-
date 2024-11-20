using MySql.Data.MySqlClient;
using System.Data;

namespace WinFormsApp3

{
    public partial class Form1 : Form
    {
        // MySQL baðlantý dizesi
        private MySqlConnection baglanti = new MySqlConnection("Server=192.168.1.212;Database=otomasyon;Uid=Tester74;Pwd=Tester74;");
        // Ýlk ve sonraki þifreler
        private string firstPassword = "34360120272024";
        private string secondPassword = "1029384756";




        public Form1()
        {
            InitializeComponent();
        }















        private bool KullaniciDogrula(string kullaniciAdi, string sifre)
        {
            string query = "SELECT * FROM kullanici WHERE kullanici_adi = @kullanici_adi AND sifre = @sifre";
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            cmd.Parameters.AddWithValue("@kullanici_adi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@sifre", sifre);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                MySqlDataReader reader = cmd.ExecuteReader();
                bool sonuc = reader.Read();

                baglanti.Close();
                return sonuc;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Baðlantý hatasý: " + ex.Message);
                return false;
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
        }





        //textbox placeholder ayarlarý
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Kullanýcý Adý")
            {
                textBox1.Text = "";

                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Kullanýcý Adý";

                textBox1.ForeColor = Color.Silver;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Þifre")
            {
                textBox2.Text = "";

                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Þifre";

                textBox2.ForeColor = Color.Silver;
            }
        }
        //textbox placeholder ayarlarý




        private void button1_Click(object sender, EventArgs e)
        {

            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;

            if (KullaniciDogrula(kullaniciAdi, sifre))
            {
                // Kullanýcýya göre ilgili formu aç
                switch (kullaniciAdi)
                {
                    case var k when k.Contains("sipariþ"):
                        Form2 form2 = new Form2(kullaniciAdi); // Sadece Form2'yi aç
                        form2.Show();
                        break;
                    case var k when k.Contains("dijitalbaski"):
                        Form3 form3 = new Form3(kullaniciAdi);
                        form3.Show();
                        break;
                    case var k when k.Contains("kalender"):
                        Form5 form5 = new Form5(kullaniciAdi);
                        form5.Show();
                        break;
                    case var k when k.Contains("kesim") || k.Contains("dikim"):
                        Form6 form6 = new Form6(kullaniciAdi);
                        form6.Show();
                        break;
                    case var k when k.Contains("paket") || k.Contains("sevkiyat"):
                        Form7 form7 = new Form7(kullaniciAdi);
                        form7.Show();
                        break;
                    case var k when k.Contains("levent6060") || k.Contains("stok1"):
                        Form9 form9 = new Form9();
                        form9.Show();
                        break;
                    case "admin":
                    case "tester":
                        Form4 form4 = new Form4();
                        form4.Show();
                        break;
                    default:
                        MessageBox.Show("Yetkisiz kullanýcý.");
                        return;
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalý kullanýcý adý veya þifre.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            DateTime lockDate = new DateTime(2025, 1, 1); // Formun kilitleneceði tarih

            // Kilit tarihinden sonra mý?
            if (currentDate >= lockDate)
            {
                if (!RequestPassword())
                {
                    MessageBox.Show("Yanlýþ þifre. Program kapatýlýyor.");
                    Application.Exit(); // Yanlýþ þifre durumunda uygulamayý kapat
                }
            }
        }


        // Þifre doðrulama metodu
        private bool RequestPassword()
        {
            string promptMessage = "Bu formu kullanmaya devam etmek için þifre girin:";
            string inputPassword = Microsoft.VisualBasic.Interaction.InputBox(promptMessage, "Þifre Giriþi", "");

            // Mevcut tarihe göre hangi þifre gerektiðini kontrol et
            DateTime currentDate = DateTime.Now;
            DateTime nextLockDate = new DateTime(2025, 7, 1); // Ýkinci þifre gereksinimi (6 ay sonra)

            if (currentDate < nextLockDate && inputPassword == firstPassword)
            {
                return true; // Ýlk þifre doðruysa
            }
            else if (currentDate >= nextLockDate && inputPassword == secondPassword)
            {
                return true; // Ýkinci þifre doðruysa
            }
            return false; // Þifre yanlýþsa
        }



    }
}
