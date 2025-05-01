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
    public partial class Urun_ekle : Form
    {
        private string connectionString = "Data Source=DESKTOP-S4H53I6\\SQLEXPRESS;Initial Catalog=isa_senturk_veritabanı_odev;Integrated Security=True";
        public Urun_ekle()
        {
            InitializeComponent();
        }

        private void listele2_Click(object sender, EventArgs e)
        {
            Listele2("SELECT * FROM Urunler");
        }

        private void Listele2(string query)
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan bilgileri kontrol et
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text))
            {
                // Bilgileri al
                int urunID = int.Parse(Ad1.Text);
                string urunAdi = textBox1.Text;
                decimal fiyat = decimal.Parse(textBox2.Text);
                int stokMiktari = int.Parse(textBox3.Text);

                // Veritabanına yeni ürünü ekle
                AddProductToDatabase(urunID, urunAdi, fiyat, stokMiktari);

                MessageBox.Show("Ürün başarıyla eklendi.");

                // Formu temizle
                ClearForm();
            }
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri girin.");
            }
        }
        private void AddProductToDatabase(int urunID, string urunAdi, decimal fiyat, int stokMiktari)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Veritabanına ekleme işlemini gerçekleştir
                string sql = "INSERT INTO Urunler (UrunID, UrunAdi, Fiyat, StokMiktari) VALUES (@UrunID, @UrunAdi, @Fiyat, @StokMiktari)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UrunID", urunID);
                    command.Parameters.AddWithValue("@UrunAdi", urunAdi);
                    command.Parameters.AddWithValue("@Fiyat", fiyat);
                    command.Parameters.AddWithValue("@StokMiktari", stokMiktari);

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
            textBox3.Clear();
        }
    }
}
