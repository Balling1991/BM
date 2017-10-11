using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BallingMaskiner.Models
{
    public class ManualViewModel
    {
        public int Id { get; set; }
        [DisplayName("Navn")]
        public string Name { get; set; }
        [DisplayName("Fil")]
        public string FileName { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}