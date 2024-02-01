using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BusApi.DTO
{
    public class EmployeeDTO
    {
        [AllowNull]
        public string? UserId { get; set; }
        [Required(ErrorMessage = "Tên Của Bạn Không Được Để Trống"), StringLength(maximumLength: 255, ErrorMessage = "Họ Và Tên Đệm Của Bạn Tối Đa Là 255 Ký Tự")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Tên Của Bạn Không Được Để Trống"), StringLength(maximumLength: 255, ErrorMessage = "Họ Và Tên Đệm Của Bạn Tối Đa Là 255 Ký Tự")]
        public string LastName { get; set; }
        [AllowNull]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email không được để trống"), EmailAddress(ErrorMessage = "Email Không đúng định dạng")]
        public string Email { get; set; }

        [Required(ErrorMessage = "SDT không được để trống"), Phone(ErrorMessage = "SDT không đúng định dạng")]
        public string Phone { get; set; }
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Chưa chọn chức vụ phù hợp")]
        public string RoleName { get; set; }
    }
}
