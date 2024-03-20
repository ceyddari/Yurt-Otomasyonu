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
    public partial class FrmBolumler : Form
    {
        SqlBaglantim bgl = new SqlBaglantim();
        public FrmBolumler()
        {
            InitializeComponent();
        }

        private void FrmBolumler_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'yurt_OtomasyonuDataSet.Bolumler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.bolumlerTableAdapter.Fill(this.yurt_OtomasyonuDataSet.Bolumler);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pcbBolumEkle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut1 = new SqlCommand("insert into Bolumler (BolumAd) values (@p1)", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", txtBolumAd.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Bölüm Başarıyla Eklendi.");
                this.bolumlerTableAdapter.Fill(this.yurt_OtomasyonuDataSet.Bolumler);


            }
            catch 
            {
                MessageBox.Show("Bölüm Eklenemedi Lütfen Yeniden Deneyin.", "HATA!");
            }
        }

        private void pcbBolumSil_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut2 = new SqlCommand("delete from Bolumler where BolumId=@p1", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", txtBolumId.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Silme İşlemi Başarıyla Gerçekleşti.");
                this.bolumlerTableAdapter.Fill(this.yurt_OtomasyonuDataSet.Bolumler);
            }
            catch 
            {
                MessageBox.Show("Hata. İşlem gerçekleştirilemedi.");
                
            }

        }
        int secilen;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string id, bolumad;
            secilen = dataGridView1.SelectedCells[0].RowIndex;  
            id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            bolumad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();

            txtBolumId.Text= id;    
            txtBolumAd.Text= bolumad;   
        }

        private void pcbBolumDuzenle_Click(object sender, EventArgs e)
        {
            
            SqlCommand komut3 = new SqlCommand("update Bolumler Set BolumAd=@p1 where BolumId=@p2", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p2", txtBolumId.Text);
            komut3.Parameters.AddWithValue("@p1", txtBolumAd.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Başarıyla Güncellendi.");
            this.bolumlerTableAdapter.Fill(this.yurt_OtomasyonuDataSet.Bolumler);
        }
    }
}
