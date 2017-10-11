using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Web.Mvc;

namespace BallingMaskiner.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [DisplayName("Navn")]
        public string Name { get; set; }
        [DisplayName("Addresse")]
        public string Address { get; set; }
        [DisplayName("Postnummer")]
        public string PostalCode { get; set; }
        [DisplayName("By")]
        public string City { get; set; }
        [DisplayName("Telefonnummer")]
        public string PhoneNumber { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Hjemmeside")]
        public string Homepage { get; set; }
        [DisplayName("Kontakter")]
        public virtual List<Contact> Contacts { get; set; }
        [DisplayName("Maskiner")]
        public virtual List<Machine> Machines { get; set; }
        [DisplayName("Tilbud")]
        public virtual List<Quotation> Quotations { get; set; }
        [DisplayName("Besøg")]
        public virtual List<Visits> Visits { get; set; }
        [DisplayName("CVR")]
        public string Cvr { get; set; }
    }
}
