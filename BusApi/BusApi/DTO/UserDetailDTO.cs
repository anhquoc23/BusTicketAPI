using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BusApi.DTO
{
    public class UserDetailDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Tên Của Bạn Không Được Để Trống"), NotNull, StringLength(maximumLength: 255, ErrorMessage = "Họ Và Tên Đệm Của Bạn Tối Đa Là 255 Ký Tự")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Họ Và Tên Đệm Của Bạn Không Được Để Trống"), NotNull, StringLength(maximumLength: 255, ErrorMessage = "Họ Và Tên Đệm Của Bạn Tối Đa Là 255 Ký Tự")]
        public string LastName { get; set; }
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email không được để trống"), EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống"), Phone(ErrorMessage = "Số điện thoại không đúng")]
        public string Phone { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Avatar { get; set; }
        public string RoleName { get; set; }
    }
}
