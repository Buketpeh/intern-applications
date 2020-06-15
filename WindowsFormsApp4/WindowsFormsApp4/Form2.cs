using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Data.SqlClient;
using System.Reflection;

namespace WindowsFormsApp4
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        string DosyaYolu = "";
        string DosyaYolu1 = "";
        string DosyaYolu2 = "";

        string DosyaAdi = "";
        string DosyaAdi1 = "";
        string DosyaAdi2 = "";

        String test = "";
        String test1 = "";
        String test2 = "";

        public void Button1_Click(object sender, EventArgs e)
        {



            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Text Dosyası|*.txt";
            file.RestoreDirectory = true; //her açıldığında bir önceki klasörü açar
            file.CheckFileExists = false; // yazdığımızda dosya var mı yok mu kontrolü yapar
            file.Title = "Bir metin belgesi seçiniz...";
            if (file.ShowDialog() == DialogResult.OK)
            {
                DosyaYolu = file.FileName; //seçilen dosyanın tüm yolunu verir
                DosyaAdi = file.SafeFileName;//seçilen dosyanın adını verir
                test = "basıldı";
            }


            file.ShowDialog();
            file.Reset();
            label2.Text = DosyaAdi;
        }

        private void Button2_Click(object sender, EventArgs e)
        {


            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Text Dosyası|*.txt";
            file.RestoreDirectory = true; //her açıldığında bir önceki klasörü açar
            file.CheckFileExists = false; // yazdığımızda dosya var mı yok mu kontrolü yapar
            file.Title = "Bir metin belgesi seçiniz...";
            if (file.ShowDialog() == DialogResult.OK)
            {
                DosyaYolu1 = file.FileName; //seçilen dosyanın tüm yolunu verir
                DosyaAdi1 = file.SafeFileName; //seçilen dosyanın adını verir
                test1 = "basıldı"; //dosya seçildi kontrolü
            }


            file.ShowDialog();
            file.Reset();
            label4.Text = DosyaAdi1;
        }
        private void Button4_Click(object sender, EventArgs e) //yazılacak dosyanın seçildiği buton
        {


            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Text Dosyası|*.txt";
            file.RestoreDirectory = true; //her açıldığında bir önceki klasörü açar
            file.CheckFileExists = false; // yazdığımızda dosya var mı yok mu kontrolü yapar
            file.Title = "Bir metin belgesi seçiniz...";
            if (file.ShowDialog() == DialogResult.OK)
            {
                DosyaYolu2 = file.FileName; //seçilen dosyanın tüm yolunu verir
                DosyaAdi2 = file.SafeFileName;//seçilen dosyanın adını verir.
                test2 = "basıldı";

            }


            file.ShowDialog();
            file.Reset();
            label6.Text = DosyaAdi2;
        }




        private void Button3_Click(object sender, EventArgs e)
        {

            if (test == "basıldı" && test1 == "basıldı" && test2 != "basıldı")
                MessageBox.Show("Lütfen, yazdırmak istediğiniz dosyayı  seçiniz.");
            if (test == "basıldı" && test1 != "basıldı" && test2 == "basıldı")
                MessageBox.Show("Lütfen,tablo-kolon kısaltma dosyasını seçiniz.");
            if (test == "basıldı" && test1 != "basıldı" && test2 != "basıldı")
                MessageBox.Show("Lütfen,tablo-kolon kısaltma dosyası ve yazdırılmak istenilen dosyayı seçiniz.");
            if (test != "basıldı" && test1 == "basıldı" && test2 == "basıldı")
                MessageBox.Show("Lütfen, tablo-kolon isim dosyasını seçiniz.");
            if (test != "basıldı" && test1 == "basıldı" && test2 != "basıldı")
                MessageBox.Show("Lütfen, tablo-kolon isim dosyasını ve yazdırılmak istenilen dosyayı seçiniz.");
            if (test != "basıldı" && test1 != "basıldı" && test2 == "basıldı")
                MessageBox.Show("Lütfen, tablo-kolon isim dosyasını ve tablo-kolon kısaltma dosyasını seçiniz.");
            if (test != "basıldı" && test1 != "basıldı" && test2 != "basıldı")
                MessageBox.Show("Hiç bir dosya seçimi yapmadınız.Lütfen, dosya seçimi yapınız.");

            if (test == "basıldı" && test1 == "basıldı" && test2 == "basıldı")
            {



                TextReader tReader = new StreamReader(DosyaYolu); //isimler olan .txtden okuma


                string[] okunan = new string[] { "\r\n" };
                string text = tReader.ReadToEnd();
                string[] satirlar = text.Split(okunan, StringSplitOptions.None);

                List<string> diziler = new List<string>();

                DataTable tbl1 = new DataTable();

                tbl1.Columns.Add("isim");
                tbl1.Columns.Add("Tip");
                tbl1.Columns.Add("Dil");


                foreach (var item in satirlar)
                {
                    if (item != "") //satır sonunda oluşan boşlukların kontrole dahil olmasını önlemek amaçlı
                    {
                        string[] ayırım = item.Split('|');


                        DataRow row = tbl1.NewRow();
                        row["isim"] = ayırım[0].ToString();
                        row["Tip"] = ayırım[1].ToString();
                        row["Dil"] = ayırım[2].ToString();
                        tbl1.Rows.Add(row);
                    }


                    else
                    {
                        continue;
                    }


                }


                TextReader t1Reader = new StreamReader(DosyaYolu1); //kısaltmalar içinolan .txtden okuma

                string[] okunan1 = new string[] { "\r\n" };
                string text1 = t1Reader.ReadToEnd();
                string[] satirlar1 = text1.Split(okunan1, StringSplitOptions.None);

                List<string> diziler1 = new List<string>();

                DataTable tbl2 = new DataTable();

                tbl2.Columns.Add("kısaltma");
                tbl2.Columns.Add("acıklama");
                tbl2.Columns.Add("tip");
                tbl2.Columns.Add("dil");


                foreach (var item1 in satirlar1)
                {
                    if (item1 != "") //satır sonunda oluşan boşlukların kontrole dahil olmasını önlemek amaçlı
                    {
                        string[] ayrırım1 = item1.Split('|');

                        DataRow row = tbl2.NewRow();
                        row["kısaltma"] = ayrırım1[0].ToString();
                        row["acıklama"] = ayrırım1[1].ToString();
                        row["tip"] = ayrırım1[2].ToString();
                        row["dil"] = ayrırım1[3].ToString();
                        tbl2.Rows.Add(row);
                    }

                    else
                    {
                        continue;
                    }

                }


                string dosya_yolu = DosyaYolu2;

                //karşılaştırma yapıldıktan sonra uzun açıklamaların yazılacağı .txt
                FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Write);

                //bir file stream nesnesi oluşturuyoruz. 1. parametre dosya yolunu,
                //2. parametre dosya açma
                //3.parametre dosya erişiminin veri yazmak için olacağını gösterir.

                StreamWriter sw = new StreamWriter(fs);

                //karşılaştırma işlemleri ve uzun açıklamanın oluşturulması burada gerçekleştiriliyor.
                string[] a = new string[tbl1.Rows.Count];

                for (int i = 0; i < tbl1.Rows.Count; i++)
                {

                    string[] ayırım3 = tbl1.Rows[i]["isim"].ToString().Split('_');


                    foreach (var item in ayırım3)
                    {


                        for (int j = 0; j < tbl2.Rows.Count; j++)
                        {

                            if (item.ToString() == tbl2.Rows[j]["kısaltma"].ToString())
                            {

                                if (tbl1.Rows[i]["tip"].ToString() == tbl2.Rows[j]["tip"].ToString())
                                {

                                    if (tbl1.Rows[i]["dil"].ToString() == tbl2.Rows[j]["dil"].ToString())
                                    {

                                        a[i] += tbl2.Rows[j]["acıklama"].ToString() + ' ';



                                    }
                                }
                            }

                        }

                        //tablo-kolon isim .txt'sindeki tüm sütunlar ve uzun açıklamaları burada başka bir .txt'ye yazılyor.
                        for (int k = 0; k < tbl1.Rows.Count; k++)
                        {

                            sw.WriteLine(tbl1.Rows[k]["isim"].ToString() + '|' + a[k].ToString() + '|' + tbl1.Rows[k]["tip"].ToString() + '|' + tbl1.Rows[k]["dil"].ToString());
                        }


                        sw.Flush();
                        sw.Close();
                        fs.Close();

                        MessageBox.Show("İşleminiz gerçekleşti.");

                        Form.ActiveForm.Close();
                    }
                }


            }
        }
    }
}
