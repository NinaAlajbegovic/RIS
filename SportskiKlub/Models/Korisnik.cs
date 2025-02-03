using System.ComponentModel.DataAnnotations;

namespace SportskiKlub.Models
{
    public class Korisnik
    {
        [Key]
        public Int64 KorisnikID { get; set; }

        public String Ime { get; set; }

        public String Prezime { get; set; }

        public String Email { get; set; }

        public String Lozinka { get; set; }


        [EnumDataType(typeof(VrstaKorisnika))]
        public VrstaKorisnika VrstaKorisnika { get; set; }

        public Korisnik() { }

    }

}
