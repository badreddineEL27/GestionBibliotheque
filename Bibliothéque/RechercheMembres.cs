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
    public partial class RechercheMembres : Form
    {
        public RechercheMembres()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS01;initial catalog=Biblothéque;integrated security=true;");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter da;

        void ChargerDGV()
        {
            try { ds.Tables["membre"].Clear(); } catch (Exception) { }
            da = new SqlDataAdapter("select * from Membre", cn);
            da.Fill(ds, "membre");
            dt = ds.Tables["membre"];
            dataGridView1.DataSource = dt;
        }
        void cbcodeMembre()
        {
            try { ds.Tables["membre"].Clear(); } catch (Exception) { }
            SqlDataAdapter da = new SqlDataAdapter("select * from Membre", cn);
            da.Fill(ds, "membre");
            dt = ds.Tables["membre"];
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "code_membre";


        }

        private void Form7_Load(object sender, EventArgs e)
        {
            ChargerDGV();
            cbcodeMembre();


            radioButton1.TabStop = false;
            radioButton2.TabStop = false;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (radioButton1.Checked)
            {
                try { ds.Tables["membre"].Clear(); } catch (Exception) { }
                int nbr1 = textBox1.Text.Length;
                SqlDataAdapter code = new SqlDataAdapter("select * from Membre where left(code_membre," + nbr1 + ")= '" + textBox1.Text + "'", cn);
                code.Fill(ds, "membre");
                DataTable dtcode = new DataTable();
                dtcode = ds.Tables["membre"];
                dataGridView1.DataSource = dtcode;
            }
            else if (radioButton2.Checked)
            {
                try { ds.Tables["membre"].Clear(); } catch (Exception) { }
                int nbr = textBox1.Text.Length;
                SqlDataAdapter danom = new SqlDataAdapter("select * from Membre where left(nom," + nbr + ")like '" + textBox1.Text + "'", cn);
                danom.Fill(ds, "membre");
                DataTable dtnom = new DataTable();
                dtnom = ds.Tables["membre"];
                dataGridView1.DataSource = dtnom;
            }








            
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            int y = dataGridView1.CurrentRow.Index;

            textBox2.Text = dt.Rows[y][1].ToString();
            textBox3.Text = dt.Rows[y][2].ToString();
            textBox4.Text = dt.Rows[y][3].ToString();
            textBox5.Text = dt.Rows[y][5].ToString();
            textBox6.Text = dt.Rows[y][4].ToString();
            textBox7.Text = dt.Rows[y][6].ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == textBox1.Text)
                {
                    dt.Rows[i][1] = textBox2.Text;
                    dt.Rows[i][2] = textBox3.Text;
                    dt.Rows[i][3] = textBox4.Text;
                    dt.Rows[i][5] = textBox5.Text;
                    dt.Rows[i][4] = textBox6.Text;
                    dt.Rows[i][6] = textBox7.Text;

                }

            }
            SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
            da.Update(ds, "membre");
            MessageBox.Show("La modification a etes effectue");
            ChargerDGV();

          

            }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int y = -1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == textBox1.Text)
                {
                    y = i;
                }

            }
            dt.Rows[y].Delete();
            SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
            da.Update(ds, "membre");
            MessageBox.Show("la supression a etes effectues");
            ChargerDGV();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

       
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
