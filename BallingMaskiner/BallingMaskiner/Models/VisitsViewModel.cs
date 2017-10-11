using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BallingMaskiner.Models
{
    public class VisitsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Kommentar")]
        public string Comment { get; set; }
        [DisplayName("Kontaktperson")]
        public string ContactPerson { get; set; }
        [DisplayName("Dato")]
        public string Date { get; set; }
        [DisplayName("Næste aftale")]
        public string NextAppointment { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
    }
}