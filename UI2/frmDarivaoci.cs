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
    public partial class frmDarivaoci : Form
    {
        public frmDarivaoci()
        {
            InitializeComponent();
        }

        donorBLL d = new donorBLL();
        donorDAL dal = new donorDAL();
        userDAL udal = new userDAL();

        string nazivFotografije = "slika-nedostaje.jpg";
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            d.ime_darivaoca = txtFirstName.Text;
            d.prezime_darivaoca = txtLastName.Text;
            d.email = txtEmail.Text;
            d.spol = cmbSpol.Text;
            d.krvna_grupa = cmbKrvnaGrupa.Text;
            d.kontakt = txtContact.Text;
            d.adresa = txtAddress.Text;
            d.dodan_datuma = DateTime.Now;

            string prijavljeniKorisnik = frmLogin.prijavljeniKorisnik;
            userBLL usr = udal.GetIDfromKorisnickoIme(prijavljeniKorisnik);
            d.zaprimaoc = usr.korisnik_id;      //id logiranog korisnika,kasnije će isti bit za update

            d.naziv_fotografije = nazivFotografije;

            bool isSuccess = dal.Insert(d);

            if (isSuccess == true)
            {
                MessageBox.Show("Novi darivaoc uspješno dodan!");

                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;

                Clear();
            }
            else
            {
                MessageBox.Show("Greška pri dodavanju korisnika!");
            }
        }
        public void Clear()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtDonorID.Text = "";
            cmbSpol.Text = "";
            cmbKrvnaGrupa.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";

            string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length) - 10);

            string imagepath = path + "\\fotografije\\slika-nedostaje.jpg";

            pictureBoxProfilePicture.Image = new Bitmap(imagepath);
        }

        private void frmDarivaoci_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvDonors.DataSource = dt;

             string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length) - 10);

            string imagepath = path + "\\fotografije\\slika-nedostaje.jpg";

            pictureBoxProfilePicture.Image = new Bitmap(imagepath);
        }

        private void dgvDonors_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int RowIndex = e.RowIndex;

            txtDonorID.Text = dgvDonors.Rows[RowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvDonors.Rows[RowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvDonors.Rows[RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDonors.Rows[RowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDonors.Rows[RowIndex].Cells[4].Value.ToString();
            cmbSpol.Text = dgvDonors.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dgvDonors.Rows[RowIndex].Cells[6].Value.ToString();
            cmbKrvnaGrupa.Text = dgvDonors.Rows[RowIndex].Cells[7].Value.ToString();

            nazivFotografije = dgvDonors.Rows[RowIndex].Cells[9].Value.ToString();

            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length) - 10);
            string imagePath = paths + "\\fotografije\\" + nazivFotografije;

            pictureBoxProfilePicture.Image = new Bitmap(imagePath);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            d.darivaoc_id = int.Parse(txtDonorID.Text);
            d.ime_darivaoca = txtFirstName.Text;
            d.prezime_darivaoca = txtLastName.Text;
            d.email = txtEmail.Text;
            d.spol = cmbSpol.Text;
            d.krvna_grupa = cmbKrvnaGrupa.Text;
            d.kontakt = txtContact.Text;
            d.adresa = txtAddress.Text;

            string prijavljeniKorisnik = frmLogin.prijavljeniKorisnik;
            userBLL usr = udal.GetIDfromKorisnickoIme(prijavljeniKorisnik);
            d.zaprimaoc = usr.korisnik_id;      

            d.naziv_fotografije = nazivFotografije;

            bool isSuccess = dal.Update(d);

            if (isSuccess == true)
            {
                MessageBox.Show("Darivaoc uspješno ažuriran.");
                Clear();

                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Greška! Korisnik nije ažuriran.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            d.darivaoc_id = int.Parse(txtDonorID.Text);

            bool isSuccess = dal.Delete(d);

            if (isSuccess == true)
            {
                MessageBox.Show("Darivaoc uspješno uklonjen!");

                Clear();

                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Korisnik nije uklonjen!");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog otvori = new OpenFileDialog();
            otvori.Filter = "Image Files Only (*.jpg; *.jpeg; *.png; *.PNG; *.gif| *.jpg; *.jpeg; *.png; *.PNG; *.gif)";

            if (otvori.ShowDialog() == DialogResult.OK)
            {
                if (otvori.CheckFileExists)
                {
                    pictureBoxProfilePicture.Image = new Bitmap(otvori.FileName);

                    string ext = Path.GetExtension(otvori.FileName);

                    string naziv = Path.GetFileNameWithoutExtension(otvori.FileName);

                    Guid g = new Guid();
                    g = Guid.NewGuid();

                    nazivFotografije = "SDKDarivaoci" + naziv + g + ext;

                    string sourcePath = otvori.FileName;

                    string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

                    string destinationPath = paths + "\\fotografije\\" + nazivFotografije;

                    File.Copy(sourcePath, destinationPath);

                    MessageBox.Show("Profilna slika je uspješno dodana");
                }
            }

         }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;

            if (keywords != null)
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
