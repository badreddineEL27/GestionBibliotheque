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
    public partial class RechercheLivre : Form
    {
        public RechercheLivre()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS01;initial catalog=Biblothéque;integrated security=true;");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter da;

       
        void chargerDGV()
        {
            try { ds.Tables["livre"].Clear(); } catch (Exception) { }
            da = new SqlDataAdapter("select * from Livre", cn);
            da.Fill(ds, "livre");
            dt = ds.Tables["livre"];
            dataGridView1.DataSource = dt;
        }
        void ChargercbCode()
        {
            try { ds.Tables["livre"].Clear(); } catch (Exception) { }
            da = new SqlDataAdapter("select * from Livre", cn);
            da.Fill(ds, "livre");
            dt = ds.Tables["livre"];
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "code_livre";

        }
        void chargercbdomaine()
        {
            try { ds.Tables["domaine"].Clear(); } catch (Exception) { }
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Domaine", cn);
            da1.Fill(ds, "domaine");
            DataTable dt1 = new DataTable();
            dt1 = ds.Tables["domaine"];
            comboBox2.DataSource = dt1;
            comboBox2.DisplayMember = "Intitulé_domaine";
           
        }
        void chargercblangue()
        {
            try { ds.Tables["langue"].Clear(); } catch (Exception) { }
            SqlDataAdapter da2 = new SqlDataAdapter("select * from Langue", cn);
            da2.Fill(ds, "langue");
            DataTable dt2 = new DataTable();
            dt2 = ds.Tables["langue"];
            comboBox3.DataSource = dt2;
            comboBox3.DisplayMember = "Intitulé_Langue";

        }
        void ChargercbEditeur()
        {
            try { ds.Tables["livre"].Clear(); } catch (Exception) { }
            da = new SqlDataAdapter("select * from Livre", cn);
            da.Fill(ds, "livre");
            dt = ds.Tables["livre"];
            comboBox4.DataSource = dt;
            comboBox4.DisplayMember = "Editeur";

        }


        private void label9_Click(object sender, EventArgs e)
        {
            
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            chargerDGV();
            ChargercbCode();
            chargercbdomaine();
            chargercblangue();
            ChargercbEditeur();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try { ds.Tables["livre1"].Clear(); } catch (Exception) { }
            int nbr = textBox1.Text.Length;
            SqlDataAdapter daTitre = new SqlDataAdapter("select * from Livre where left(Titre,"+nbr+")like '"+textBox1.Text+"'", cn);
            daTitre.Fill(ds, "livre1");
            DataTable dtTitre = new DataTable();
            dtTitre = ds.Tables["livre1"];
            dataGridView1.DataSource = dtTitre;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int y = dataGridView1.CurrentRow.Index;
            textBox2.Text = dt.Rows[y][0].ToString();
            textBox3.Text = dt.Rows[y][1].ToString();
            textBox4.Text = dt.Rows[y][2].ToString();
            textBox5.Text = dt.Rows[y][3].ToString();
            textBox6.Text = dt.Rows[y][4].ToString();
            textBox7.Text = dt.Rows[y][5].ToString();
            textBox8.Text = dt.Rows[y][6].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == textBox2.Text)
                {
                    dt.Rows[i][1] = textBox3.Text;
                    dt.Rows[i][2] = textBox4.Text;
                    dt.Rows[i][3] = textBox5.Text;
                    dt.Rows[i][4] = textBox6.Text;
                    dt.Rows[i][5] = textBox7.Text;
                    dt.Rows[i][6] = textBox8.Text;
                   
                }
                
            }
            SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
            da.Update(ds, "livre");
            MessageBox.Show("la modification a réussi");
            chargerDGV();


        }
    }
}
