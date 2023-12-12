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
    public partial class MiseJourDomaines : Form
    {
        public MiseJourDomaines()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS01;initial catalog=Biblothéque;integrated security=true;");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter da;

        void chargerDGV()
        {
            try { ds.Tables["domaine"].Clear(); } catch (Exception) { }
            da = new SqlDataAdapter("select * from Domaine", cn);
            da.Fill(ds, "domaine");
            dt = ds.Tables["domaine"];

            dataGridView1.DataSource = dt;

        }
        int rechercher()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Domaine where code_domaine="+textBox1.Text+"", cn);
            da1.Fill(ds, "recherche");
            DataTable dt1 = new DataTable();
            dt1 = ds.Tables["recherche"];

            return dt1.Rows.Count;
        }

        int position;
        void defilement(int x)
        {
            textBox1.Text = dt.Rows[x][0].ToString();
            textBox2.Text = dt.Rows[x][1].ToString();
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            chargerDGV();
            position = 0;
            defilement(position);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""|| textBox2.Text == "")
            {
                MessageBox.Show("il faut saisir tous les champs");
            }
            else
            {
                try { ds.Tables["domaine"].Clear(); } catch (Exception) { }
                int existe = rechercher();
                if (rechercher() == 0)
                {
                    DataRow dtr = dt.NewRow();

                    dtr[0] = textBox1.Text;
                    dtr[1] = textBox2.Text;

                    dt.Rows.Add(dtr);

                    SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                    da.Update(ds, "domaine");
                    MessageBox.Show("domaine etes ajouter avec succes");
                    chargerDGV();
                }
                else
                {
                    MessageBox.Show("ce code existe deja ;l'ajoute a echoue");
                    textBox2.Clear();
                }
               
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("il faut saisair le code");
            }
            else {
            try { ds.Tables["domaine1"].Clear(); } catch (Exception) { }
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Domaine where code_domaine=" + textBox1.Text + "", cn);
            da1.Fill(ds, "domaine1");
            DataTable dt1 = new DataTable();
            dt1 = ds.Tables["domaine1"];

            if (dt1.Rows.Count == 1)
            {
                textBox2.Text = dt1.Rows[0][1].ToString();
            }
            else
            {
                MessageBox.Show("ce code n'existe pas ");
                textBox2.Clear();
            }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("il faut saisir le code ");
            }
            else
            {
                int existe = rechercher();
                if (rechercher() >0)
                {
                    for(int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() == textBox1.Text)
                        {
                            
                            dt.Rows[i][1]= textBox2.Text;
                        }
                    }
                    SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                    da.Update(ds, "domaine");
                    MessageBox.Show("le domaine et mdoifier avec succes");
                    chargerDGV();
                    
                    textBox2.Clear();
                }
                else
                {
                    MessageBox.Show("ce code n'existe pas ,la modification est echoue");
                    textBox2.Clear();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("il faut saisir le code pour fait la suppression");
            }
            else
            {
              
                int existe = rechercher();
                if (rechercher() > 0)
                {
                    int y = -1;
                    for (int i=0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() == textBox1.Text)
                        {
                           y=i;
                        }

                    }
                    dt.Rows[y].Delete();
                    SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                    da.Update(ds, "domaine");
                    MessageBox.Show("la suppression a etes effectuter");
                    chargerDGV();
                }
                else
                {
                    MessageBox.Show("ce code n'existe pas ,la suppression a etes echoue");
                    textBox2.Clear();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            position = 0;
            defilement(position);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (position < dt.Rows.Count - 1)
            {
                position++;
                defilement(position);
            }
            else
            {
                MessageBox.Show("vous etes sur le dernier enregistrement");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (position > 0)
            {
                position--;
                defilement(position);
            }
            else
            {
                MessageBox.Show("vous etes sur le premier enregistrement");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            position = dt.Rows.Count-1;
            defilement(position);
          


        }
    }
}
