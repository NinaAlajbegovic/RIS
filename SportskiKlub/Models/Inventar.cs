using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportskiKlub.Models
{
    public class Inventar
    {
        [Key]
        public Int64 InventarID { get; set; }

        [Required(ErrorMessage = "Naziv je obavezan!")]
        public String Naziv { get; set; }

        [Required(ErrorMessage = "Količina je obavezna!")]
        [Range(1, Int64.MaxValue, ErrorMessage = "Količina mora biti veća od 0!")]
        public Int64 Kolicina { get; set; }

        [ForeignKey("Korisnik")]
        public Int64 KorisnikID { get; set; }

        public Inventar() { }
    }
}
