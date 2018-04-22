using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOT_CW_Assistant.Models
{
    public class TankStatistics
    {
        public int id { get; set; }
        public string tankId { get; set;}
        public string playerNo {get; set;}
        public int battlesCount { get; set; }
        public int damageDealt { get; set; }
        public int avgExperiencePerBattle { get; set; }
        public DateTime lastUpdate { get; set; }
        public int avgDamagePerBattle { get; set;}
        public int spotted { get; set; }
        public int avgDamageBlocked { get; set; }
        public int winningPercent { get; set; }
        public double spotPerBattle { get; set; }
        public string playerNickName { get; set; }
        public string tank { get; set; }
    }
}