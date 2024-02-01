using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusApi.Models
{
    public class Chair
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ChairNumber { get; set; }

        [Required, ForeignKey(nameof(Bus))]
        public String BusId { get; set; }
        public virtual Bus? ChairOnBus { get; set; }
        public bool IsActive { get; set; }


        ICollection<Ticket> TicketList { get; set; }

        public Chair()
        {
            IsActive = true;
        }
    }
}
