using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TTTGame.Shared
{
    class GameStats
    {
        public int ID { get; set; }

        [Display(Name = "Wins")]
        public int Wins { get; set; }

        [Display(Name = "Losses")]
        public int Losses { get; set; }

        [Display(Name = "Ties")]
        public int Ties { get; set; }

        [Display(Name = "AVG Game Time")]
        [DataType(DataType.Time)]
        public TimeSpan AvgerageGameTime { get; set; }
    }
}
