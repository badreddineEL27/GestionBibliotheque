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
    public partial class MisejourLivre : Form
    {
        public MisejourLivre()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS01;initial catalog=Biblothéque;integrated security=true;");

        DataSet ds = new DataSet();
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        
        void ChargerDGV()
        {
            try { ds.Tables["livre"].Clear(); }catch (Exception) { }
            da = new SqlDataAdapter("select * from Livre", cn);
            da.Fill(ds, "livre");
            dt = ds.Tables["livre"];
            dataGridView1.DataSource = dt;
        }
        void cblangue()
        {
            try { ds.Tables["langue"].Clear(); } catch (Exception) { }
            SqlDataAdapter daLangue = new SqlDataAdapter("select * from Langue", cn);
            daLangue.Fill(ds, "langue");
            DataTable dtLangue = new DataTable();
            dtLangue = ds.Tables["langue"];
            comboBox1.DataSource = dtLangue;
            comboBox1.DisplayMember = "intitulé_langue";
            comboBox1.ValueMember = "code_langue";
        }
        void cbdomaine()
        {
            try { ds.Tables["domaine"].Clear(); } catch (Exception) { }
            SqlDataAdapter dadomaine = new SqlDataAdapter("select* from Domaine", cn);
            dadomaine.Fill(ds, "domaine");
            DataTable dtdomaine = new DataTable();
            dtdomaine = ds.Tables["domaine"];
            comboBox2.DataSource = dtdomaine;
            comboBox2.DisplayMember = "intitulé_domaine";
            comboBox2.ValueMember = "code_domaine";
        }
        int recherecher()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Livre where code_livre="+textBox1.Text+"",cn);
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
            textBox3.Text = dt.Rows[x][2].ToString();
            textBox4.Text = dt.Rows[x][3].ToString();
            textBox5.Text = dt.Rows[x][4].ToString();
            comboBox1.SelectedValue = dt.Rows[x][5].ToString();
            comboBox2.SelectedValue = dt.Rows[x][6].ToString();
        }


       

        private void button6_Click(object sender, EventArgs e)
        {
            
            if (position < dt.Rows.Count-1)
            {
                position++;
                defilement(position);
            }
            else
            {
                MessageBox.Show("vous etes sur le dernier");
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            ChargerDGV();
            cblangue();
            cbdomaine();
            position = 0;
            defilement(position);




        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""|| textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("il faut saisir tous les champs");
            }
            else
            {
                int existe = recherecher();
                if (recherecher() == 0)
                {
                    DataRow dtr = dt.NewRow();
                    dtr[0] = textBox1.Text;
                    dtr[1] = textBox2.Text;
                    dtr[2] = textBox3.Text;
                    dtr[3] = textBox4.Text;
                    dtr[4] = textBox5.Text;
                    dtr[5] = comboBox1.SelectedValue;
                    dtr[6] =  comboBox2.SelectedValue;

                    dt.Rows.Add(dtr);
                    SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                    da.Update(ds,"livre");
                    MessageBox.Show("le livre est ajoutée avec succée");
                    ChargerDGV();
                    cbdomaine();
                    cblangue();

                }
                else
                {
                    MessageBox.Show("ce code existe deja ,l'ajoute a echoue");
                }
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
                try { ds.Tables["livre1"].Clear(); } catch (Exception) { }
                SqlDataAdapter da1 = new SqlDataAdapter("select * from Livre where code_livre=" + textBox1.Text + "", cn);
                da1.Fill(ds, "livre1");
                DataTable dt1 = new DataTable();
                dt1 = ds.Tables["livre1"];
                if (dt1.Rows.Count == 1)
                {
                    
                    textBox2.Text = dt1.Rows[0][1].ToString();
                    textBox3.Text = dt1.Rows[0][2].ToString();
                    textBox4.Text = dt1.Rows[0][3].ToString();
                    textBox5.Text = dt1.Rows[0][4].ToString();
                    comboBox1.SelectedValue = dt1.Rows[0][5].ToString();
                    comboBox2.SelectedValue = dt1.Rows[0][6].ToString();
                }
                else
                {
                    MessageBox.Show("ce code n'existe pas");
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    comboBox1.ResetText();
                    comboBox2.ResetText();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("il faut saisir le code");
            }
            else
            {
                int existe = recherecher();
                if (recherecher() > 0) { 
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == textBox1.Text)
                    {
                        dt.Rows[i][1]= textBox2.Text;
                        dt.Rows[i][2] = textBox3.Text;
                        dt.Rows[i][3] = textBox4.Text;
                        dt.Rows[i][4] = textBox5.Text;
                        dt.Rows[i][5] = comboBox1.SelectedValue;
                        dt.Rows[i][6] = comboBox2.SelectedValue;

                        SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                        da.Update(ds, "livre");
                        MessageBox.Show("le livre etes modifier avec succes");
                        ChargerDGV();
                       

                    }
                }
                }
                else
                {
                    MessageBox.Show("ce code n'existe pas ,la modification etes echoué");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("il faut saisir le code");
            }
            else
            {
                int y = -1;
                int existe = recherecher();

                if (recherecher() > 0) { 

                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == textBox1.Text)
                    {
                            y = i;
                    }
                       
                }
                    dt.Rows[y].Delete();

                    SqlCommandBuilder cmd = new SqlCommandBuilder(da);
                    da.Update(ds, "livre");
                    MessageBox.Show("le livre est supprimer avec succes");
                    ChargerDGV();
                }
                else
                {
                    MessageBox.Show("ce code n'existe pas,la suppression a echoué ");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            position = 0;
            defilement(position);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            position = dt.Rows.Count - 1;
            defilement(position);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.ResetText();
            comboBox2.ResetText();
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
    }
}
