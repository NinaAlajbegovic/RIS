using System.ComponentModel.DataAnnotations;

namespace SportskiKlub.Models
{
    public enum VrstaTreninga
    {
        [Display(Name = "Kondicioni")]
        Kondicioni,
        [Display(Name = "Tehnika")]
        Tehnika,
        [Display(Name = "Igra")]
        Igra
    }

}
