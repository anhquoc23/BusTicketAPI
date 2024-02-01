
using BusApi.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BusApi.Models
{
    public class Ticket
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }
        [NotNull]
        public DateTime CreatedDate { get; set; }
        [Required, NotNull]
        public DateTime BookDate { get; set; }
        [Required, NotNull]
        public Decimal Price { get; set; }
        [NotNull, StringLength(50)]
        public string Status { get; set; }
        public DateTime ModifiedDate { get; set; }
        [NotNull]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Số Điện Thoại Không Được Để Trống"), MaxLength(12, ErrorMessage = "Số Điện Thoại Tối Đa Là 12 Ký Tự"), NotNull]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email Không Được Để Trống"), NotNull, EmailAddress(ErrorMessage = "Bạn Nhập Không Đúng Định Dạng Email")]
        public string Email { get; set; }

        [Required, ForeignKey(nameof(Chair))]
        public int SeatId { get; set; }
        public virtual Chair? Seat { get; set; }
        [Required, ForeignKey(nameof(Trip))]
        public int TripId { get; set; }
        public virtual Trip? Trip { get; set; }

        public Ticket()
        {
            this.Status = TicketStatus.WAITTING;
            this.CreatedDate = DateTime.Now;
            this.ModifiedDate = DateTime.Now;
        }
    }
}
