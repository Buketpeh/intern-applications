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
using System.Reflection;

//using System.Data.SqlClient.SqlException;
namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool ButtonGetir = true;
        private void Button1_Click(object sender, EventArgs e)
        {

            if (ButtonGetir) { 
            string connectionString;
            SqlConnection cnn;

            connectionString = @"Data Source=DESKTOP-DG89RRT;Initial Catalog=ogrencitakip ; Integrated Security = True";
            //"Data Source = DESKTOP - DG89RRT; Initial Catalog = ogrencitakip; Integrated Security = True"
            cnn = new SqlConnection(connectionString);

         cnn.Open();
       

            SqlCommand cmd = new SqlCommand("select * from tabloisimleri", cnn);
            SqlDataReader dr = cmd.ExecuteReader();
            List<string> liste = new List<string>();
            while(dr.Read())
            {
                
                liste.Add(dr["tabloadi"].ToString()); 
           
            }


            if (!dr.IsClosed)
                dr.Close();

            foreach (var item in liste)
            {
                string[] colonliste = item.Split('_');

                string query = "select (";

                foreach (var colon in colonliste)
                {
                    query += " ISNULL((select distinct aciklama from kayit where tur='t' and kısaltma ='" + colon + "' ),'') + ' ' +";
                }
                query += "'') as tabloaciklama";

                cmd = new SqlCommand(query, cnn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listBox1.Items.Add(item +'-' + dr["tabloaciklama"].ToString());
                }


                if (!dr.IsClosed)
                    dr.Close();
            }

         
            //---------------------------------------------------------------------------------------------------------------
            SqlCommand cmd2 = new SqlCommand("select * from kayit", cnn);
            SqlCommand cmd1 = new SqlCommand("select * from Kolonisimleri", cnn);
            dr = cmd1.ExecuteReader();
           
          
            List<string> liste1 = new List<string>();
            List<string> liste2 = new List<string>();

//kolon adı için okuma yapıyoruz
            while (dr.Read())
            {
                liste1.Add(dr["kolonadi"].ToString());
              
            }

            if (!dr.IsClosed)
                dr.Close();





            foreach (var item1 in liste1)
            {
                string[] colonliste1 = item1.Split('_');

                string query1 = "select (";

                foreach (var colon1 in colonliste1)
                {
                    query1 += " ISNULL((select top 1  aciklama from kayit where tur='k' and  kısaltma ='" + colon1 + "' ),'') + ' ' +";
                }
                query1 += "'') as kolonaciklama";
                     
          


                cmd1 = new SqlCommand(query1, cnn);
                dr = cmd1.ExecuteReader();

              

               // dr = cmd2.ExecuteReader();


                while (dr.Read())
                {
                    listBox2.Items.Add(item1 +'-' + dr["kolonaciklama"].ToString());
                }


                if (!dr.IsClosed)
                    dr.Close();
            }

           

            //-------------------------------------------------
          


            /*  //extboxtan alınan veri açıklaması ile yazılıyor


                        string colonadı = textBox1.Text.Trim();
                        string[] colonliste= colonadı.Split('_');
                        DataSet sonuc = new DataSet();
                        for(int i=0; i<colonliste.Length;i++)
                        {
                          adaptor= new SqlDataAdapter("select * from kayit where kısaltma ='"+colonliste[i]+"' ", cnn);
                          adaptor.Fill(sonuc, "dt");
                            textBox2.Text += ' '+ sonuc.Tables[0].Rows[i]["aciklama"].ToString().Trim();
                        }*/
            cnn.Close();
                ButtonGetir = false;
}}

     
      


        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int sonuc;
            sonuc = listBox1.FindString(textBox3.Text);
            listBox1.SelectedIndex = sonuc;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int sonuc1;
            sonuc1 = listBox2.FindString(textBox4.Text);
            listBox2.SelectedIndex = sonuc1;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
    }

