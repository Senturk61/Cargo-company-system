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
    public partial class RolEkle : Form
    {
        private string connectionString = "Data Source=DESKTOP-S4H53I6\\SQLEXPRESS;Initial Catalog=isa_senturk_veritabanı_odev;Integrated Security=True";
        public RolEkle()
        {
            InitializeComponent();
        }

        private void btnKullanicilarListele_Click_Click(object sender, EventArgs e)
        {
            Listele("SELECT * FROM Kullanicilar");
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

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                // TextBox'tan kullanıcıID ve rolID'yi al
                int kullaniciID = Convert.ToInt32(textBox1.Text);
                int rolID = Convert.ToInt32(textBox2.Text);

                // SqlConnection oluştur
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // KullanıcıID ve RolID'ye göre kaydın varlığını kontrol et
                    string checkQuery = "SELECT COUNT(*) FROM KullaniciRolleri WHERE KullaniciID = @KullaniciID AND RolID = @RolID";

                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        // Parametreleri ekleyerek SQL sorgusunu güvenli hale getir
                        checkCommand.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                        checkCommand.Parameters.AddWithValue("@RolID", rolID);

                        // Kaydın varlığını kontrol et
                        int existingRecordCount = (int)checkCommand.ExecuteScalar();

                        if (existingRecordCount == 0)
                        {
                            // Kayıt yoksa INSERT sorgusunu oluştur
                            string insertQuery = "INSERT INTO KullaniciRolleri (KullaniciID, RolID) VALUES (@KullaniciID, @RolID)";

                            // SqlCommand oluştur
                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Parametreleri ekleyerek SQL sorgusunu güvenli hale getir
                                command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                                command.Parameters.AddWithValue("@RolID", rolID);

                                // INSERT sorgusunu çalıştır
                                int affectedRows = command.ExecuteNonQuery();

                                if (affectedRows > 0)
                                {
                                    MessageBox.Show("Kullanıcı rol bilgileri başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Kullanıcı rol bilgileri eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bu kullanıcı zaten belirtilen role sahiptir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearForm()
        {
            // Form üzerindeki TextBox kontrollerini temizle
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                // Veritabanı bağlantısı
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL sorgusu
                    string sql = "SELECT * FROM Roller";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridView'e verileri yükle
                        dataGridView2.DataSource = dataTable;
                    }
                }
            }
        }
    }
}
