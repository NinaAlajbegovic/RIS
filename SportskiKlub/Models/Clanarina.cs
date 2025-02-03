using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportskiKlub.Models
{
    public class Clanarina
    {
        [Key]
        public Int64 ClanarinaID { get; set; }

        [ForeignKey("Korisnik")]
        [Required(ErrorMessage = "Član je obavezan!")]
        public Int64 KorisnikID { get; set; }

        [Required(ErrorMessage = "Datum uplate je obavezan!")]
        public DateOnly DatumUplate { get; set; }

        [Required(ErrorMessage = "Iznos je obavezan!")]
        [Range(1, 1000, ErrorMessage = "Iznos mora biti između 1 i 1000!")]
        public float Iznos { get; set; }

        public Korisnik Clan { get; set; }

        public Clanarina() { }
    }
}
