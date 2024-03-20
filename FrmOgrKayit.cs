using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Personel_Takip_Programı
{
    public partial class FrmOgrKayit : Form
    {
        public FrmOgrKayit()
        {
            InitializeComponent();
        }

        SqlBaglantim bgl = new SqlBaglantim();

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            SqlCommand komut = new SqlCommand("Select BolumAd From Bolumler", bgl.baglanti());
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBolum.Items.Add(oku[0].ToString());
            }
            bgl.baglanti().Close();

           
            SqlCommand komut2 = new SqlCommand("Select OdaNo From Odalar where OdaKapasite != OdaAktif", bgl.baglanti());
            SqlDataReader oku2 = komut2.ExecuteReader();
            while(oku2.Read())
            {
                comboOda.Items.Add(oku2[0].ToString()); 
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //öprenci bilgilerinin kayıt edilme komutları
            try
            {
                
                SqlCommand komutKaydet = new SqlCommand("insert into Ogrenci (OgrAd, OgrSoyad, OgrTc, OgrTelefon, OgrDogum, OgrBolum, OgrMail, OgrOdaNo, OgrVeliAdSoyad, OgrVeliTelefon, OgrVeliAdres) values (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)", bgl.baglanti());
                komutKaydet.Parameters.AddWithValue("@p1", TxtOgrAd.Text);
                komutKaydet.Parameters.AddWithValue("@p2", TxtOgrSoyad.Text);
                komutKaydet.Parameters.AddWithValue("@p3", MskTc.Text);
                komutKaydet.Parameters.AddWithValue("@p4", MskOgrTel.Text);
                komutKaydet.Parameters.AddWithValue("@p5", MskDogum.Text);
                komutKaydet.Parameters.AddWithValue("@p6", comboBolum.Text);
                komutKaydet.Parameters.AddWithValue("@p7", TxtMail.Text);
                komutKaydet.Parameters.AddWithValue("@p8", comboOda.Text);
                komutKaydet.Parameters.AddWithValue("@p9", txtVeliAdSoyad.Text);
                komutKaydet.Parameters.AddWithValue("@p10", mskVeliTel.Text);
                komutKaydet.Parameters.AddWithValue("@p11", richAdres.Text);
                komutKaydet.ExecuteNonQuery();
                bgl.baglanti().Close();

              
                //öğrenci idyi labela çekme
                SqlCommand komut3 = new SqlCommand("select OgrId from Ogrenci", bgl.baglanti());
                SqlDataReader oku = komut3.ExecuteReader();
                while (oku.Read())
                {
                    label12.Text = oku[0].ToString();

                }
                bgl.baglanti().Close();


                //öğrenci borç alanı oluşturma
                SqlCommand komutkaydet2 = new SqlCommand("insert into Borclar (OgrId, OgrAd, OgrSoyad) values (@b1, @b2, @b3)", bgl.baglanti());
                komutkaydet2.Parameters.AddWithValue("@b1", label12.Text);
                komutkaydet2.Parameters.AddWithValue("@b2", TxtOgrAd.Text);
                komutkaydet2.Parameters.AddWithValue("@b3", TxtOgrSoyad.Text);
                komutkaydet2.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Öğrenci Başarıyla Kaydedildi.");
            }
            catch 
            {

                MessageBox.Show("Öğrenci Kaydedilemedi. Lütfen Tekrar Deneyin. " , "HATA");
            }

            //öğrenci oda kontenjanı arttırma
            SqlCommand komutoda = new SqlCommand("update Odalar set OdaAktif=OdaAktif+1 where OdaNo=@oda1", bgl.baglanti());
            komutoda.Parameters.AddWithValue("@oda1", comboOda.Text);
            komutoda.ExecuteNonQuery();
            bgl.baglanti() .Close();
            
        }
    }
}
