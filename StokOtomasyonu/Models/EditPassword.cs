using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StokOtomasyonu.Models
{
    public class EditPassword
    {
        public int id  { get;set; }
        public string sifreOld { get; set; }
        public string sifreNew { get; set; }
        public string sifreNewRepeat { get; set; }

    }
}