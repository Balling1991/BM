using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BallingMaskiner.Models
{
    public class Machine
    {
        public int Id { get; set; }
        [DisplayName("Serienummer")]
        public string SerialNumber { get; set; }
        [DisplayName("Navn")]
        public string Name { get; set; }
        [DisplayName("Reservedele")]
        public virtual List<SparePart> SpareParts { get; set; }
        public virtual List<Service> Services { get; set; }
        [DisplayName("Solgt")]
        public bool IsSold { get; set; }
        [DisplayName("Oprettet")]
        public DateTime? DateCreated { get; set; }
    }
}