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

namespace WindowsFormsApp1.UI2
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();

       public static string prijavljeniKorisnik;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            l.korisnickoIme = txtkorisnickoIme.Text;
            l.lozinka = txtLozinka.Text;

            bool isSuccess = dal.loginProvjera(l);

            if(isSuccess == true)
            {
                MessageBox.Show("Prijava uspješna!");

                prijavljeniKorisnik = l.korisnickoIme;

                frmHome home = new frmHome();
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Prijava nije uspjela, pokušajte ponovno.");
            }
        }
    }
}
