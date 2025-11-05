using Euroskills2018.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Euroskills2018.DTOs
{
    public class VersenyzoDTO
    {
        public int id { get; set; }
        public required string nev { get; set; }
        public required string szakmaId { get; set; }
        public required string orszagId { get; set; }
        public int pont { get; set; }
    }
}
