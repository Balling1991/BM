using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BallingMaskiner.Models
{
    public class Visits
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
    }
}