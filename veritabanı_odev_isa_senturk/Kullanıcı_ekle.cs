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
    public partial class Kullanıcı_ekle : Form
    {
        private const string ConnectionString = "Data Source=DESKTOP-S4H53I6\\SQLEXPRESS;Initial Catalog=isa_senturk_veritabanı_odev;Integrated Security=True";
        public Kullanıcı_ekle()
        {
            InitializeComponent();
        }

        private void Ad_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Kullanıcı_ekle_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ad = Ad1.Text;
            string soyad = soyadekle.Text;
            string eposta = epostaekle.Text;
            string sifre = sifreekle.Text;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string kullaniciEkleQuery = "INSERT INTO Kullanicilar (Ad, Soyad, Eposta, Sifre) VALUES (@Ad, @Soyad, @Eposta, @Sifre);";
                using (SqlCommand command = new SqlCommand(kullaniciEkleQuery, connection))
                {
                    command.Parameters.AddWithValue("@Ad", ad);
                    command.Parameters.AddWithValue("@Soyad", soyad);
                    command.Parameters.AddWithValue("@Eposta", eposta);
                    command.Parameters.AddWithValue("@Sifre", sifre);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Kullanıcı başarıyla eklendi.");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Miktar_Click(object sender, EventArgs e)
        {

        }

        private void UrunAdı_Click(object sender, EventArgs e)
        {

        }
    }
}
