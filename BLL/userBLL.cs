using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BLL
{
    class userBLL
    {
        public int korisnik_id { get; set; }
        public string korisnicko_ime { get; set; }
        public string email { get; set; }
        public string lozinka { get; set; }
        public string ime_i_prezime { get; set; }
        public string kontakt { get; set; }
        public string adresa { get; set; }
        public DateTime datum_dodavanja { get; set; }
        public string slika_korisnika { get; set; }
    }
}
