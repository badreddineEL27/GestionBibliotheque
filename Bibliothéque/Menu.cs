using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bibliothéque
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void gestionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gestionDesMembresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gestion_des_Membres f = new Gestion_des_Membres();
            f.MdiParent = this;
            f.Show();
        }

        private void gestionDesLangueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionLangues f1 = new GestionLangues();
            f1.MdiParent = this;
            f1.Show();
        }

        private void gestionDesDomainesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MiseJourDomaines f2 = new MiseJourDomaines();
            f2.MdiParent = this;
            f2.Show();
        }

        private void gestionDesLivresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MisejourLivre f3 = new MisejourLivre();
            f3.MdiParent = this;
            f3.Show();
        }

        private void gestionDesOuvragesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mise_jour_Explaires f4 = new Mise_jour_Explaires();
            f4.MdiParent = this;
            f4.Show();
        }

        private void gestionDesPretsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CréePrèt f5 = new CréePrèt();
            f5.MdiParent = this;
            f5.Show();
        }

        private void rechercheDesLivresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RechercheLivre f6 = new RechercheLivre();
            f6.MdiParent = this;
            f6.Show();
        }

        private void rechercheDesOuvragesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RechercheOuvrage f7 = new RechercheOuvrage();
            f7.MdiParent = this;
            f7.Show();
        }

        private void rechercheDesMembresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RechercheMembres f9 = new RechercheMembres();
            f9.MdiParent = this;
            f9.Show();
        }

        private void receptionDesPretsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RéceptionPrets f10 = new RéceptionPrets();
            f10.MdiParent = this;
            f10.Show();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
