using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BallingMaskiner.Models
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [DisplayName("Navn")]
        public string Name { get; set; }
        [DisplayName("Telefonnummer")]
        public string PhoneNumber { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }

        public int CustomerId { get; set; }
        [DisplayName("Kundenavn")]
        public string CustomerName { get; set; }
    }
}
