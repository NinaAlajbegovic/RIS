using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportskiKlub.Models
{
    public class Ocjena
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImeTrenera { get; set; }

        [Required(ErrorMessage = "Ocjena mora biti u rasponu od 1 do 5.!")]
        [Range(1, 6, ErrorMessage = "Ocjena mora biti u rasponu od 1 do 5.")]
        public Int64 OcjenaTrenera { get; set; }

        [ForeignKey("Korisnik")]
        public Int64 KorisnikID { get; set; }
    }
}
