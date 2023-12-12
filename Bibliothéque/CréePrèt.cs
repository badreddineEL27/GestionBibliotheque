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
    public partial class CréePrèt : Form
    {
        public CréePrèt()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS01;initial catalog=Biblothéque;integrated security=true;");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter da;

        void chargercbmembre()
        {
            da = new SqlDataAdapter("select * from Membre", cn);
            da.Fill(ds, "membre");
            dt = ds.Tables["membre"];
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "code_membre";
            comboBox1.ValueMember = "code_membre";
        } 

        void chargercbExemp()
        {
           
            
                SqlDataAdapter da1 = new SqlDataAdapter("select * from Exemplaire  where  disponiblité like 'Oui' ", cn);
                da1.Fill(ds, "exemp1");
                DataTable dt1 = new DataTable();
                dt1 = ds.Tables["exemp1"];
                comboBox2.DataSource = dt1;
                comboBox2.DisplayMember = "code_exemp";
                comboBox2.ValueMember = "code_exemp";
            
          
               
            
        }

        void chargercbetat()
        {
            try { ds.Tables["exemp"].Clear(); } catch (Exception) { }
            SqlDataAdapter da3 = new SqlDataAdapter("select * from Exemplaire ", cn);
            da3.Fill(ds, "exemp");
            DataTable dt3 = new DataTable();
            dt3= ds.Tables["exemp"];
            comboBox3.DataSource = dt3;
            comboBox3.DisplayMember = "Etat";
            comboBox3.ValueMember = "Etat";
        }

       







        private void Form9_Load(object sender, EventArgs e)
        {
            chargercbmembre();
            chargercbExemp();
            chargercbetat();
           


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter damembre = new SqlDataAdapter("select * from Membre where code_membre="+comboBox1.Text+"", cn);
                DataTable dtmembre = new DataTable();
                damembre.Fill(dtmembre);
                
              
                if (dtmembre.Rows.Count ==1)
                {
                    textBox1.Text = dtmembre.Rows[0][1].ToString();
                    textBox2.Text = dtmembre.Rows[0][2].ToString();
                    textBox3.Text = dtmembre.Rows[0][3].ToString();
                    textBox4.Text = dtmembre.Rows[0][5].ToString();
                    textBox5.Text = dtmembre.Rows[0][4].ToString();
                    textBox6.Text = dtmembre.Rows[0][6].ToString();
             

                }
               
                
            }
            catch (Exception)
            {

            }
           


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                    SqlDataAdapter daExempliare = new SqlDataAdapter("select * from Exemplaire where code_exemp="+comboBox2.Text+"", cn);
                    DataTable dtexemp = new DataTable();
                    daExempliare.Fill(dtexemp);
                    if (dtexemp.Rows.Count >0)
                    {
                        dateTimePicker1.Value =DateTime.Parse(dtexemp.Rows[0][1].ToString());
                        textBox7.Text = dtexemp.Rows[0][2].ToString();
                        comboBox3.Text = dtexemp.Rows[0][3].ToString();
                    }
             
            }
            catch (Exception)
            {

            }     
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dapret = new SqlDataAdapter("select * from Pret",cn);
           
            DataTable dt3 = new DataTable();
            DataRow dtr = dt3.NewRow();
            dtr[0] = comboBox1.SelectedValue;
            dtr[1] = comboBox2.SelectedValue;
            dtr[2] = dateTimePicker2.Value.ToString();

            dt3.Rows.Add(dtr);

            SqlCommandBuilder cmdb = new SqlCommandBuilder(dapret);
            dapret.Update(ds,"pret");

            MessageBox.Show("vous etes enregistrer");


        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
