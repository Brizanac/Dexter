using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.BLL;
using WindowsFormsApp1.DAL;
using WindowsFormsApp1.UI2;

namespace WindowsFormsApp1
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        donorDAL dal = new donorDAL();

        private void frmHome_Load(object sender, EventArgs e)
        {
            // home
            zbrojiSveDarivaoce();

            //prikaz na home stranici
            DataTable dt = dal.Select();
            dgvDonors.DataSource = dt;

            lblUser.Text = frmLogin.prijavljeniKorisnik;
        }

        private void lbl0PositiveCount_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void korisniciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKorisnici korisnici = new frmKorisnici();
            korisnici.Show();
        }

        private void donoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDarivaoci darivaoci = new frmDarivaoci();
            darivaoci.Show();
        }

        public void zbrojiSveDarivaoce()
        {
            lbl0PositiveCount.Text = dal.zbrajajDarivaoce("O+");
            lbl0NegativeCount.Text = dal.zbrajajDarivaoce("O-");
            lblApositiveCount.Text = dal.zbrajajDarivaoce("A+");
            lblAnegativeCount.Text = dal.zbrajajDarivaoce("A-");
            lblBpositiveCount.Text = dal.zbrajajDarivaoce("B+");
            lblBnegativeCount.Text = dal.zbrajajDarivaoce("B-");
            lblABpositiveCount.Text = dal.zbrajajDarivaoce("AB+");
            lblABnegativeCount.Text = dal.zbrajajDarivaoce("AB-");
        }

        private void frmHome_Activated(object sender, EventArgs e)
        {
            zbrojiSveDarivaoce();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;

            if(keywords!= null)
            {
                DataTable dt = dal.Pretraga(keywords);
                dgvDonors.DataSource = dt;
            }
            else
            {
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
        }
    }
}
