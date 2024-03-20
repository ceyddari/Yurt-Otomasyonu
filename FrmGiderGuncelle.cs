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
    public partial class FrmGiderGuncelle : Form
    {
        public FrmGiderGuncelle()
        {
            InitializeComponent();
        }
        public string elektrik, su, dogalgaz, internet, gida, personel, diger, id;
        SqlBaglantim bgl = new SqlBaglantim();

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("update Giderler set Elektrik=@p1, Su=@p2, Dogalgaz=@p3, Internet=@p4, Gida=@p5, Personel=@p6, Diger=@p7 where OdemeId=@p8", bgl.baglanti());
                komut.Parameters.AddWithValue("@p8", txtId.Text);
                komut.Parameters.AddWithValue("@p1", txtElektrik.Text);
                komut.Parameters.AddWithValue("@p2", txtSu.Text);
                komut.Parameters.AddWithValue("@p3", txtDogalgaz.Text);
                komut.Parameters.AddWithValue("@p4", txtInt.Text);
                komut.Parameters.AddWithValue("@p5", txtGida.Text);
                komut.Parameters.AddWithValue("@p6", txtPersonel.Text);
                komut.Parameters.AddWithValue("@p7", txtDiger.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Bilgiler Başarıyla Güncellendi.");
            }
            catch (Exception)
            {

                MessageBox.Show("Güncellenemedi. Lütfen Tekrar Deneyin.");
            }
        }

        private void FrmGiderGuncelle_Load(object sender, EventArgs e)
        {
            txtId.Text = id;
            txtElektrik.Text = elektrik;
            txtSu.Text = su;
            txtDogalgaz.Text = dogalgaz;
            txtInt.Text = internet;
            txtGida.Text = gida;
            txtPersonel.Text = personel;    
            txtDiger.Text = diger;  
            
        }
    }
}
