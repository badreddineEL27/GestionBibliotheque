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
    public partial class RechercheOuvrage : Form
    {
        public RechercheOuvrage()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS01;initial catalog=Biblothéque;integrated security=true;");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter da;


        void ChargerDGV()
        {
            try { ds.Tables["exemp"].Clear(); } catch (Exception) { }
            da = new SqlDataAdapter("select * from Exemplaire", cn);
            da.Fill(ds, "exemp");
            dt = ds.Tables["exemp"];
            dataGridView1.DataSource = dt;
        }

        void cbcodeOuvrage()
        {
            try { ds.Tables["exemp"].Clear(); } catch (Exception) { }
            SqlDataAdapter da = new SqlDataAdapter("select * from Exemplaire", cn);
            da.Fill(ds, "exemp");
            dt = ds.Tables["exemp"];
            comboBox1.DataSource =dt ;
            comboBox1.DisplayMember = "code_Exemp";
          
        }
        void cbcodelivre()
        {
            try { ds.Tables["exemp"].Clear(); } catch (Exception) { }
            SqlDataAdapter da = new SqlDataAdapter("select * from Exemplaire", cn);
            da.Fill(ds, "exemp");
            dt = ds.Tables["exemp"];
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "code_Livre";


        }
        void cbEtat()
        {
            try { ds.Tables["exemp"].Clear(); } catch (Exception) { }
            SqlDataAdapter da = new SqlDataAdapter("select * from Exemplaire", cn);
            da.Fill(ds, "exemp");
            dt = ds.Tables["exemp"];
            comboBox3.DataSource = dt;
            comboBox3.DisplayMember = "Etat";


        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            ChargerDGV();
            cbcodeOuvrage();
            cbcodelivre();
            cbEtat();

            radioButton1.TabStop = false;
            radioButton2.TabStop = false;
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
          
            string dispon = "";
            if (radioButton1.Checked)
            {
                dispon = "Oui";
                try { ds.Tables["exemp"].Clear(); } catch (Exception) { }
                da = new SqlDataAdapter("select * from Exemplaire where  disponiblité='"+dispon+"'", cn);
                da.Fill(ds, "exemp");
                dt = ds.Tables["exemp"];
                dataGridView1.DataSource = dt;
            
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            string dispon = "";
            if (radioButton2.Checked)
            {
                dispon = "Non";
                try { ds.Tables["exemp"].Clear(); } catch (Exception) { }
                da = new SqlDataAdapter("select * from Exemplaire where  disponiblité='" + dispon + "'", cn);
                da.Fill(ds, "exemp");
                dt = ds.Tables["exemp"];
                dataGridView1.DataSource = dt;

            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            int y = dataGridView1.CurrentRow.Index;


            string dispon = "";
           
          
            textBox1.Text = dt.Rows[y][0].ToString();
            textBox2.Text = dt.Rows[y][1].ToString();
            textBox3.Text = dt.Rows[y][2].ToString();
            textBox4.Text = dt.Rows[y][3].ToString();
            dispon = dt.Rows[y][4].ToString();
            if (dispon == "Oui")
            { radioButton3.Checked = true; }
             if (dispon == "Non")
            { radioButton4.Checked = true; }
           



        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                
                if (dt.Rows[i][0].ToString()== textBox1.Text)
                {
                    string dipon = "";
                    if (radioButton3.Checked)
                    {
                        dipon = "Oui";
                    }
                    if (radioButton4.Checked)
                    {
                        dipon = "Non";
                    }
                    dt.Rows[i][1] = textBox2.Text;
                    dt.Rows[i][2] = textBox3.Text;
                    dt.Rows[i][3] = textBox4.Text;
                   
                    dt.Rows[i][4] = dipon;
                   
                }
                
            }
            SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
            da.Update(ds, "exemp");
            MessageBox.Show("la modification a etes effectues");
            ChargerDGV();
        }

        private void button2_Click(object sender, EventArgs e)
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
            da.Update(ds, "exemp");
            MessageBox.Show("la supression a etes effectues");
            ChargerDGV();

        }
    }
}

