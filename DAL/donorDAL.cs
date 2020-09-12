using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.BLL;

namespace WindowsFormsApp1.DAL
{
    class donorDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        #region SELECT za prikaz podataka u DataGridViewu iz baze podataka
        public DataTable Select()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "SELECT * FROM tbl_darivaoci";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

            return dt;
        }
        #endregion
        #region INSERT podataka u bazu
        public bool Insert(donorBLL d)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "INSERT INTO tbl_darivaoci (ime_darivaoca, prezime_darivaoca, email, kontakt, spol, adresa, krvna_grupa, dodan_datuma, naziv_fotografije, zaprimaoc) VALUES (@ime_darivaoca, @prezime_darivaoca, @email, @kontakt, @spol, @adresa, @krvna_grupa, @dodan_datuma, @naziv_fotografije, @zaprimaoc)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ime_darivaoca", d.ime_darivaoca);
                cmd.Parameters.AddWithValue("@prezime_darivaoca", d.prezime_darivaoca);
                cmd.Parameters.AddWithValue("@email", d.email);
                cmd.Parameters.AddWithValue("@kontakt", d.kontakt);
                cmd.Parameters.AddWithValue("@spol", d.spol);
                cmd.Parameters.AddWithValue("@adresa", d.adresa);
                cmd.Parameters.AddWithValue("@krvna_grupa", d.krvna_grupa);
                cmd.Parameters.AddWithValue("@dodan_datuma", d.dodan_datuma);
                cmd.Parameters.AddWithValue("@naziv_fotografije", d.naziv_fotografije);
                cmd.Parameters.AddWithValue("@zaprimaoc", d.zaprimaoc);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true; 
                }
                else
                {
                    isSuccess = false; 
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
        #region UPDATE donora u bazi podataka
        public bool Update(donorBLL d)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE tbl_darivaoci SET ime_darivaoca=@ime_darivaoca, prezime_darivaoca=@prezime_darivaoca, email=@email, kontakt=@kontakt, spol=@spol, adresa=@adresa, krvna_grupa=@krvna_grupa, naziv_fotografije=@naziv_fotografije, zaprimaoc=@zaprimaoc WHERE darivaoc_id=@darivaoc_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ime_darivaoca", d.ime_darivaoca);
                cmd.Parameters.AddWithValue("@prezime_darivaoca", d.prezime_darivaoca);
                cmd.Parameters.AddWithValue("@email", d.email);
                cmd.Parameters.AddWithValue("@kontakt", d.kontakt);
                cmd.Parameters.AddWithValue("@spol", d.spol);
                cmd.Parameters.AddWithValue("@adresa", d.adresa);
                cmd.Parameters.AddWithValue("@krvna_grupa", d.krvna_grupa);
                cmd.Parameters.AddWithValue("@naziv_fotografije", d.naziv_fotografije);
                cmd.Parameters.AddWithValue("@zaprimaoc", d.zaprimaoc);
                cmd.Parameters.AddWithValue("@darivaoc_id", d.darivaoc_id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true; 
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region DELETE darivaoca iz baze podataka
        public bool Delete(donorBLL d)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_darivaoci WHERE darivaoc_id=@darivaoc_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@darivaoc_id", d.darivaoc_id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
        #region ZBRAJAJ darivaoce po krvnim grupama
        public string zbrajajDarivaoce(string krvna_grupa)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            string donori = "0";

            try
            {
                string sql = "SELECT * FROM tbl_darivaoci WHERE krvna_grupa = '"+krvna_grupa+"'";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                donori = dt.Rows.Count.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return donori;
        }

        #endregion
        #region PRETRAGA darivaoca
        public DataTable Pretraga(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_darivaoci WHERE darivaoc_id LIKE '%"+keywords+"%' OR ime_darivaoca LIKE '%"+keywords+"%' OR prezime_darivaoca LIKE '%"+keywords+"%' OR email LIKE '%"+keywords+"%' OR krvna_grupa LIKE '%"+keywords+"%'";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion
    }
}
