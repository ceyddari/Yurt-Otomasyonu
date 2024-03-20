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

namespace Personel_Takip_Programı
{
    public partial class FrmOgrDuzenle : Form
    {
        public FrmOgrDuzenle()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl = new SqlBaglantim();  
        public string id, ad, soyad, tc, telefon, dogum, bolum;

        private void txtOgrId_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //öğrenci silme
            SqlCommand komutsil = new SqlCommand("delete from Ogrenci where OgrId=@k1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@k1", txtOgrId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silindi.");


            //oda kontenjanı artırma
            SqlCommand komutoda = new SqlCommand("update odalar set OdaAktif=OdaAktif-1 where OdaNo=@oda", bgl.baglanti());
            komutoda.Parameters.AddWithValue("@oda", comboOda.Text);
            komutoda.ExecuteNonQuery();
            bgl.baglanti().Close();

        }

        private void comboBolum_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboOda_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
            

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            try
            {
                SqlCommand komut = new SqlCommand("update Ogrenci set OgrAd=@p2, OgrSoyad=@p3, OgrTc=@p4, OgrTelefon=@p5, OgrDogum=@p6, OgrBolum=@p7, OgrOdaNo=@p8, OgrMail=@p8, OgrVeliAdSoyad=@p9, OgrVeliTelefon=@p10, OgrVeliAdres=@p11 where OgrId=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtOgrId.Text);
                komut.Parameters.AddWithValue("@p2", TxtOgrAd.Text);
                komut.Parameters.AddWithValue("@p3", TxtOgrSoyad.Text);
                komut.Parameters.AddWithValue("@p4", MskTc.Text);
                komut.Parameters.AddWithValue("@p5", MskOgrTel.Text);
                komut.Parameters.AddWithValue("@p6", MskDogum.Text);
                komut.Parameters.AddWithValue("@p7", comboOda.Text);
                komut.Parameters.AddWithValue("@p8", TxtMail.Text);
                komut.Parameters.AddWithValue("@p9", txtVeliAdSoyad.Text);
                komut.Parameters.AddWithValue("@p10", mskVeliTel.Text);
                komut.Parameters.AddWithValue("@p11", richAdres.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                

                MessageBox.Show("Öğrenci Bilgileri Başarıyla Güncellendi.");
            }
            catch 
            {

                MessageBox.Show("Öğrenci Bilgileri Güncellenemedi.", "HATA");
            }
            
        }

        public string odano, mail, veliad, velitel, adres;
        private void FrmOgrDuzenle_Load(object sender, EventArgs e)
        {
            txtOgrId.Text = id;
            TxtOgrAd.Text = ad; 
            TxtOgrSoyad.Text = soyad;
            MskTc.Text = tc;
            MskOgrTel.Text = telefon;
            MskDogum.Text = dogum;
            comboBolum.Text = bolum;
            comboOda.Text = odano;
            TxtMail.Text = mail;
            txtVeliAdSoyad.Text = veliad;
            mskVeliTel.Text = velitel;
            richAdres.Text = adres;



            SqlCommand komut = new SqlCommand("Select BolumAd From Bolumler", bgl.baglanti());
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBolum.Items.Add(oku[0].ToString());
            }
            bgl.baglanti().Close();


            SqlCommand komut2 = new SqlCommand("Select OdaNo From Odalar where OdaKapasite != OdaAktif", bgl.baglanti());
            SqlDataReader oku2 = komut2.ExecuteReader();
            while (oku2.Read())
            {
                comboOda.Items.Add(oku2[0].ToString());
            }
            bgl.baglanti().Close();


        }
    }
}
