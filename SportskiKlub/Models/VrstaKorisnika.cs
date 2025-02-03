using System.ComponentModel.DataAnnotations;

namespace SportskiKlub.Models
{
    public enum VrstaKorisnika
    {
        [Display(Name = "Administrator")]
        Administrator,
        [Display(Name = "Trener")]
        Trener,
        [Display(Name = "Clan")]
        Clan
    }
}
