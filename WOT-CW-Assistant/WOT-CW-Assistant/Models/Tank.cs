using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WOT_CW_Assistant.Models
{
    public class Tank
    {   
        [Key]
        public int tankId { get; set; }
        public string tankNo { get; set; }
        public string tankName { get; set; }
        public string tankTier { get; set; }
        public string tankType{ get; set; }
        public string tankImage { get; set; }
        public string nation { get; set;}
        public bool selected { get; set; }
    }
}