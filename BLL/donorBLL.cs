using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BLL
{
    class donorBLL
    {
        public int darivaoc_id { get; set; }
        public string  ime_darivaoca { get; set; }
        public string prezime_darivaoca { get; set; }
        public string email { get; set; }
        public string kontakt { get; set; }
        public string spol { get; set; }
        public string adresa { get; set; }
        public string krvna_grupa { get; set; }
        public DateTime dodan_datuma { get; set; }
        public string naziv_fotografije { get; set; }
        public int zaprimaoc { get; set; }
    }
}
