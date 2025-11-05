using System.ComponentModel.DataAnnotations;

namespace Euroskills2018.Models
{
    public class SzakmaModel
    {
        [Key]
        public required string id { get; set; }
        public required string szakmaNev { get; set; }

        public ICollection<VersenyzoModel>? Versenyzok { get; set; }
    }
}
