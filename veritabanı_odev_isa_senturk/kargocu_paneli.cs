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
    public partial class kargocu_paneli : Form
    {
        private string connectionString = "Data Source=DESKTOP-S4H53I6\\SQLEXPRESS;Initial Catalog=isa_senturk_veritabanı_odev;Integrated Security=True";
        public kargocu_paneli()
        {
            InitializeComponent();
        }

        private void kargocu_paneli_Load(object sender, EventArgs e)
        {

        }

        private void Kargocu_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Kullanıcı formu kapatmaya çalıştığında uygulamayı kapat
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Listele("SELECT * FROM Kargo" , dataGridView4);
        }
        private void Listele(string query, DataGridView dataGridView)
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

        private void guncelle3_Click_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView4.SelectedRows[0].Index;
                int kargoID = Convert.ToInt32(dataGridView4.Rows[selectedRowIndex].Cells["KargoID"].Value);

                // Güncelleme işlemini gerçekleştirmek için gerekli kontrolleri oluşturun
                DateTime yeniKargoTarihi = Convert.ToDateTime(dataGridView4.Rows[selectedRowIndex].Cells["KargoTarihi"].Value);
                string yeniTeslimatAdresi = dataGridView4.Rows[selectedRowIndex].Cells["TeslimatAdresi"].Value.ToString();
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
                                Listele("SELECT * FROM Kargo", dataGridView4);
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView4.SelectedRows[0].Index;
                int kargoID = Convert.ToInt32(dataGridView4.Rows[selectedRowIndex].Cells["KargoID"].Value);

                DialogResult result = MessageBox.Show("Seçili kargo kaydını silmek istiyor musunuz?", "Kargo Kaydını Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    KargoKaydiniSil(kargoID);
                    Listele("SELECT * FROM Kargo", dataGridView4); // Güncellenmiş verileri tekrar yükle
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
    }
}
