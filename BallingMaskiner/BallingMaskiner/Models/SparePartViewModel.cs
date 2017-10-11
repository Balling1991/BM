using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BallingMaskiner.Models
{
    public class SparePartViewModel
    {
        public int Id { get; set; }
        [DisplayName("Navn")]
        public string Name { get; set; }
        [DisplayName("Fil")]
        public HttpPostedFileBase File { get; set; }
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string FileName { get; set; }
    }
}