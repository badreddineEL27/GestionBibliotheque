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
  
    public partial class admin : Form
    {
        public static string user;
        public admin()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS01;initial catalog=Biblothéque;integrated security=true;");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter da;




        private void admin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""|| textBox2.Text == "")
            {
                MessageBox.Show("Il faut saisir Login");
            }
            else
            {
                try { ds.Tables["admin"].Clear(); } catch (Exception) { }
                da = new SqlDataAdapter("select * from Administrateur where login='" + textBox1.Text + "'and password='" + textBox2.Text + "'", cn);
                da.Fill(ds, "admin");
                dt = ds.Tables["admin"];
                if (dt.Rows.Count != 0)
                {
                    textBox1.Text = dt.Rows[0][0].ToString();
                    textBox2.Text = dt.Rows[0][1].ToString();

                    Menu m = new Menu();
                    m.Show();
                    
                }
                else
                {
                    MessageBox.Show("Mot de pass incoreccte");
                }
            }
            
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            user = textBox1.Text;
            Verfication v = new Verfication();
            v.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
