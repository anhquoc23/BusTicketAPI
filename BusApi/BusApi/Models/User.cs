
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BusApi.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Tên Của Bạn Không Được Để Trống"), NotNull, StringLength(maximumLength: 255, ErrorMessage = "Họ Và Tên Đệm Của Bạn Tối Đa Là 255 Ký Tự")]
        public String FirstName { get; set; }
        [Required(ErrorMessage = "Họ Và Tên Đệm Của Bạn Không Được Để Trống"), NotNull, StringLength(maximumLength: 255, ErrorMessage = "Họ Và Tên Đệm Của Bạn Tối Đa Là 255 Ký Tự")]
        public String LastName { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }
        public Boolean IsActive { get; set; }
        public string Avatar { get; set; }
        [NotMapped]
        public string RoleName { get; set; }
        [NotMapped]
        public string RoleId { get; set; }

        public User()
        
        {
            this.CreatedDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.IsActive = true;
        }

        ICollection<Trip> CommentList { get; set; }
    }
}
