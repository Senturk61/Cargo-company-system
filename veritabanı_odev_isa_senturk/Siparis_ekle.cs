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

namespace veritabanı_odev_isa_senturk
{
    public partial class Siparis_ekle : Form
    {
        static string connectionString = "Data Source=DESKTOP-S4H53I6\\SQLEXPRESS;Initial Catalog=isa_senturk_veritabanı_odev;Integrated Security=True";
        public Siparis_ekle()
        {
            InitializeComponent();
        }

        private void Siparis_ekle_Load(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            Listele("SELECT * FROM Siparisler");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan bilgileri kontrol et
            if (!string.IsNullOrEmpty(Ad1.Text) && !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                // Bilgileri al
                int siparisID = int.Parse(Ad1.Text);
                int kullaniciID = int.Parse(textBox1.Text);
                decimal toplamFiyat = decimal.Parse(textBox2.Text);
                DateTime siparisTarihi = DateTime.Now; // Şu anki tarih ve saat

                // Veritabanına yeni siparişi ekle
                AddOrderToDatabase(siparisID, kullaniciID, toplamFiyat, siparisTarihi);

                MessageBox.Show("Sipariş başarıyla eklendi.");

                // Formu temizle
                ClearForm();
            }
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri girin.");
            }
        }
        private void AddOrderToDatabase(int siparisID, int kullaniciID, decimal toplamFiyat, DateTime siparisTarihi)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Veritabanına ekleme işlemini gerçekleştir
                string sql = "INSERT INTO Siparisler (SiparisID, KullaniciID, ToplamFiyat, SiparisTarihi) VALUES (@SiparisID, @KullaniciID, @ToplamFiyat, @SiparisTarihi)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@SiparisID", siparisID);
                    command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    command.Parameters.AddWithValue("@ToplamFiyat", toplamFiyat);
                    command.Parameters.AddWithValue("@SiparisTarihi", siparisTarihi);

                    command.ExecuteNonQuery();
                }
            }
        }
        private void ClearForm()
        {
            // Form üzerindeki TextBox kontrollerini temizle
            Ad1.Clear();
            textBox1.Clear();
            textBox2.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Ad1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
