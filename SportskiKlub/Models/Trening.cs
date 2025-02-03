using System.ComponentModel.DataAnnotations;

namespace SportskiKlub.Models
{
    public class Trening
    {
        [Key]
        public Int64 TreningID { get; set; }

        [Required(ErrorMessage = "Datum treninga je obavezan!")]
        public DateOnly Datum { get; set; }

        [Required(ErrorMessage = "Vrijeme treninga je obavezno!")]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Vrijeme mora biti u formatu HH:mm!")]
        public String Vrijeme { get; set; }

        [Required(ErrorMessage = "Grupa je obavezna!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Naziv grupe mora biti između 2 i 50 karaktera!")]
        public String Grupa { get; set; }

        [Required(ErrorMessage = "Vrsta treninga je obavezna!")]

        [EnumDataType(typeof(VrstaTreninga), ErrorMessage = "Neispravna vrsta treninga!")]
        public VrstaTreninga Vrsta { get; set; }

        public Trening() { }
    }
}
