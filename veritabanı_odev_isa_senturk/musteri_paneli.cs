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
using veritabanı_odev_isa_senturk;

namespace veritabanı_odev_isa_senturk
{
    public partial class musteri_paneli : Form
    {
        private string connectionString = "Data Source=DESKTOP-S4H53I6\\SQLEXPRESS;Initial Catalog=isa_senturk_veritabanı_odev;Integrated Security=True";
        public musteri_paneli()
        {
            InitializeComponent();
        }

        private void musteri_paneli_Load(object sender, EventArgs e)
        {

        }

        private void müsteriPaneli_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Kullanıcı formu kapatmaya çalıştığında uygulamayı kapat
            Application.Exit();
        }

        private void AramaYapVeGridDoldur(string arananAd, string arananSoyad)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Kullanicilar WHERE Ad = @ArananAd AND Soyad = @ArananSoyad ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ArananAd", arananAd);
                        command.Parameters.AddWithValue("@ArananSoyad", arananSoyad);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // DataGridView'e verileri set et
                            dataGridView1.DataSource = dataTable;

                            if (dataTable.Rows.Count == 0)
                            {
                                MessageBox.Show("Müşteri bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string arananAd = txtArananAd.Text;
            string arananSoyad = txtArananSoyad.Text;

            // Arama işlemini başlat
            AramaYapVeGridDoldur(arananAd, arananSoyad);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // TextBox'tan SiparisID'yi al
            if (int.TryParse(textBoxSiparisID.Text, out int siparisID))
            {
                // Arama fonksiyonunu çağır
                KargoAra(siparisID);
            }
            else
            {
                MessageBox.Show("Geçerli bir Sipariş ID girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void KargoAra(int siparisID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SiparisID'ye göre Kargo tablosunu arayan sorgu
                    string query = "SELECT * FROM Kargo WHERE SiparisID = @SiparisID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SiparisID", siparisID);

                        // Verileri okumak için bir SqlDataReader kullanın
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // DataTable oluşturarak verileri saklayın
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            // DataGridView'e verileri yazdır
                            dataGridView2.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AramaYap(int kullaniciID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // KullanıcıID'ye göre Siparisler tablosunu arayan sorgu
                    string query = "SELECT * FROM Siparisler WHERE KullaniciID = @KullaniciID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                        // Verileri okumak için bir SqlDataReader kullanın
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // DataTable oluşturarak verileri saklayın
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            // DataGridView'e verileri yazdır
                            dataGridView3.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // TextBox'tan KullaniciID'yi al
            if (int.TryParse(textBoxKullaniciID.Text, out int kullaniciID))
            {
                // Arama fonksiyonunu çağır
                AramaYap(kullaniciID);
            }
            else
            {
                MessageBox.Show("Geçerli bir Kullanıcı ID girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtKullaniciID_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
