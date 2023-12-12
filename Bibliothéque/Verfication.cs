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
    public partial class Verfication : Form
    {
        public static string user;
        public Verfication()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS01;initial catalog=Biblothéque;integrated security=true;");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter da;

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void Verfication_Load(object sender, EventArgs e)
        {
            textBox1.Text = admin.user.ToString();


            da = new SqlDataAdapter("select * from Administrateur where Login='" + textBox1.Text + "'", cn);
            da.Fill(ds, "admin");
            dt = ds.Tables["admin"];
            if (dt.Rows.Count != 0)
            {
                textBox3.Text = dt.Rows[0][2].ToString();
                
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { ds.Tables["admin"].Clear(); } catch (Exception) { }
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Administrateur where question='" + textBox3.Text + "' and reponse='" + textBox2.Text + "'", cn);
            da1.Fill(ds, "admin");
            dt = ds.Tables["admin"];
            if (dt.Rows.Count != 0)
            {
                textBox3.Text = dt.Rows[0][2].ToString();
                textBox2.Text = dt.Rows[0][3].ToString();

                user = textBox2.Text;
                user = textBox1.Text;
                ModifactionUser md = new ModifactionUser();
                md.Show();

                

            }
            else
            {
                MessageBox.Show("La reponse est incoreccte");
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
