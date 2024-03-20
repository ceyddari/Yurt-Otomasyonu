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
    public partial class FrmOdemeler : Form
    {
        SqlBaglantim bgl = new SqlBaglantim();
        public FrmOdemeler()
        {
            InitializeComponent();
        }

        private void FrmOdemeler_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'yurt_OtomasyonuDataSet3.Borclar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.borclarTableAdapter1.Fill(this.yurt_OtomasyonuDataSet3.Borclar);
            // TODO: Bu kod satırı 'yurt_OtomasyonuDataSet2.Borclar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.borclarTableAdapter.Fill(this.yurt_OtomasyonuDataSet2.Borclar);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen;
            string id, ad, soyad, kalan;
            secilen = dataGridView1.SelectedCells[0].RowIndex;
            id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            ad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            soyad= dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            kalan= dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            txtOgrAd.Text = ad;
            txtOgrSoyad.Text = soyad;   
            txtOgrID.Text = id;
            txtKalanBorc.Text = kalan;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ödenen tutarı kalan tutardan düşürme
            int odenen, kalan, yeniborc;
            odenen=Convert.ToInt16(txtOgrOdenen.Text);
            kalan = Convert.ToInt16(txtKalanBorc.Text);
            yeniborc = kalan - odenen;
            txtKalanBorc.Text = yeniborc.ToString();

            //yeni tutarı veritabanına kaydetme
            SqlCommand komut = new SqlCommand("update Borclar set OgrKalanBorc=@p1  where OgrId=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p2", txtOgrID.Text);
            komut.Parameters.AddWithValue("@p1", txtKalanBorc.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ödeme Başarıyla Tamamlandı.");
            this.borclarTableAdapter1.Fill(this.yurt_OtomasyonuDataSet3.Borclar);

            //kasa tablosuna ekleme yapma
            SqlCommand komut2 = new SqlCommand("insert into Kasa (OdemeAy, OdemeMiktar) values (@k1, @k2)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@k1", txtOdenenAy.Text);
            komut2.Parameters.AddWithValue("@k2", txtOgrOdenen.Text);
            komut2.ExecuteNonQuery();   
            bgl.baglanti().Close();


        }
    }
}
