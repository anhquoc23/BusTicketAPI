
using BusApi.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BusApi.Models
{
    public class Trip
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TripId { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime TripDate { get; set; }
        [Required, DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [Required]
        public Decimal UnitPrice { get; set; }
        [NotNull, StringLength(50)]
        public string Status { get; set; }
        [Required, ForeignKey(nameof(User))]
        public String DriverId { get; set; }
        public virtual User? Driver { get; set; }
        [Required, ForeignKey(nameof(Bus))]
        public String BusId { get; set; }
        public virtual Bus? BusTrip { get; set; }
        [Required, ForeignKey(nameof(Routine))]
        public int RoutineId { get; set; }
        public virtual Routine? RouteTrip { get; set; }
        public bool IsActive { get; set; }

        public Trip()
        {
            this.Status = TripStatus.WAITTING;
            this.IsActive = true;
        }

        ICollection<Ticket> TicketList { get; set; }
        ICollection<Comment> CommentList { get; set; }
    }
}
