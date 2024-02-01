using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusApi.Models
{
    public class Station
    {
        [Key]
        public String StationId { get; set; }
        [Required, MinLength(10, ErrorMessage ="Tên Trạm tối thiểu phải chứa 10 ký tự")]
        public string StationName { get; set;}
        [Required, MinLength(1)]
        public string StationAddress { get; set; }
        [Required]
        public string StationProvince { get; set; }
        [DefaultValue(true)]
        public Boolean IsActive { get; set; }


        ICollection<Bus> BusesOnStation { get; set; }
        ICollection<Routine> RoutesOnStationStart { get; set;}
        ICollection<Routine> RoutesOnStationEnd { get; set; }

        public Station() {
            this.StationId = Guid.NewGuid().ToString();
            this.IsActive = true;
        }
    }
}
