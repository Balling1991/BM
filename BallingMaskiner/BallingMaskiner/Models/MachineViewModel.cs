using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallingMaskiner.Models
{
    public class MachineViewModel
    {
        public int Id { get; set; }
        [DisplayName("Serienummer")]
        public string SerialNumber { get; set; }
        [DisplayName("Navn")]
        public string Name { get; set; }
        [DisplayName("Reservedele")]
        public List<SparePart> SpareParts { get; set; }
        [DisplayName("Service dokumenter")]
        public List<Service> Services { get; set; }
        [DisplayName("Solgt")]
        public bool IsSold { get; set; }
        [DisplayName("Oprettet")]
        public DateTime? DateCreated { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
    }
}
