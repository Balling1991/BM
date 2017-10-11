using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BallingMaskiner.Models
{
    public class Prospect
    {
        public int Id { get; set; }
        [DisplayName("Navn")]
        public string Name { get; set; }
        [DisplayName("Filnavn")]
        [Required]
        public string FileName { get; set; }
        [Required]
        public FileData FileData { get; set; }
    }
}