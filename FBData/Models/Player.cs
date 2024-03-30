using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBData.Models
{
    public class Player

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }
        public required string PlayerName { get; set; }
        public required Position Position { get; set; }
        public int JerseyNumber { get; set; }
        public int GoalScored { get; set; }
    }
    public enum Position
    {
        GoalKeeper,
        Defender,
        Midfielder,
        Forward
    }
}
