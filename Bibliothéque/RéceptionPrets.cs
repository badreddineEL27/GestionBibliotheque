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
    public partial class RéceptionPrets : Form
    {
        public RéceptionPrets()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS01;initial catalog=Biblothéque;integrated security=true;");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
      
        SqlDataAdapter da;


        void chargercb()
        {
            try { ds.Tables["pret"].Clear(); } catch (Exception) { }
            da = new SqlDataAdapter("select * from Pret ", cn);
            da.Fill(ds, "pret");
            dt = ds.Tables["pret"];
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "Code_exemp";
            comboBox2.ValueMember = "Code_exemp";
        }
        void chargercbMembre()
        {
            try { ds.Tables["pret"].Clear(); } catch (Exception) { }
            SqlDataAdapter damembre = new SqlDataAdapter("select * from Pret ", cn);
            damembre.Fill(ds, "pret");
            DataTable dtmembre = new DataTable();
            dtmembre = ds.Tables["pret"];
            comboBox1.DataSource = dtmembre;
            comboBox1.DisplayMember = "code_membre";
            comboBox1.ValueMember = "code_membre";
        }
        void afficherPret()
        {
            SqlDataAdapter daafficher = new SqlDataAdapter("select * from Pret where Code_exemp=" + comboBox2.Text + "", cn);
            DataTable dtaffi = new DataTable();
            daafficher.Fill(dtaffi);
            
            if (dtaffi.Rows.Count >0)
            {
                comboBox1.Text = dtaffi.Rows[0][1].ToString();
                dateTimePicker4.Text = dtaffi.Rows[0][2].ToString();
                dateTimePicker5.Text = dtaffi.Rows[0][3].ToString();
             
            }
        }


        private void Form10_Load(object sender, EventArgs e)
        {
            chargercb();
            chargercbMembre();
            afficherPret();


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da1 = new SqlDataAdapter("select * from Exemplaire where code_exemp=" + comboBox2.Text + "", cn);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                if (dt1.Rows.Count > 0)
                {
                    dateTimePicker1.Value = DateTime.Parse(dt1.Rows[0][1].ToString());
                    textBox8.Text = dt1.Rows[0][2].ToString();
                    textBox9.Text = dt1.Rows[0][3].ToString();
                   
                }
            }
            catch (Exception)
            {

            }
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da2 = new SqlDataAdapter("select * from Membre where code_membre='" + comboBox1.Text + "'", cn);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    textBox2.Text = dt2.Rows[0][1].ToString();
                    textBox3.Text = dt2.Rows[0][2].ToString();
                    textBox4.Text = dt2.Rows[0][3].ToString();
                    textBox5.Text = dt2.Rows[0][5].ToString();
                    textBox6.Text = dt2.Rows[0][4].ToString();
                    textBox7.Text = dt2.Rows[0][6].ToString();
                    
                }
            }
            catch (Exception)
            {

            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == comboBox1.Text)
                {
                    dt.Rows[i][2] = dateTimePicker4.Value.ToString();
                    dt.Rows[i][3] = dateTimePicker5.Value.ToString();
                }
                
            }
            SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
            da.Update(ds,"pret");
            MessageBox.Show("La modifiaction a étes effectuer avec succes");
            afficherPret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dateTimePicker4.ResetText();
            dateTimePicker5.ResetText();
        }
    }
}
