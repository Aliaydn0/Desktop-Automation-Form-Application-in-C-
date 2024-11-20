using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using MySql.Data.MySqlClient; // MySQL bağlantısı için eklemelisiniz

namespace WinFormsApp3
{
    public partial class Form8 : Form
    {
        private Bitmap currentBarcodeBitmap; // Barkod bitmap'ini saklamak için
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        


        private void button1_Click(object sender, EventArgs e)
        {

            // textBox1'deki veri (parti no)
            string partiNo = textBox1.Text;

            if (!string.IsNullOrEmpty(partiNo))
            {
                // Veritabanı bağlantısı
                string connectionString = "Server=192.168.1.212;Database=otomasyon;Uid=Tester74;Pwd=Tester74;";
                string query = "SELECT urun_id, urun_adi, parti_no, tarih, boyut, adet, bolge, kullanici, aciklama FROM siparis WHERE parti_no = @partino";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@partino", partiNo);

                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // İlk kaydı al ve barkod oluştur
                        string urunId = reader["urun_id"].ToString();
                        string urunAdi = reader["urun_adi"].ToString();
                        string aciklama = reader["aciklama"].ToString();
                        string boyut = reader["boyut"].ToString();
                        string adet = reader["adet"].ToString();
                        string bolge = reader["bolge"].ToString();
                        string kullanici = reader["kullanici"].ToString();
                        string tarih = reader["tarih"].ToString();

                        // Barkod oluşturma
                        var barcodeWriter = new BarcodeWriterPixelData
                        {
                            Format = BarcodeFormat.CODE_128,
                            Options = new EncodingOptions
                            {
                                Width = 500,
                                Height = 200,
                                Margin = 10
                            }
                        };

                        // Barkod verisini oluştur
                        var pixelData = barcodeWriter.Write(partiNo);

                        // Barkodun altında metin eklemek için bitmap oluştur
                        using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height + 210, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                        {
                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                g.Clear(Color.White);
                            }

                            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                                System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                            try
                            {
                                System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                            }
                            finally
                            {
                                bitmap.UnlockBits(bitmapData);
                            }

                            // Barkodun altına metin eklemek için Graphics nesnesi kullan
                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                g.DrawString($"Parti No: {partiNo}", new Font("Arial", 12), Brushes.Black, new PointF(90, pixelData.Height + 5));
                                g.DrawString($"Ürün ID: {urunId}", new Font("Arial", 10), Brushes.Black, new PointF(90, pixelData.Height + 25));
                                g.DrawString($"Ürün Adı: {urunAdi}", new Font("Arial", 10), Brushes.Black, new PointF(90, pixelData.Height + 45));
                                g.DrawString($"Açıklama: {aciklama}", new Font("Arial", 10), Brushes.Black, new PointF(90, pixelData.Height + 65));
                                g.DrawString($"Boyut: {boyut}", new Font("Arial", 10), Brushes.Black, new PointF(90, pixelData.Height + 85));
                                g.DrawString($"Adet: {adet}", new Font("Arial", 10), Brushes.Black, new PointF(90, pixelData.Height + 105));
                                g.DrawString($"Bölge: {bolge}", new Font("Arial", 10), Brushes.Black, new PointF(90, pixelData.Height + 125));
                                //g.DrawString($"Kullanıcı: {kullanici}", new Font("Arial", 10), Brushes.Black, new PointF(10, pixelData.Height + 145));
                                g.DrawString($"Tarih: {tarih}", new Font("Arial", 10), Brushes.Black, new PointF(90, pixelData.Height + 145));
                            }

                            // PictureBox'ta göster
                            pictureBox1.Image = new Bitmap(bitmap);
                            currentBarcodeBitmap = new Bitmap(bitmap); // Barkodu sakla
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bu parti numarasıyla eşleşen bir kayıt bulunamadı.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir parti numarası girin.");
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {

            if (currentBarcodeBitmap != null)
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += new PrintPageEventHandler(PrintDoc_PrintPage);

                // Yazıcı seçimi ve yazdırma işlemi
                PrintDialog printDialog = new PrintDialog
                {
                    Document = printDoc
                };

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print(); // Yazdır
                }
            }
            else
            {
                MessageBox.Show("Önce barkodu oluşturmalısınız.");
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Barkodu yazdır
            if (currentBarcodeBitmap != null)
            {
                e.Graphics.DrawImage(currentBarcodeBitmap, new Point(0, 0));
            }
        }
    }
}
