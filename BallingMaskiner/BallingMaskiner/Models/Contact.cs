using System.ComponentModel;

namespace BallingMaskiner.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [DisplayName("Navn")]
        public string Name { get; set; }
        [DisplayName("Telefonnummer")]
        public string PhoneNumber { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
