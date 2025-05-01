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
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=DESKTOP-S4H53I6\\SQLEXPRESS;Initial Catalog=isa_senturk_veritabanı_odev;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcıları listeleme sorgusunu çağır
            List<UserData> userList = ListAllUsers();

            // Elde edilen veriyi kullanarak istediğiniz işlemleri gerçekleştirin
            // Örneğin, bir DataGridView kontrolüne verileri bağlama gibi

            // Örnek olarak, DataGridView kontrolüne bağlamak:
            dataGridView1.DataSource = userList;
        }

        // Kullanıcıları listeleme sorgusunu gerçekleştiren metod
        private List<UserData> ListAllUsers()
        {
            List<UserData> userList = new List<UserData>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            SELECT DISTINCT 
                Kullanicilar.KullaniciID, Kullanicilar.Ad, Kullanicilar.Soyad, Kullanicilar.Eposta, Kullanicilar.Sifre,
                Siparisler.SiparisID, Siparisler.ToplamFiyat, Siparisler.SiparisTarihi,
                SiparisDetaylari.DetayID, SiparisDetaylari.UrunID, SiparisDetaylari.Miktar,
                Odemeler.OdemeID, Odemeler.OdemeTarihi, Odemeler.OdemeMiktari, Odemeler.OdemeYontemi,
                Kargo.KargoID, Kargo.KargoTarihi, Kargo.TeslimatAdresi
            FROM Kullanicilar
            INNER JOIN Siparisler ON Kullanicilar.KullaniciID = Siparisler.KullaniciID
            LEFT JOIN SiparisDetaylari ON Siparisler.SiparisID = SiparisDetaylari.SiparisID
            LEFT JOIN Odemeler ON Siparisler.SiparisID = Odemeler.SiparisID
            LEFT JOIN Kargo ON Siparisler.SiparisID = Kargo.SiparisID
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Verileri okuyarak bir UserData nesnesi oluşturun ve listeye ekleyin
                            UserData userData = new UserData
                            {
                                // Veritabanından alınan sütunları uygun şekilde atayın
                                KullaniciID = Convert.ToInt32(reader["KullaniciID"]),
                                Ad = reader["Ad"].ToString(),
                                Soyad = reader["Soyad"].ToString(),
                                // Diğer sütunları da ekleyin
                                // ...
                                SiparisID = Convert.ToInt32(reader["SiparisID"]),
                                ToplamFiyat = Convert.ToDecimal(reader["ToplamFiyat"]),
                                // Diğer sütunları da ekleyin
                                // ...
                            };

                            userList.Add(userData);
                        }
                    }
                }
            }

            return userList;
        }

        // UserData sınıfı, verileri tutmak için kullanılabilir
        public class UserData
        {
            public int KullaniciID { get; set; }
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public string Eposta { get; set; }
            public string Sifre { get; set; }

            public int SiparisID { get; set; }
            public decimal ToplamFiyat { get; set; }
            public DateTime SiparisTarihi { get; set; }

            public int DetayID { get; set; }
            public int UrunID { get; set; }
            public int Miktar { get; set; }

            public int OdemeID { get; set; }
            public DateTime OdemeTarihi { get; set; }
            public decimal OdemeMiktari { get; set; }
            public string OdemeYontemi { get; set; }

            public int KargoID { get; set; }
            public DateTime KargoTarihi { get; set; }
            public string TeslimatAdresi { get; set; }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
