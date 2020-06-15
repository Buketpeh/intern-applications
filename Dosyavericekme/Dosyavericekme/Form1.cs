using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Data.SqlClient;
using System.Reflection;

//PROGRAM AMACI: TXT DOSYASINDAN ALINAN KISALTMA ŞEKLİNDEKİ İSİMLER, TABLO VE KOLON İSİMLERİNE 
//GÖRE AÇIKLAMALARI BULUNAANARAK LİSTBOXLARA VE TXT DOSYASINA YAZDIRILMAKTADIR.
namespace Dosyavericekme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      

        string test = "";
        string test1 = "";
        string DosyaYolu;
        string DosyaAdi;
        string DosyaYolu1;
        string DosyaAdi1;
        string ayrac = "|";

        //dosya seçmek için olan buton 
        private void Button2_Click(object sender, EventArgs e)
        {
            test = "basıldı";
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Text Dosyası|*.txt";
            file.RestoreDirectory = false; //her açıldığında bir önceki klasörü açar
            file.CheckFileExists = false; // yazdığımızda dosya var mı yok mu kontrolü yapar
            file.Title = "Bir metin belgesi seçiniz...";
            if (file.ShowDialog() == DialogResult.OK)
            {
                DosyaYolu = file.FileName; //seçilen dosyanın tüm yolunu verir
                DosyaAdi = file.SafeFileName; //seçilen dosyanın adını 
            }

            file.ShowDialog();
            file.Reset();
        }

        //tablo isimlerini listeleyecek olan buton
        private void Button4_Click(object sender, EventArgs e)
        {

            if (test != "basıldı")
            {
                MessageBox.Show("Lütfen okunacak dosya seçiniz!");
            }
            else
            {

                string connectionString;
                SqlConnection cnn;

                connectionString = @"Data Source=DESKTOP-DG89RRT;Initial Catalog=ogrencitakip ; Integrated Security = True";
                cnn = new SqlConnection(connectionString);

                cnn.Open();

                const string quote = "\"";
                const string quote1 = "@";



                TextReader tReader = new StreamReader(DosyaYolu);
                string[] okunan = new string[] { "\r\n" }; //her satırı ayrı ayrı alabilmek için split ediyoruz.
                string text = tReader.ReadToEnd();
                string[] satirlar = text.Split(okunan, StringSplitOptions.None);


                foreach (var item in satirlar)
                {

                    string[] dizi = item.Split('_');

                    string query = "select (";



                    foreach (var item1 in dizi)
                    {
                        query += " ISNULL((select top 1 aciklama from kayit where tur='t' and kısaltma ='" + item1 + "' ),'?') + ' ' +";
                    }
                    query += "'') as tablocolonaciklama";

                    SqlCommand cmd = new SqlCommand(query, cnn);

                    SqlDataReader dr = cmd.ExecuteReader();


                    while (dr.Read())
                    {

                        listBox1.Items.Add(item + '|' + dr["tablocolonaciklama"].ToString());
                    }


                    if (!dr.IsClosed)
                        dr.Close();
                }
                cnn.Close();
            }

        }

        //kolon isimlerini ve açıklamalarını listelemek için olan buton
        private void Button1_Click(object sender, EventArgs e)
        {

            if (test != "basıldı")
            {
                MessageBox.Show("Lütfen okunacak dosya seçiniz!");
            }

            else
            { 
                string connectionString;
                SqlConnection cnn;

                connectionString = @"Data Source=DESKTOP-DG89RRT;Initial Catalog=ogrencitakip ; Integrated Security = True";
        
                cnn = new SqlConnection(connectionString);

                cnn.Open();

                TextReader tReader1 = new StreamReader(DosyaYolu);
                string[] okunan1 = new string[] { "\r\n" };
                string text1 = tReader1.ReadToEnd();
                string[] satirlar1 = text1.Split(okunan1, StringSplitOptions.None);


                foreach (var item1 in satirlar1)
                {

                  string[] dizi1 = item1.Split('_');

                  string query = "select (";



                  foreach (var item2 in dizi1)
                  {
                    query += " ISNULL((select top 1 aciklama from kayit where tur='k' and  kısaltma ='" + item2 + "' ),'?') + ' ' +";
                  }
                    query += "'') as tablocolonaciklama";


 
                    
                   SqlCommand cmd = new SqlCommand(query, cnn);

                   SqlDataReader dr = cmd.ExecuteReader();


                   while (dr.Read())
                   {

                    listBox2.Items.Add(item1 + '|' + dr["tablocolonaciklama"].ToString());
                   }


                   if (!dr.IsClosed)
                    dr.Close();
                   }
                   cnn.Close();

             }

        }



       private void Button5_Click(object sender, EventArgs e)
       {
            test1 = "basıldı";
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Text Dosyası|*.txt";
            file.RestoreDirectory = true; //her açıldığında bir önceki klasörü açar
            file.CheckFileExists = false;
            // yazdığımızda dosya var mı yok mu kontrolü yapar
            file.Title = "Bir metin belgesi seçiniz...";
            if (file.ShowDialog() == DialogResult.OK)
            {
                DosyaYolu1 = file.FileName; //seçilen dosyanın tüm yolunu verir
                DosyaAdi1 = file.SafeFileName; //seçilen dosyanın adını 
            }

            file.ShowDialog();
            file.Reset();

       }



        private void Button3_Click(object sender, EventArgs e)
        {
            if (test1 !="basıldı")
            {
                MessageBox.Show("Lütfen yazılacak dosya seçiniz!");
            }

            else {

                   if (comboBox1.Text== "tablo ve kolon açıklamaları")
                   {
                        string sPath =  DosyaYolu1.ToString();

                       System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
                       foreach(var item5 in listBox1.Items)
                       { 

                       SaveFile.WriteLine(item5.ToString());
                       }

                       foreach (var item6 in listBox2.Items)
                       {

                       SaveFile.WriteLine(item6.ToString());
                       }

                       SaveFile.Close();

                       MessageBox.Show("Programs saved!");

                   }



                   else if (comboBox1.Text == "tablo açıklamaları")
                   {
                       string sPath = DosyaYolu1.ToString();

                       System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
                       foreach (var item5 in listBox1.Items)
                       {

                        SaveFile.WriteLine(item5.ToString());
                       }

                       SaveFile.Close();

                       MessageBox.Show("Programs saved!");

                   }


                  else if (comboBox1.Text == "kolon açıklamaları")
                   {
                     string sPath = DosyaYolu1.ToString();

                     System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);

                      foreach (var item6 in listBox2.Items)
                      {

                      SaveFile.WriteLine(item6.ToString());

                      }

                      SaveFile.Close();

                      MessageBox.Show("Programs saved!");
                      }

                      else
                      {
                      MessageBox.Show("Please choose one!");
                      }


        
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
        int sonuc;
        sonuc = listBox1.FindString(textBox1.Text);
        listBox1.SelectedIndex = sonuc;

        int sonuc1;
        sonuc1 = listBox2.FindString(textBox2.Text);
        listBox2.SelectedIndex = sonuc1;

        }
    }
}
