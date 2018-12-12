using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TTTGame.Server.Models
{
    public class GameStats
    {
        public int ID { get; set; }

        [Display(Name = "Wins")]
        public int Wins { get; set; }

        [Display(Name = "Losses")]
        public int Losses { get; set; }

        [Display(Name = "Ties")]
        public int Ties { get; set; }

        [Display(Name = "Game Time Completion")]
        [DataType(DataType.Time)]
        public TimeSpan GameTime { get; set; }
    }
}
