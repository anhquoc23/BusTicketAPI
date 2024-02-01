using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusApi.Models
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        [Required, StringLength(maximumLength: 255)]
        public string Content { get; set; }

        [Required, ForeignKey(name: "User")]
        public String CustomerId { get; set; }
        public virtual User? Customer { get; set; }
        [Required, ForeignKey(nameof(Trip))]
        public int TripId { get; set; }
        public virtual Trip? TripComment { get; set; }
        public DateTime CreatedDate { get; set; }

        public Comment() 
        {
            this.CreatedDate = DateTime.Now;
        }
    }
}
