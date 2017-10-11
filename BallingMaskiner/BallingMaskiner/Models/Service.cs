using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BallingMaskiner.Models
{
    public class Service
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