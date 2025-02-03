using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SportskiKlub.Models
{
    public class Galerija
    {
        [Key]
        public Int64 GalerijaID { get; set; }
        public String naslov { get; set; }

        [NotMapped]
        public IFormFile DatotekaSlike { get; set; }

        [ForeignKey("Korisnik")]
        public Int64 KorisnikID { get; set; }



        public Galerija() { }
    }
}
