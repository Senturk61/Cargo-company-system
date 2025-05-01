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
    public partial class Siparis_detayı : Form
    {
        static string connectionString = "Data Source=DESKTOP-S4H53I6\\SQLEXPRESS;Initial Catalog=isa_senturk_veritabanı_odev;Integrated Security=True";
        public Siparis_detayı()
        {
            InitializeComponent();
        }

        private void Siparis_detayı_Load(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            Listele("SELECT * FROM SiparisDetaylari");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan bilgileri kontrol et
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text)
                && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text))
            {
                // Bilgileri al
                int detayID = int.Parse(textBox1.Text);
                int siparisID = int.Parse(textBox2.Text);
                int urunID = int.Parse(textBox3.Text);
                int miktar = int.Parse(textBox4.Text);

                // Veritabanına yeni sipariş detayı ekle
                AddOrderDetailToDatabase(detayID, siparisID, urunID, miktar);

                MessageBox.Show("Sipariş detayı başarıyla eklendi.");

                // Formu temizle
                ClearForm();
            }
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri girin.");
            }
        }
        private void AddOrderDetailToDatabase(int detayID, int siparisID, int urunID, int miktar)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Veritabanına ekleme işlemini gerçekleştir
                string sql = "INSERT INTO SiparisDetaylari (DetayID, SiparisID, UrunID, Miktar) VALUES (@DetayID, @SiparisID, @UrunID, @Miktar)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@DetayID", detayID);
                    command.Parameters.AddWithValue("@SiparisID", siparisID);
                    command.Parameters.AddWithValue("@UrunID", urunID);
                    command.Parameters.AddWithValue("@Miktar", miktar);

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
