using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.BLL;
using WindowsFormsApp1.DAL;

namespace WindowsFormsApp1.UI2
{
    public partial class frmKorisnici : Form
    {
        public frmKorisnici()
        {
            InitializeComponent();
        }

        userBLL u = new userBLL();
        userDAL dal = new userDAL();

        string slikaKorisnika = "slika-nedostaje.jpg";
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String keywords = txtSearch.Text;

            if (keywords != null)
            {
                DataTable dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;
            }
            else
            {
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            u.ime_i_prezime = txtFullName.Text;
            u.email = txtEmail.Text;
            u.korisnicko_ime = txtUsername.Text;
            u.lozinka = txtPassword.Text;
            u.kontakt = txtContact.Text;
            u.adresa = txtAddress.Text;
            u.datum_dodavanja = DateTime.Now;
            u.slika_korisnika = slikaKorisnika;

            bool success = dal.Insert(u);

            if(success == true)
            {
                MessageBox.Show("Novi korisnik uspješno dodan. ");

                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

                Clear();
            }
            else
            {
                MessageBox.Show("Bezuspješno dodavanje korisnika");
            }
        }

        public void Clear()
        {
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtUserID.Text = "";
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            string imagePath = paths + "\\fotografije\\slika-nedostaje.jpg";

            pictureBoxProfilePicture.Image = new Bitmap(imagePath);
        }
       
        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int RowIndex = e.RowIndex;
            txtUserID.Text = dgvUsers.Rows[RowIndex].Cells[0].Value.ToString();
            txtUsername.Text = dgvUsers.Rows[RowIndex].Cells[1].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[RowIndex].Cells[2].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[RowIndex].Cells[3].Value.ToString();
            txtFullName.Text = dgvUsers.Rows[RowIndex].Cells[4].Value.ToString();
            txtContact.Text = dgvUsers.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[RowIndex].Cells[6].Value.ToString();
            slikaKorisnika = dgvUsers.Rows[RowIndex].Cells[8].Value.ToString();
           
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            
            if (slikaKorisnika != "slika-nedostaje.jpg")
            {
                string imagePath = paths + "\\fotografije\\" + slikaKorisnika;

                pictureBoxProfilePicture.Image = new Bitmap(imagePath);
            }
            else
            {
                string imagePath = paths + "\\fotografije\\slika-nedostaje.jpg"; 

                pictureBoxProfilePicture.Image = new Bitmap(imagePath);
            }
        }
        //dovdje 
        private void frmKorisnici_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            u.korisnik_id = int.Parse(txtUserID.Text);
            u.ime_i_prezime = txtFullName.Text;
            u.email = txtEmail.Text;
            u.korisnicko_ime = txtUsername.Text;
            u.lozinka = txtPassword.Text;
            u.kontakt = txtContact.Text;
            u.adresa = txtAddress.Text;
            u.datum_dodavanja = DateTime.Now;
            u.slika_korisnika = slikaKorisnika;

            bool success = dal.Update(u);

            if (success == true)
            {
                MessageBox.Show("Korisnik je uspješno ažuriran.");

                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

                Clear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            u.korisnik_id = int.Parse(txtUserID.Text);

            bool success = dal.Delete(u);

            if (success == true)
            {
                MessageBox.Show("Korisnik uspješno uklonjen");

                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

                Clear();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.PNG; *.gif;)|*.jpg; *.jpeg; *.png; *.PNG; *.gif;";

            if (open.ShowDialog() == DialogResult.OK)
            {
                if (open.CheckFileExists)
                {
                    pictureBoxProfilePicture.Image = new Bitmap(open.FileName);

                    string ext = Path.GetExtension(open.FileName);

                    Random random = new Random();
                    int RandInt = random.Next(0, 1000);

                    slikaKorisnika = "SustavDoniranjaKrvi" + RandInt + ext;

                    string sourcePath = open.FileName;

                    string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

                    string destinationPath = paths + "\\fotografije\\" + slikaKorisnika;

                    File.Copy(sourcePath, destinationPath);

                    MessageBox.Show("Profilna slika je uspješno dodana");
                }
            }
        }
    }
}
