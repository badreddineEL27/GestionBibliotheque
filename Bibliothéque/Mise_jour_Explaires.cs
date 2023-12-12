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
    public partial class Mise_jour_Explaires : Form
    {
        public Mise_jour_Explaires()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS01;initial catalog=Biblothéque;integrated security=true;");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        SqlDataAdapter da;

        void chargerDGV()
        {
            try { ds.Tables["exemp"].Clear(); } catch (Exception) { }
            da = new SqlDataAdapter("select * from Exemplaire", cn);
            da.Fill(ds,"exemp");

            dt = ds.Tables["exemp"];
            dataGridView1.DataSource = dt;

        } 
        void chargerCB()
        {
            try { ds.Tables["exemp"].Clear(); } catch (Exception) { }
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Exemplaire", cn);
            da1.Fill(ds, "exemp");
            DataTable dt1 = new DataTable();
            dt1 = ds.Tables["exemp"];
            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "Etat";
            comboBox1.ValueMember = "Etat";
        }
        int rechercher()
        {
            SqlDataAdapter darechercher = new SqlDataAdapter("select * from Exemplaire where code_exemp="+textBox1.Text+"", cn);
            darechercher.Fill(ds, "exemp2");
            DataTable dtrechercher = new DataTable();
            dtrechercher = ds.Tables["exemp2"];

            return dtrechercher.Rows.Count ;
            
        }
        int position;
        void defilement(int x)
        {
            
            textBox1.Text = dt.Rows[x][0].ToString();
            dateTimePicker1.Text = dt.Rows[x][1].ToString();
            textBox2.Text = dt.Rows[x][2].ToString();
            comboBox1.Text = dt.Rows[x][3].ToString();

            string dispo = "";
            dispo= dt.Rows[x][4].ToString();
            if (dispo == "Oui") { radioButton1.Checked=true; }
            if (dispo == "Non") { radioButton2.Checked = true; ; }
        }









        private void Form8_Load(object sender, EventArgs e)
        {
            chargerDGV();
            chargerCB();
            position = 0;
            defilement(position);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
          
            int existe = rechercher();
            if (rechercher() == 0)
            {
               
            string dispo = "";
            if (radioButton1.Checked){dispo = "Oui"; }
            if (radioButton2.Checked){dispo = "Non"; }

            DataRow dtr = dt.NewRow();
            dtr[0] = textBox1.Text;
            dtr[1] = dateTimePicker1.Value.ToString();
            dtr[2] = textBox2.Text;
            dtr[3] = comboBox1.SelectedValue;
            dtr[4] = dispo.ToString();


                dt.Rows.Add(dtr);
                SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                da.Update(ds, "exemp");
                MessageBox.Show("l'ajoute a étes effectuer avec succes");
                chargerDGV();
                chargerCB();


            }
            else
            {
                MessageBox.Show("ce code existe deja;");
                textBox1.Clear();
                textBox2.Clear();
                comboBox1.Text = null;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
            }
          

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("il faut saisir le code");
            }
            else
            {

                try { ds.Tables["rechercher"].Clear(); } catch (Exception) { }
                SqlDataAdapter da2 = new SqlDataAdapter("select * from Exemplaire where code_exemp ='" + textBox1.Text + "'", cn);
                da2.Fill(ds, "rechercher");
                DataTable dt3 = new DataTable();
                dt3 = ds.Tables["rechercher"];

                if (dt3.Rows.Count>0)
                {
                    dateTimePicker1.Text= dt3.Rows[0][1].ToString();
                    textBox2.Text = dt3.Rows[0][2].ToString();
                    comboBox1.Text = dt3.Rows[0][3].ToString();
                    string dispo = "";
                    dispo = dt3.Rows[0][4].ToString();
                    if (dispo == "Oui") { radioButton1.Checked = true; }
                    if (dispo == "Non") { radioButton2.Checked = true; }

                }
                else
                {
                    MessageBox.Show("ce code n'existe pas");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("il faut saisir le code pour la modification");
            }
            else
            { int existe = rechercher();
                if (rechercher() > 0)
                {
                   
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() == textBox1.Text)
                        {
                            string dispo = "";
                            if (radioButton1.Checked) { dispo = "Oui"; }
                            if (radioButton2.Checked) { dispo = "Non"; }
                            dt.Rows[i][1] = dateTimePicker1.Value;
                            dt.Rows[i][2] = textBox2.Text;
                            dt.Rows[i][3] = comboBox1.SelectedValue;
                            dt.Rows[i][4] = dispo;
                        }

                    }
                    SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                    da.Update(ds, "exemp");
                    MessageBox.Show("la modification a étes effectuer avec succes");
                    chargerDGV();
                }
                else
                {
                    MessageBox.Show("ce code n'existe pas");
                    textBox1.Clear();
                    textBox2.Clear();
                    dateTimePicker1.ResetText();
                    comboBox1.ResetText();
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                }
               
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("il faut saisir le code ");
            }
            else
            {
                int existe = rechercher();
                if (rechercher() > 0) { 
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
            da.Update(ds, "exemp");
            MessageBox.Show("la suppression a étes effectuer avec succes");
            chargerDGV();
                }
                else
                {
                    MessageBox.Show("ce n'existe pas");
                    textBox1.Clear();
                    textBox2.Clear();
                    dateTimePicker1.ResetText();
                    comboBox1.ResetText();
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.ResetText();
            comboBox1.ResetText();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            position = 0;
            defilement(position);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            position = dt.Rows.Count - 1;
            defilement(position);
        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (position < dt.Rows.Count - 1)
            {
                position++;
                defilement(position);
            }
            else
            {
                MessageBox.Show("vous etes sur le dernier");
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
                MessageBox.Show("vous etes sur le premier");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
