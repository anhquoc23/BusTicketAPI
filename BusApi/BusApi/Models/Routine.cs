using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusApi.Models
{
    public class Routine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MinLength(10)]
        public int Distance { set; get; }

        [Required, ForeignKey(nameof(Station))]
        public String StationStartId { set; get; }
        public virtual Station StationStart { set; get; }
        [Required, ForeignKey(nameof(Station))]
        public String StationEndId { set; get; }
        public virtual Station StationEnd { set; get; }
        public bool IsActive { get; set; }


        ICollection<Trip> TripList { get; set; }
    }
}
