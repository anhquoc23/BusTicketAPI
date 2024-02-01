using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusApi.Models
{
    public class Bus
    {
        [Key]
        public string BusId { get; set; }
        [Required]
        public int BusNumber { get; set; }

        [Required, ForeignKey(nameof(Station))]
        public string StationId { get; set; }
        public virtual Station?  BusStation { get; set; }
        public bool IsActive { get; set; }
        [NotMapped]
        public int? NumberSeat { get; set; }


        ICollection<Chair> SeatsOnBus { get; set; }
        ICollection<Trip> TripBus { get; set;}

        public Bus()
        {
            this.BusId = Guid.NewGuid().ToString();
            this.IsActive = true;
        }
    }
}
