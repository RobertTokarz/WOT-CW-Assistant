using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOT_CW_Assistant.Models
{
    public class Player
    {
        public int id { get; set; }
        public string playerNo { get; set; }
        public string playerNickName { get; set; }
        public string clanId { get; set; }
        public int battles { get; set; }
        public int avgExperience { get; set; }
        public int personalRating { get; set; }
        public int hitPercent { get; set; }
        public string role { get; set; }
    }
}
