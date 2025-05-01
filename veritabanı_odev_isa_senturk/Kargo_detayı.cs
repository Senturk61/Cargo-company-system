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

    public partial class Kargo_detayı : Form
    {
        static string connectionString = "Data Source=DESKTOP-S4H53I6\\SQLEXPRESS;Initial Catalog=isa_senturk_veritabanı_odev;Integrated Security=True";
        public Kargo_detayı()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Listele("SELECT * FROM Kargo");
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

        private void button12_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan bilgileri kontrol et
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text)
                && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text))
            {
                // Bilgileri al
                int kargoID = int.Parse(textBox1.Text);
                int siparisID = int.Parse(textBox2.Text);
                DateTime kargoTarihi = DateTime.Parse(textBox3.Text);
                string teslimatAdresi = textBox4.Text;

                // Veritabanına yeni kargo ekle
                AddShippingToDatabase(kargoID, siparisID, kargoTarihi, teslimatAdresi);

                MessageBox.Show("Kargo başarıyla eklendi.");

                // Formu temizle
                ClearForm();
            }
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri girin.");
            }
        }
        private void AddShippingToDatabase(int kargoID, int siparisID, DateTime kargoTarihi, string teslimatAdresi)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Veritabanına ekleme işlemini gerçekleştir
                string sql = "INSERT INTO Kargo (KargoID, SiparisID, KargoTarihi, TeslimatAdresi) VALUES (@KargoID, @SiparisID, @KargoTarihi, @TeslimatAdresi)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@KargoID", kargoID);
                    command.Parameters.AddWithValue("@SiparisID", siparisID);
                    command.Parameters.AddWithValue("@KargoTarihi", kargoTarihi);
                    command.Parameters.AddWithValue("@TeslimatAdresi", teslimatAdresi);

                    command.ExecuteNonQuery();
                }
            }
        }
        private void ClearForm()
        {
            // Form üzerindeki TextBox kontrollerini temizle
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();   
            textBox4.Clear();
        }
    }
}
