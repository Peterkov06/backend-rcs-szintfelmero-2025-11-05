using System.ComponentModel.DataAnnotations;

namespace Euroskills2018.Models
{
    public class OrszagModel
    {
        [Key]
        public required string id { get; set; }
        public required string orszagNev { get; set; }

        public ICollection<VersenyzoModel>? Versenyzok { get; set; }
    }
}
