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
    public partial class Odeme_ekle : Form
    {
        static string connectionString = "Data Source=DESKTOP-S4H53I6\\SQLEXPRESS;Initial Catalog=isa_senturk_veritabanı_odev;Integrated Security=True";
        public Odeme_ekle()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Listele("SELECT * FROM Odemeler");
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

        private void button11_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan bilgileri kontrol et
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text)
                && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text)
                && !string.IsNullOrEmpty(textBox5.Text))
            {
                // Bilgileri al
                int odemeID = int.Parse(textBox1.Text);
                int siparisID = int.Parse(textBox2.Text);
                DateTime odemeTarihi = DateTime.Parse(textBox3.Text);
                decimal odemeMiktari = decimal.Parse(textBox4.Text);
                string odemeYontemi = textBox5.Text;

                // Veritabanına yeni ödeme ekle
                AddPaymentToDatabase(odemeID, siparisID, odemeTarihi, odemeMiktari, odemeYontemi);

                MessageBox.Show("Ödeme başarıyla eklendi.");

                // Formu temizle
                ClearForm();
            }
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri girin.");
            }
        }
        private void AddPaymentToDatabase(int odemeID, int siparisID, DateTime odemeTarihi, decimal odemeMiktari, string odemeYontemi)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Veritabanına ekleme işlemini gerçekleştir
                string sql = "INSERT INTO Odemeler (OdemeID, SiparisID, OdemeTarihi, OdemeMiktari, OdemeYontemi) VALUES (@OdemeID, @SiparisID, @OdemeTarihi, @OdemeMiktari, @OdemeYontemi)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@OdemeID", odemeID);
                    command.Parameters.AddWithValue("@SiparisID", siparisID);
                    command.Parameters.AddWithValue("@OdemeTarihi", odemeTarihi);
                    command.Parameters.AddWithValue("@OdemeMiktari", odemeMiktari);
                    command.Parameters.AddWithValue("@OdemeYontemi", odemeYontemi);

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
            textBox5.Clear();
        }
    }
}
