using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BallingMaskiner.Models
{
    public class Quotation
    {
        public int Id { get; set; }
        [DisplayName("Navn")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Filnavn")]
        public string FileName { get; set; }
        [Required]
        public virtual FileData FileData { get; set; }
        [DisplayName("Kategori")]
        public string Category { get; set; }
        [DisplayName("Skabelon")]
        public bool IsTemplate { get; set; }
    }
}
