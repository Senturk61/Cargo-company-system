using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;
using Microsoft.VisualBasic;

namespace veritabanı_odev_isa_senturk
{
    public partial class yonetici_paneli : Form
    {
        private int selectedUserID;
        SqlCommandBuilder commandBuilder;
        SqlDataAdapter adtr;
        DataTable dataTable = new DataTable();

        static string connectionString = "Data Source=DESKTOP-S4H53I6\\SQLEXPRESS;Initial Catalog=isa_senturk_veritabanı_odev;Integrated Security=True";

        string query = @"
        SELECT 
            K.KullaniciID,
            K.Ad,
            K.Soyad,
            K.Eposta,
            U.UrunID,
            U.UrunAdi,
            U.Fiyat,
            U.StokMiktari,
            S.SiparisID,
            S.ToplamFiyat,
            S.SiparisTarihi,
            SD.DetayID,
            SD.Miktar,
            O.OdemeID,
            O.OdemeTarihi,
            O.OdemeMiktari,
            O.OdemeYontemi,
            Krg.KargoID,
            Krg.KargoTarihi,
            Krg.TeslimatAdresi
        FROM Kullanicilar K
        JOIN Siparisler S ON K.KullaniciID = S.KullaniciID
        JOIN SiparisDetaylari SD ON S.SiparisID = SD.SiparisID
        JOIN Urunler U ON SD.UrunID = U.UrunID
        JOIN Odemeler O ON S.SiparisID = O.SiparisID
        JOIN Kargo Krg ON S.SiparisID = Krg.SiparisID
    ";

        public yonetici_paneli()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(1678, 750);
        }
        SqlConnection baglan = new SqlConnection(connectionString);



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        private void listele_Click(object sender, EventArgs e)
        {
            Listele("SELECT * FROM Kullanicilar");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string isim = aramaisim.Text;
            string soyisim = aramasoyisim.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL sorgusu oluştur
                string query = @"
    SELECT DISTINCT 
        Kullanicilar.KullaniciID, Kullanicilar.Ad, Kullanicilar.Soyad, Kullanicilar.Eposta, Kullanicilar.Sifre,
        Siparisler.SiparisID, Siparisler.ToplamFiyat, Siparisler.SiparisTarihi,
        SiparisDetaylari.DetayID, SiparisDetaylari.UrunID, SiparisDetaylari.Miktar,
        Odemeler.OdemeID, Odemeler.OdemeTarihi, Odemeler.OdemeMiktari, Odemeler.OdemeYontemi,
        Kargo.KargoID, Kargo.KargoTarihi, Kargo.TeslimatAdresi
    FROM Kullanicilar
    INNER JOIN Siparisler ON Kullanicilar.KullaniciID = Siparisler.KullaniciID
    LEFT JOIN SiparisDetaylari ON Siparisler.SiparisID = SiparisDetaylari.SiparisID
    LEFT JOIN Odemeler ON Siparisler.SiparisID = Odemeler.SiparisID
    LEFT JOIN Kargo ON Siparisler.SiparisID = Kargo.SiparisID
    WHERE Kullanicilar.Ad LIKE @Isim AND Kullanicilar.Soyad LIKE @Soyisim
";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Parametreleri ekleyerek sorguyu hazırla
                    command.Parameters.AddWithValue("@Isim", "%" + isim + "%");
                    command.Parameters.AddWithValue("@Soyisim", "%" + soyisim + "%");

                    // Verileri çek
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // DataTable oluştur
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'e verileri bağla
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int kullaniciID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["KullaniciID"].Value);

                DialogResult result = MessageBox.Show("Seçili kullanıcıyı silmek istiyor musunuz?", "Kullanıcı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    KullaniciKayitlariniSil(kullaniciID);
                    VerileriYukle(); // Silinen verileri tekrar yükle
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz kullanıcıyı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                // Kullanıcı Ekle Form'unu aç
                Kullanıcı_ekle kullaniciEkleForm = new Kullanıcı_ekle();
                kullaniciEkleForm.Show();
            }
        }
        private void KullaniciKayitlariniSil(int kullaniciID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string kullaniciKayitlariniSilQuery = "DELETE FROM Kullanicilar WHERE KullaniciID = @KullaniciID;" +
                                                         "DELETE FROM Siparisler WHERE KullaniciID = @KullaniciID;" +
                                                         "DELETE FROM SiparisDetaylari WHERE SiparisID IN (SELECT SiparisID FROM Siparisler WHERE KullaniciID = @KullaniciID);" +
                                                         "DELETE FROM Odemeler WHERE SiparisID IN (SELECT SiparisID FROM Siparisler WHERE KullaniciID = @KullaniciID);" +
                                                         "DELETE FROM Kargo WHERE SiparisID IN (SELECT SiparisID FROM Siparisler WHERE KullaniciID = @KullaniciID);";

                    using (SqlCommand command = new SqlCommand(kullaniciKayitlariniSilQuery, connection))
                    {
                        command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Kullanıcının tüm kayıtları başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private void VerileriYukle()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string kullaniciGetirQuery = "SELECT KullaniciID, Ad, Soyad, Eposta FROM Kullanicilar;";
                using (SqlCommand command = new SqlCommand(kullaniciGetirQuery, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void listele2_Click(object sender, EventArgs e)
        {
            Listele2("SELECT * FROM Urunler", dataGridView2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Listele3("SELECT * FROM Siparisler", dataGridView3);
        }

        private void Listele(string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void Listele2(string query, DataGridView dataGridView)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void Listele3(string query, DataGridView dataGridView)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private void Listele4(string query, DataGridView dataGridView)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private void Listele5(string query, DataGridView dataGridView)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private void Listele6(string query, DataGridView dataGridView)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Listele4("SELECT * FROM SiparisDetaylari", dataGridView4);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Listele5("SELECT * FROM Odemeler", dataGridView5);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Listele6("SELECT * FROM Kargo", dataGridView6);
        }

        private void guncelle1_Click_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int kullaniciID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["KullaniciID"].Value);

                // Güncelleme işlemini gerçekleştirmek için gerekli kontrolleri oluşturun
                string yeniAd = dataGridView1.Rows[selectedRowIndex].Cells["Ad"].Value.ToString();
                string yeniSoyad = dataGridView1.Rows[selectedRowIndex].Cells["Soyad"].Value.ToString();
                string yeniEposta = dataGridView1.Rows[selectedRowIndex].Cells["Eposta"].Value.ToString();
                // Diğer sütunlara göre gerekli kontrolleri ekleyin

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Güncelleme sorgusunu oluşturun
                        string updateQuery = "UPDATE Kullanicilar SET Ad = @Ad, Soyad = @Soyad, Eposta = @Eposta WHERE KullaniciID = @KullaniciID";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Ad", yeniAd);
                            command.Parameters.AddWithValue("@Soyad", yeniSoyad);
                            command.Parameters.AddWithValue("@Eposta", yeniEposta);
                            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                            // Güncelleme sorgusunu çalıştırın
                            int affectedRows = command.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Kullanıcı bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Güncelleme işlemi başarılıysa DataGridView'i yenileyin
                                Listele("SELECT * FROM Kullanicilar");
                            }
                            else
                            {
                                MessageBox.Show("Kullanıcı bilgileri güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz kullanıcıyı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guncelle2_Click_Click(object sender, EventArgs e)
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = dataGridView2.SelectedRows[0].Index;
                    int urunID = Convert.ToInt32(dataGridView2.Rows[selectedRowIndex].Cells["UrunID"].Value);

                    // Güncelleme işlemini gerçekleştirmek için gerekli kontrolleri oluşturun
                    string yeniUrunAdi = dataGridView2.Rows[selectedRowIndex].Cells["UrunAdi"].Value.ToString();
                    decimal yeniFiyat = Convert.ToDecimal(dataGridView2.Rows[selectedRowIndex].Cells["Fiyat"].Value);
                    int yeniStokMiktari = Convert.ToInt32(dataGridView2.Rows[selectedRowIndex].Cells["StokMiktari"].Value);
                    // Diğer sütunlara göre gerekli kontrolleri ekleyin

                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            // Güncelleme sorgusunu oluşturun
                            string updateQuery = "UPDATE Urunler SET UrunAdi = @UrunAdi, Fiyat = @Fiyat, StokMiktari = @StokMiktari WHERE UrunID = @UrunID";

                            using (SqlCommand command = new SqlCommand(updateQuery, connection))
                            {
                                command.Parameters.AddWithValue("@UrunAdi", yeniUrunAdi);
                                command.Parameters.AddWithValue("@Fiyat", yeniFiyat);
                                command.Parameters.AddWithValue("@StokMiktari", yeniStokMiktari);
                                command.Parameters.AddWithValue("@UrunID", urunID);

                                // Güncelleme sorgusunu çalıştırın
                                int affectedRows = command.ExecuteNonQuery();

                                if (affectedRows > 0)
                                {
                                    MessageBox.Show("Ürün bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    // Güncelleme işlemi başarılıysa DataGridView'i yenileyin
                                    Listele2("SELECT * FROM Urunler", dataGridView2);
                                }
                                else
                                {
                                    MessageBox.Show("Ürün bilgileri güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen güncellemek istediğiniz ürünü seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        private void guncelle3_Click_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView3.SelectedRows[0].Index;
                int siparisID = Convert.ToInt32(dataGridView3.Rows[selectedRowIndex].Cells["SiparisID"].Value);

                // Güncelleme işlemini gerçekleştirmek için gerekli kontrolleri oluşturun
                decimal yeniToplamFiyat = Convert.ToDecimal(dataGridView3.Rows[selectedRowIndex].Cells["ToplamFiyat"].Value);
                DateTime yeniSiparisTarihi = Convert.ToDateTime(dataGridView3.Rows[selectedRowIndex].Cells["SiparisTarihi"].Value);
                // Diğer sütunlara göre gerekli kontrolleri ekleyin

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Güncelleme sorgusunu oluşturun
                        string updateQuery = "UPDATE Siparisler SET ToplamFiyat = @ToplamFiyat, SiparisTarihi = @SiparisTarihi WHERE SiparisID = @SiparisID";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@ToplamFiyat", yeniToplamFiyat);
                            command.Parameters.AddWithValue("@SiparisTarihi", yeniSiparisTarihi);
                            command.Parameters.AddWithValue("@SiparisID", siparisID);

                            // Güncelleme sorgusunu çalıştırın
                            int affectedRows = command.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Sipariş bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Güncelleme işlemi başarılıysa DataGridView'i yenileyin
                                Listele3("SELECT * FROM Siparisler", dataGridView3);
                            }
                            else
                            {
                                MessageBox.Show("Sipariş bilgileri güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz siparişi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guncelle4_Click_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView4.SelectedRows[0].Index;
                int detayID = Convert.ToInt32(dataGridView4.Rows[selectedRowIndex].Cells["DetayID"].Value);

                // Güncelleme işlemini gerçekleştirmek için gerekli kontrolleri oluşturun
                int yeniUrunID = Convert.ToInt32(dataGridView4.Rows[selectedRowIndex].Cells["UrunID"].Value);
                int yeniMiktar = Convert.ToInt32(dataGridView4.Rows[selectedRowIndex].Cells["Miktar"].Value);
                // Diğer sütunlara göre gerekli kontrolleri ekleyin

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Güncelleme sorgusunu oluşturun
                        string updateQuery = "UPDATE SiparisDetaylari SET UrunID = @UrunID, Miktar = @Miktar WHERE DetayID = @DetayID";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@UrunID", yeniUrunID);
                            command.Parameters.AddWithValue("@Miktar", yeniMiktar);
                            command.Parameters.AddWithValue("@DetayID", detayID);

                            // Güncelleme sorgusunu çalıştırın
                            int affectedRows = command.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Sipariş detayları başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Güncelleme işlemi başarılıysa DataGridView'i yenileyin
                                Listele4("SELECT * FROM SiparisDetaylari", dataGridView4);
                            }
                            else
                            {
                                MessageBox.Show("Sipariş detayları güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz sipariş detayını seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guncelle5_Click_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView5.SelectedRows[0].Index;
                int odemeID = Convert.ToInt32(dataGridView5.Rows[selectedRowIndex].Cells["OdemeID"].Value);

                // Güncelleme işlemini gerçekleştirmek için gerekli kontrolleri oluşturun
                decimal yeniOdemeMiktari = Convert.ToDecimal(dataGridView5.Rows[selectedRowIndex].Cells["OdemeMiktari"].Value);
                string yeniOdemeYontemi = dataGridView5.Rows[selectedRowIndex].Cells["OdemeYontemi"].Value.ToString();
                // Diğer sütunlara göre gerekli kontrolleri ekleyin

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Güncelleme sorgusunu oluşturun
                        string updateQuery = "UPDATE Odemeler SET OdemeMiktari = @OdemeMiktari, OdemeYontemi = @OdemeYontemi WHERE OdemeID = @OdemeID";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@OdemeMiktari", yeniOdemeMiktari);
                            command.Parameters.AddWithValue("@OdemeYontemi", yeniOdemeYontemi);
                            command.Parameters.AddWithValue("@OdemeID", odemeID);

                            // Güncelleme sorgusunu çalıştırın
                            int affectedRows = command.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Ödeme bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Güncelleme işlemi başarılıysa DataGridView'i yenileyin
                                Listele5("SELECT * FROM Odemeler", dataGridView5);
                            }
                            else
                            {
                                MessageBox.Show("Ödeme bilgileri güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz ödeme bilgisini seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guncelle6_Click_Click(object sender, EventArgs e)
        {
            if (dataGridView6.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView6.SelectedRows[0].Index;
                int kargoID = Convert.ToInt32(dataGridView6.Rows[selectedRowIndex].Cells["KargoID"].Value);

                // Güncelleme işlemini gerçekleştirmek için gerekli kontrolleri oluşturun
                DateTime yeniKargoTarihi = Convert.ToDateTime(dataGridView6.Rows[selectedRowIndex].Cells["KargoTarihi"].Value);
                string yeniTeslimatAdresi = dataGridView6.Rows[selectedRowIndex].Cells["TeslimatAdresi"].Value.ToString();
                // Diğer sütunlara göre gerekli kontrolleri ekleyin

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Güncelleme sorgusunu oluşturun
                        string updateQuery = "UPDATE Kargo SET KargoTarihi = @KargoTarihi, TeslimatAdresi = @TeslimatAdresi WHERE KargoID = @KargoID";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@KargoTarihi", yeniKargoTarihi);
                            command.Parameters.AddWithValue("@TeslimatAdresi", yeniTeslimatAdresi);
                            command.Parameters.AddWithValue("@KargoID", kargoID);

                            // Güncelleme sorgusunu çalıştırın
                            int affectedRows = command.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Kargo bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Güncelleme işlemi başarılıysa DataGridView'i yenileyin
                                Listele6("SELECT * FROM Kargo", dataGridView6);
                            }
                            else
                            {
                                MessageBox.Show("Kargo bilgileri güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz kargo bilgisini seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView2.SelectedRows[0].Index;
                int urunID = Convert.ToInt32(dataGridView2.Rows[selectedRowIndex].Cells["UrunID"].Value);

                DialogResult result = MessageBox.Show("Seçili ürünü silmek istiyor musunuz?", "Ürün Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    UrunKayitlariniSil(urunID);
                    Listele2("SELECT * FROM Urunler", dataGridView2); // Güncellenmiş verileri tekrar yükle
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz ürünü seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void UrunKayitlariniSil(int urunID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Silme sorgusu oluştur
                    string urunSilQuery = "DELETE FROM Urunler WHERE UrunID = @UrunID;";

                    using (SqlCommand command = new SqlCommand(urunSilQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UrunID", urunID);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Ürün başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView3.SelectedRows[0].Index;
                int siparisID = Convert.ToInt32(dataGridView3.Rows[selectedRowIndex].Cells["SiparisID"].Value);

                DialogResult result = MessageBox.Show("Seçili siparişi silmek istiyor musunuz?", "Sipariş Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SiparisKayitlariniSil(siparisID);
                    Listele3("SELECT * FROM Siparisler", dataGridView3); // Güncellenmiş verileri tekrar yükle
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz siparişi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void SiparisKayitlariniSil(int siparisID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string siparisKayitlariniSilQuery = "DELETE FROM SiparisDetaylari WHERE SiparisID = @SiparisID;" +
                                                         "DELETE FROM Odemeler WHERE SiparisID = @SiparisID;" +
                                                         "DELETE FROM Kargo WHERE SiparisID = @SiparisID;" +
                                                         "DELETE FROM Siparisler WHERE SiparisID = @SiparisID;";

                    using (SqlCommand command = new SqlCommand(siparisKayitlariniSilQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SiparisID", siparisID);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Sipariş ve ilişkili kayıtlar başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            // Seçili satır var mı kontrol et
            if (dataGridView4.SelectedRows.Count > 0)
            {
                // Seçili satırın DetayID değerini al
                int detayID = Convert.ToInt32(dataGridView4.SelectedRows[0].Cells["DetayID"].Value);

                // SiparisDetaylariKayitlariniSil metodunu çağır
                SiparisDetaylariKayitlariniSil(detayID);

                // Verileri tekrar yükle
                Listele4("SELECT * FROM SiparisDetaylari", dataGridView4);
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz sipariş detayını seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void SiparisDetaylariKayitlariniSil(int detayID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string siparisDetaylariKayitlariniSilQuery = "DELETE FROM SiparisDetaylari WHERE DetayID = @DetayID;";

                    using (SqlCommand command = new SqlCommand(siparisDetaylariKayitlariniSilQuery, connection))
                    {
                        command.Parameters.AddWithValue("@DetayID", detayID);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Sipariş detayları başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            // Seçili satır var mı kontrol et
            if (dataGridView5.SelectedRows.Count > 0)
            {
                // Seçili satırın OdemeID değerini al
                int odemeID = Convert.ToInt32(dataGridView5.SelectedRows[0].Cells["OdemeID"].Value);

                // OdemeleriSil metodunu çağır
                OdemeleriSil(odemeID);

                // Verileri tekrar yükle
                Listele5("SELECT * FROM Odemeler", dataGridView5);
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz ödeme kaydını seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void OdemeleriSil(int odemeID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Ödeme kaydını silmek için SQL sorgusu
                    string odemeSilQuery = "DELETE FROM Odemeler WHERE OdemeID = @OdemeID;";

                    using (SqlCommand command = new SqlCommand(odemeSilQuery, connection))
                    {
                        command.Parameters.AddWithValue("@OdemeID", odemeID);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Ödeme kaydı başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (dataGridView6.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView6.SelectedRows[0].Index;
                int kargoID = Convert.ToInt32(dataGridView6.Rows[selectedRowIndex].Cells["KargoID"].Value);

                DialogResult result = MessageBox.Show("Seçili kargo kaydını silmek istiyor musunuz?", "Kargo Kaydını Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    KargoKaydiniSil(kargoID);
                    Listele6("SELECT * FROM Kargo", dataGridView6); // Güncellenmiş verileri tekrar yükle
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz kargo kaydını seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void KargoKaydiniSil(int kargoID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kargo kaydını silmek için SQL sorgusu
                    string kargoSilQuery = "DELETE FROM Kargo WHERE KargoID = @KargoID;";

                    using (SqlCommand command = new SqlCommand(kargoSilQuery, connection))
                    {
                        command.Parameters.AddWithValue("@KargoID", kargoID);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Kargo kaydı başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            {
                // Kullanıcı Ekle Form'unu aç
                Form1 KullanıcıHome = new Form1();
                KullanıcıHome.Show();


            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            {
                // Kullanıcı Ekle Form'unu aç
                Urun_ekle urunekleform = new Urun_ekle();
                urunekleform.Show();
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            {
                // Kullanıcı Ekle Form'unu aç
                Siparis_ekle siparsekle = new Siparis_ekle();
                siparsekle.Show();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            {
                Siparis_detayı siparisdetayı = new Siparis_detayı();
                siparisdetayı.Show();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            {
                
                Kargo_detayı kargoekle = new Kargo_detayı();
                kargoekle.Show();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            {
                Odeme_ekle odemeekle = new Odeme_ekle();
                odemeekle.Show();
            }
        }
        private void YoneticiPaneli_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Kullanıcı formu kapatmaya çalıştığında uygulamayı kapat
            Application.Exit();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            // Veritabanı bağlantısı
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL sorgusu
                string sql = "SELECT * FROM KullaniciRolleri";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // DataGridView'e verileri yükle
                    dataGridView7.DataSource = dataTable;
                }
            }
        }
        private void button26_Click(object sender, EventArgs e)
        {
            if (dataGridView7.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView7.SelectedRows[0].Index;
                int kullaniciID = Convert.ToInt32(dataGridView7.Rows[selectedRowIndex].Cells["KullaniciID"].Value);
                int rolID = Convert.ToInt32(dataGridView7.Rows[selectedRowIndex].Cells["RolID"].Value);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string updateQuery = "UPDATE KullaniciRolleri SET KullaniciID = @KullaniciID, RolID = @RolID WHERE KullaniciID = @KullaniciID AND RolID = @RolID";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                            command.Parameters.AddWithValue("@RolID", rolID);

                            int affectedRows = command.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Kullanıcı rol bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dataGridView7.Rows[selectedRowIndex].Cells["KullaniciID"].Value = kullaniciID;
                                dataGridView7.Rows[selectedRowIndex].Cells["RolID"].Value = rolID;
                            }
                            else
                            {
                                MessageBox.Show("Kullanıcı rol bilgileri güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz kullanıcı rolünü seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            {
                RolEkle rolekle = new RolEkle();
                rolekle.Show();
            }
        }

        private void yonetici_paneli_Load(object sender, EventArgs e)
        {

        }
    }

}
