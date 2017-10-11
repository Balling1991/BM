using System.ComponentModel;
using System.Web;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using System.Collections;

namespace BallingMaskiner.Models
{
    public class QuotationViewModel
    {
        public int Id { get; set; }
        [DisplayName("Navn")]
        public string Name { get; set; }
        [DisplayName("Kategori")]
        public string Category { get; set; }
        [DisplayName("Skabelon")]
        public bool IsTemplate { get; set; }
        public HttpPostedFileBase File { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string FileName { get; set; }
    }
}
