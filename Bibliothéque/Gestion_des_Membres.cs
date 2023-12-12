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

namespace Bibliothéque
{
    public partial class Gestion_des_Membres : Form
    {
        public Gestion_des_Membres()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS01;initial catalog=Biblothéque;integrated security=true;");
        DataSet ds=new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter da;

        public void chargerDGV()
        {
            da = new SqlDataAdapter("select * from Membre", cn);
            da.Fill(ds, "membre");
            dt = ds.Tables["membre"];
            dataGridView1.DataSource = dt;
        }

         int rechercher()
        {
            try { ds.Tables["recherche"].Clear(); } catch (Exception) { }
            SqlDataAdapter da1 = new SqlDataAdapter("select *from Membre where code_membre="+textBox1.Text+"", cn);
            da1.Fill(ds, "recherche");
            DataTable dt1 = new DataTable();
            dt1 = ds.Tables["recherche"];

            return dt1.Rows.Count;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            chargerDGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""|| textBox2.Text == ""|| textBox3.Text == ""|| textBox4.Text == ""|| textBox5.Text == ""|| textBox6.Text == ""|| textBox7.Text == "")
            {
                MessageBox.Show("il faut saisir tous les champs");
            }
            else
            {
                int existe = rechercher();
                if (rechercher() == 0)
                {
                
                    DataRow dtr = dt.NewRow();
                    dtr[0] = textBox1.Text;
                    dtr[1] = textBox2.Text;
                    dtr[2] = textBox3.Text;
                    dtr[3] = textBox4.Text;
                    dtr[4] = textBox5.Text;
                    dtr[5] = textBox6.Text;
                    dtr[6] = textBox7.Text;

                    dt.Rows.Add(dtr);

                    SqlCommandBuilder cmdb = new SqlCommandBuilder(da);

                    da.Update(ds, "membre");
                    MessageBox.Show("membre ajouter avec succes");
                    
                }
                else
                {
                    MessageBox.Show("ce code existe deja ,l'ajoute a échoué");
                }

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("il faut saisiar le code");
            }
            else {
                try { ds.Tables["membre"].Clear(); } catch (Exception) { }
            SqlDataAdapter da1 = new SqlDataAdapter("select * from membre where code_membre="+textBox1.Text+"", cn);
            da1.Fill(ds, "membre");
            DataTable dt1 = new DataTable();
            dt1 = ds.Tables["membre"];
            if (dt1.Rows.Count == 1)
            {
                textBox2.Text = dt1.Rows[0][1].ToString();
                textBox3.Text = dt1.Rows[0][2].ToString();
                textBox4.Text = dt1.Rows[0][3].ToString();
                textBox5.Text = dt1.Rows[0][4].ToString();
                textBox6.Text = dt1.Rows[0][5].ToString();
                textBox7.Text = dt1.Rows[0][6].ToString();
            }
            else
            {
                MessageBox.Show("ce code n'existe pas ");
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                
            }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int existe = rechercher();
            if (rechercher() > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == textBox1.Text)
                    {
                        dt.Rows[i][0] = textBox1.Text;
                        dt.Rows[i][1] = textBox2.Text;
                        dt.Rows[i][2] = textBox3.Text;
                        dt.Rows[i][3] = textBox4.Text;
                        dt.Rows[i][4] = textBox5.Text;
                        dt.Rows[i][5] = textBox6.Text;
                        dt.Rows[i][6] = textBox7.Text;

                    }
                }
                SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                da.Update(ds, "membre");
                MessageBox.Show("la mpdification avec succes");
               

            }
            else
            {
                MessageBox.Show("ce n'existe pas ,la modification a echoue ");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int existe = rechercher();
            if (rechercher() > 0)
            {
                int y = -1;
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == textBox1.Text)
                    {
                        y = i;
                    }
                }
                dt.Rows[y].Delete();
                SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                da.Update(ds, "membre");
                MessageBox.Show("la suppression a reussi");
            }
            else
            {
                MessageBox.Show("ce code n'existe pas ,la suppression a échoué");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }
    }
}
