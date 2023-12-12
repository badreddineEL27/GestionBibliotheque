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
    public partial class ModifactionUser : Form
    {
        public static string user;
        public ModifactionUser()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS01;initial catalog=Biblothéque;integrated security=true;");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter da;


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ModifactionUser_Load(object sender, EventArgs e)
        {
            textBox1.Text = Verfication.user.ToString();
            try { ds.Tables["admin"].Clear(); } catch (Exception) { }
            da = new SqlDataAdapter("select * from Administrateur where Login ='" + textBox1.Text + "' ", cn);
            da.Fill(ds, "admin");
            dt = ds.Tables["admin"];
            if (dt.Rows.Count > 0)
            {
                textBox2.Text = dt.Rows[0][1].ToString();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[0][1].ToString() == textBox2.Text)
                {
                    
                    dt.Rows[0][1] = textBox3.Text;

                    
                }
            }
            SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
            da.Update(ds,"admin");
            MessageBox.Show("la modifiaction a ete effectuer avec succes");
            admin a = new admin();
            a.Show();

            
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3.UseSystemPasswordChar = false;

            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
