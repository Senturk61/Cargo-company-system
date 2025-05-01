using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace veritabanı_odev_isa_senturk
{
    public partial class GirisFormu : Form
    {

        SqlConnection con;
        SqlDataReader dr;
        SqlCommand com;
        private string connectionString = "Data Source=DESKTOP-S4H53I6\\SQLEXPRESS;Initial Catalog=isa_senturk_veritabanı_odev;Integrated Security=True";
        public GirisFormu()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
   
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            // Kullanıcı adı ve şifreyi kontrol et
            if (CheckCredentials(username, password))
            {
                // Kullanıcı türüne göre yönlendirme yap
                int userRole = GetUserRole(username);

                switch (userRole)
                {
                    case 1:
                        // 1 rolü ise müşteri paneline yönlendir
                        musteri_paneli musteriPanel = new musteri_paneli();
                        musteriPanel.Show();
                        break;
                    case 2:
                        // 2 rolü ise kargocu paneline yönlendir
                        kargocu_paneli kargocuPanel = new kargocu_paneli();
                        kargocuPanel.Show();
                        break;
                    case 3:
                        // 3 rolü ise yönetici paneline yönlendir
                        yonetici_paneli yoneticiPanel = new yonetici_paneli();
                        yoneticiPanel.Show();
                        break;
                    default:
                        MessageBox.Show("Tanımsız kullanıcı rolü!");
                        break;
                }

                // Giriş başarılı, mevcut formu gizle
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış!");
            }
        }

        // Kullanıcının rolünü getiren metod
        private int GetUserRole(string username)
        {
            // Kullanıcının rolünü veritabanından al
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Parametre kullanarak SQL sorgusu oluştur
                string sql = "SELECT RolID FROM KullaniciRolleri WHERE KullaniciID = (SELECT TOP 1 KullaniciID FROM Kullanicilar WHERE Ad = @Username)";


                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Parametreyi ekleyerek SQL sorgusunu güvenli hale getir
                    command.Parameters.AddWithValue("@Username", username);

                    // ExecuteScalar kullanarak tek bir değeri al
                    int userRole = (int)command.ExecuteScalar();

                    return userRole;
                }
            }
        }
        private bool CheckCredentials(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Parametre kullanarak SQL sorgusu oluştur
                string sql = "SELECT COUNT(*) FROM Kullanicilar WHERE Ad = @Username AND Sifre = @Password";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Parametreleri ekleyerek SQL sorgusunu güvenli hale getir
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    // ExecuteScalar kullanarak sonucu al
                    int userCount = (int)command.ExecuteScalar();

                    // Kullanıcı varsa ve sadece admini kontrol etmiyorsak true döndür
                    return userCount > 0;
                }
            }
        }
    }
}
