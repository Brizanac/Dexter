using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.BLL;

namespace WindowsFormsApp1.DAL
{
    class userDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region SELECT data from database 
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                String sql = "SELECT * FROM tbl_korisnici";

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

        #region Insert Data into Database for User Module 
        public bool Insert(userBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng); 


            try
            {
                String sql = "INSERT INTO tbl_korisnici(korisnicko_ime, email, lozinka, ime_i_prezime, kontakt, adresa, datum_dodavanja, slika_korisnika) VALUES(@korisnicko_ime, @email, @lozinka, @ime_i_prezime, @kontakt, @adresa, @datum_dodavanja, @slika_korisnika)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@korisnicko_ime", u.korisnicko_ime);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@lozinka", u.lozinka);
                cmd.Parameters.AddWithValue("@ime_i_prezime", u.ime_i_prezime);
                cmd.Parameters.AddWithValue("@kontakt", u.kontakt);
                cmd.Parameters.AddWithValue("@adresa", u.adresa);
                cmd.Parameters.AddWithValue("@datum_dodavanja", u.datum_dodavanja);
                cmd.Parameters.AddWithValue("@slika_korisnika", u.slika_korisnika);

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
        #region UPDATE data in database (User Module)
        public bool Update (userBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE tbl_korisnici SET korisnicko_ime=@korisnicko_ime, email=@email, lozinka=@lozinka, ime_i_prezime=@ime_i_prezime, kontakt=@kontakt, adresa=@adresa, datum_dodavanja=@datum_dodavanja, slika_korisnika=@slika_korisnika WHERE korisnik_id=@korisnik_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@korisnicko_ime", u.korisnicko_ime);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@lozinka", u.lozinka);
                cmd.Parameters.AddWithValue("@ime_i_prezime", u.ime_i_prezime);
                cmd.Parameters.AddWithValue("@kontakt", u.kontakt);
                cmd.Parameters.AddWithValue("@adresa", u.adresa);
                cmd.Parameters.AddWithValue("@datum_dodavanja", u.datum_dodavanja);
                cmd.Parameters.AddWithValue("@slika_korisnika", u.slika_korisnika);
                cmd.Parameters.AddWithValue("@korisnik_id", u.korisnik_id);

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

        #region Delete Data from Database (User Module)
        public bool Delete(userBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_korisnici WHERE korisnik_id=@korisnik_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@korisnik_id", u.korisnik_id);

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

        #region SEARCH
        public DataTable Search(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                String sql = "SELECT * FROM tbl_korisnici WHERE korisnik_id LIKE '%" + keywords + "%' OR ime_i_prezime LIKE '%" + keywords + "%' OR adresa LIKE '%" + keywords + "%' ";

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
        #region
        public userBLL GetIDfromKorisnickoIme(string korisnickoIme)
        {
            userBLL u = new userBLL();

            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT korisnik_id FROM tbl_korisnici WHERE korisnicko_ime='"+korisnickoIme+"'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                adapter.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    u.korisnik_id = int.Parse(dt.Rows[0]["korisnik_id"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            

            return u;
        }
        #endregion
    }
}
