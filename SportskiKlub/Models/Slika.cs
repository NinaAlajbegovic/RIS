using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportskiKlub.Models
{
    public class Slika
    {
        [Key]
        public Int64 SlikaID { get; set; }

        [ForeignKey("Galerija")]
        public Int64 GalerijaID { get; set; }

        public String SlikaSadrzaj { get; set; }

        public Galerija Galerija { get; set; }

        public Slika() { }

    }
}
