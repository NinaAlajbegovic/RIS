using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportskiKlub.Models
{
    public class Proizvod
    {
        [Key]
        public Int64 ProizvodID { get; set; }


        [ForeignKey("Inventar")]
        public Int64 InventarID { get; set; }

        public Inventar Inventar { get; set; }

        public Proizvod() { }
    }
}
