using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Euroskills2018.Models
{
    public class VersenyzoModel
    {
        [Key]
        public int id { get; set; }
        public required string nev { get; set; }
        [ForeignKey(nameof(Szakma))]
        public required string szakmaId { get; set; }
        [ForeignKey(nameof(Orszag))]
        public required string orszagId { get; set; }
        public int pont { get; set; }

        public SzakmaModel? Szakma { get; set; }
        public OrszagModel? Orszag { get; set; }
    }
}
