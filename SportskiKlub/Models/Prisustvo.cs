using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportskiKlub.Models
{
    public class Prisustvo
    {
        [Key]
        public Int64 PrisustvoID { get; set; }

        [ForeignKey("Korisnik")]
        [Required(ErrorMessage = "Član je obavezan!")]
        public Int64 KorisnikID { get; set; }

        [ForeignKey("Trening")]
        [Required(ErrorMessage = "Trening je obavezan!")]
        public Int64 TreningID { get; set; }

        [Required(ErrorMessage = "Prisustvo mora biti označeno!")]
        public Boolean Prisutan { get; set; }

        public Prisustvo() { }
    }
}
