using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using MySql.Data.MySqlClient; // MySQL bağlantısı için eklemelisiniz

namespace WinFormsApp3
{
    public partial class Form10 : Form
    {


        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }



        private Bitmap currentBarcodeBitmap;

        private void button1_Click(object sender, EventArgs e)
        {
            string partiNo = textBox1.Text;

            if (!string.IsNullOrEmpty(partiNo))
            {
                string connectionString = "Server=192.168.1.212;Database=stoklar;Uid=Tester74;Pwd=Tester74;";
                string query = "SELECT aciklama, aciklama2, aciklama3, aciklama4, tarih FROM stok WHERE partino = @partino";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@partino", partiNo);

                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string aciklama = reader["aciklama"].ToString();
                        string aciklama2 = reader["aciklama2"].ToString();
                        string aciklama3 = reader["aciklama3"].ToString();
                        string aciklama4 = reader["aciklama4"].ToString();
                        string tarih = reader["tarih"].ToString();

                        var barcodeWriter = new BarcodeWriterPixelData
                        {
                            Format = BarcodeFormat.CODE_128,
                            Options = new EncodingOptions
                            {
                                Width = 378, // 10 cm genişlik
                                Height = 151, // 4 cm yükseklik
                                Margin = 15
                            }
                        };

                        var pixelData = barcodeWriter.Write(partiNo);

                        using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height + 140, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
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

                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                // Barkodun üstünde parti numarasını göster
                                g.DrawString(partiNo, new Font("Arial", 14), Brushes.Black, new PointF(10, pixelData.Height + 15)); // 10 px marj

                                // Altındaki bilgileri alt alta diz
                                g.DrawString($"Stok AD : {aciklama}", new Font("Arial", 9), Brushes.Black, new PointF(10, pixelData.Height + 40));

                                // Boyut, ağırlık ve konum bilgilerini aynı satırda göster
                                string infoLine = $"Boyut M2: {aciklama2} | Ağırlık KG: {aciklama3} | Konum: {aciklama4}";
                                g.DrawString(infoLine, new Font("Arial", 9), Brushes.Black, new PointF(10, pixelData.Height + 60));

                                // Tarih bilgisini ayrı bir satıra koy
                                g.DrawString($"Tarih: {tarih}", new Font("Arial", 9), Brushes.Black, new PointF(10, pixelData.Height + 80));
                            }

                            pictureBox1.Image = new Bitmap(bitmap);
                            currentBarcodeBitmap = new Bitmap(bitmap);
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
                e.Graphics.DrawImage(currentBarcodeBitmap, new Rectangle(0, 0, 1180, 590)); // Yüksek DPI için baskı boyutları
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // MySQL bağlantı bilgileri
            string connectionString = "Server=192.168.1.212;Database=stoklar;Uid=Tester74;Pwd=Tester74;";

            // Belirli bir stokid aralığındaki ürünleri almak için SQL sorgusu
            string query = "SELECT partino FROM stok WHERE stokid BETWEEN 1434 AND 1512 ORDER BY stokid ASC";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                // Barkodları oluşturmak için bir Bitmap listesi
                List<Bitmap> barcodeBitmaps = new List<Bitmap>();

                while (reader.Read())
                {
                    string partiNo = reader["partino"].ToString();
                    if (!string.IsNullOrEmpty(partiNo))
                    {
                        var barcodeWriter = new BarcodeWriterPixelData
                        {
                            Format = BarcodeFormat.CODE_128,
                            Options = new EncodingOptions
                            {
                                Width = 378, // 10 cm genişlik
                                Height = 151, // 4 cm yükseklik
                                Margin = 15
                            }
                        };

                        var pixelData = barcodeWriter.Write(partiNo);
                        using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height + 40, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                        {
                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                g.Clear(Color.White);
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

                                // Barkodun üstünde parti numarasını göster
                                g.DrawString(partiNo, new Font("Arial", 12), Brushes.Black, new PointF(10, pixelData.Height + 15)); // 10 px marj
                            }

                            // Barkod bitmap'ini listeye ekle
                            barcodeBitmaps.Add(new Bitmap(bitmap));
                        }
                    }
                }

                // Barkodlar oluşturulduktan sonra onay iste
                if (barcodeBitmaps.Count > 0)
                {
                    var result = MessageBox.Show("Seçilen stok için barkodları yazdırmak istiyor musunuz?", "Yazdırma Onayı", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Yazdırma işlemi
                        PrintBarcodes(barcodeBitmaps);
                    }
                }
                else
                {
                    MessageBox.Show("Seçilen ürünler için barkod bulunamadı.");
                }
            }
        }

        // Barkodları yazdıran metot
        private void PrintBarcodes(List<Bitmap> barcodeBitmaps)
        {
            if (barcodeBitmaps.Count == 0)
            {
                MessageBox.Show("Yazdırılacak barkod bulunamadı.");
                return;
            }

            // İlk barkod için yazıcı ayarını seç ve kaydet
            PrintDocument printDoc = new PrintDocument();
            PrintDialog printDialog = new PrintDialog
            {
                Document = printDoc
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                PrinterSettings printerSettings = printDialog.PrinterSettings;
                PageSettings pageSettings = new PageSettings(printerSettings);

                // Tüm barkodları aynı yazıcı ayarıyla yazdır
                foreach (var barcode in barcodeBitmaps)
                {
                    printDoc = new PrintDocument
                    {
                        PrinterSettings = printerSettings,
                        DefaultPageSettings = pageSettings
                    };

                    printDoc.PrintPage += (sender, e) =>
                    {
                        e.Graphics.DrawImage(barcode, new Rectangle((e.PageBounds.Width - 378) / 2, (e.PageBounds.Height - 151) / 2, 378, 151));
                    };

                    printDoc.Print();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // MySQL bağlantı bilgileri
            string connectionString = "Server=192.168.1.212;Database=stoklar;Uid=Tester74;Pwd=Tester74;";

            // İlk 753 ürünü almak için SQL sorgusu
            string query = "SELECT partino FROM stok ORDER BY tarih ASC LIMIT 753"; // Tarihe göre artan sıralama

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                // Barkodları oluşturmak için bir Bitmap listesi
                List<Bitmap> barcodeBitmaps = new List<Bitmap>();

                while (reader.Read())
                {
                    string partiNo = reader["partino"].ToString();
                    if (!string.IsNullOrEmpty(partiNo))
                    {
                        var barcodeWriter = new BarcodeWriterPixelData
                        {
                            Format = BarcodeFormat.CODE_128,
                            Options = new EncodingOptions
                            {
                                Width = 378, // 10 cm genişlik
                                Height = 151, // 4 cm yükseklik
                                Margin = 15
                            }
                        };

                        var pixelData = barcodeWriter.Write(partiNo);
                        using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height + 40, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                        {
                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                g.Clear(Color.White);
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

                                // Barkodun üstünde parti numarasını göster
                                g.DrawString(partiNo, new Font("Arial", 12), Brushes.Black, new PointF(10, pixelData.Height + 15)); // 10 px marj
                            }

                            // Barkod bitmap'ini listeye ekle
                            barcodeBitmaps.Add(new Bitmap(bitmap));
                        }
                    }
                }

                // Barkodlar oluşturulduktan sonra onay iste
                if (barcodeBitmaps.Count > 0)
                {
                    var result = MessageBox.Show("İlk 753 stok için barkodları yazdırmak istiyor musunuz?", "Yazdırma Onayı", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Yazdırma işlemi
                        PrintBarcodes(barcodeBitmaps);
                    }
                }
                else
                {
                    MessageBox.Show("İlk 753 ürün için barkod bulunamadı.");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // MySQL bağlantı bilgileri
            string connectionString = "Server=192.168.1.212;Database=stoklar;Uid=Tester74;Pwd=Tester74;";

            // Son 5 ürünü almak için SQL sorgusu
            string query = "SELECT partino FROM stok ORDER BY tarih DESC LIMIT 27"; // Tarihe göre azalan sıralama

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                // Barkodları oluşturmak için bir Bitmap listesi
                List<Bitmap> barcodeBitmaps = new List<Bitmap>();

                while (reader.Read())
                {
                    string partiNo = reader["partino"].ToString();
                    if (!string.IsNullOrEmpty(partiNo))
                    {
                        var barcodeWriter = new BarcodeWriterPixelData
                        {
                            Format = BarcodeFormat.CODE_128,
                            Options = new EncodingOptions
                            {
                                Width = 378, // 10 cm genişlik
                                Height = 151, // 4 cm yükseklik
                                Margin = 15
                            }
                        };

                        var pixelData = barcodeWriter.Write(partiNo);
                        using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height + 40, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                        {
                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                g.Clear(Color.White);
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

                                // Barkodun üstünde parti numarasını göster
                                g.DrawString(partiNo, new Font("Arial", 12), Brushes.Black, new PointF(10, pixelData.Height + 15)); // 10 px marj
                            }

                            // Barkod bitmap'ini listeye ekle
                            barcodeBitmaps.Add(new Bitmap(bitmap));
                        }
                    }
                }

                // Barkodlar oluşturulduktan sonra onay iste
                if (barcodeBitmaps.Count > 0)
                {
                    var result = MessageBox.Show("Son  stok için barkodları yazdırmak istiyor musunuz?", "Yazdırma Onayı", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Yazdırma işlemi
                        PrintBarcodes(barcodeBitmaps);
                    }
                }
                else
                {
                    MessageBox.Show("Son 5 ürün için barkod bulunamadı.");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // MySQL bağlantı bilgileri
            string connectionString = "Server=192.168.1.212;Database=stoklar;Uid=Tester74;Pwd=Tester74;";

            // Yalnızca stokid 246 ve sonrasındaki ürünleri almak için SQL sorgusu
            string query = "SELECT partino, aciklama, aciklama2, aciklama3, aciklama4, tarih FROM stok WHERE stokid >= 247";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                // Barkodları oluşturmak için bir Bitmap listesi
                List<Bitmap> barcodeBitmaps = new List<Bitmap>();

                while (reader.Read())
                {
                    string partiNo = reader["partino"].ToString();
                    string aciklama = reader["aciklama"].ToString();
                    string aciklama2 = reader["aciklama2"].ToString();
                    string aciklama3 = reader["aciklama3"].ToString();
                    string aciklama4 = reader["aciklama4"].ToString();
                    string tarih = reader["tarih"].ToString();

                    if (!string.IsNullOrEmpty(partiNo))
                    {
                        var barcodeWriter = new BarcodeWriterPixelData
                        {
                            Format = BarcodeFormat.CODE_128,
                            Options = new EncodingOptions
                            {
                                Width = 378, // 10 cm genişlik
                                Height = 151, // 4 cm yükseklik
                                Margin = 15
                            }
                        };

                        var pixelData = barcodeWriter.Write(partiNo);
                        using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height + 140, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                        {
                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                g.Clear(Color.White);
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

                                // Bilgileri alt alta yazdır
                                g.DrawString(partiNo, new Font("Arial", 10), Brushes.Black, new PointF(10, pixelData.Height + 15));
                                g.DrawString($"Stok AD : {aciklama}", new Font("Arial", 9), Brushes.Black, new PointF(10, pixelData.Height + 40));
                                g.DrawString($"Boyut M2: {aciklama2} | Ağırlık KG: {aciklama3} | Konum: {aciklama4}", new Font("Arial", 9), Brushes.Black, new PointF(10, pixelData.Height + 60));
                                g.DrawString($"Tarih: {tarih}", new Font("Arial", 9), Brushes.Black, new PointF(10, pixelData.Height + 80));
                            }

                            // Barkod bitmap'ini listeye ekle
                            barcodeBitmaps.Add(new Bitmap(bitmap));
                        }
                    }
                }

                if (barcodeBitmaps.Count > 0)
                {
                    var result = MessageBox.Show("Tüm ürünler için barkodları yazdırmak istiyor musunuz?", "Yazdırma Onayı", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Yazdırma işlemi
                        PrintBarcodes(barcodeBitmaps);
                    }
                }
                else
                {
                    MessageBox.Show("Ürünler için barkod bulunamadı.");
                }
            }
        }
    }
}






