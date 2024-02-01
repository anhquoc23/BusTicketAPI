using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BusApi.DTO
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Tên Của Bạn Không Được Để Trống"), StringLength(maximumLength: 255, ErrorMessage = "Họ Và Tên Đệm Của Bạn Tối Đa Là 255 Ký Tự")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Tên Của Bạn Không Được Để Trống"), StringLength(maximumLength: 255, ErrorMessage = "Họ Và Tên Đệm Của Bạn Tối Đa Là 255 Ký Tự")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username Của Bạn Không Được Để Trống")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu Của Bạn Không Được Để Trống"), MinLength(6, ErrorMessage = "Mật khẩu Của Bạn Tối thiểu Là 6 Ký Tự")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email không được để trống"), EmailAddress(ErrorMessage = "Email Không đúng định dạng")]
        public string Email { get; set; }

        [Required(ErrorMessage = "SDT không được để trống"), Phone(ErrorMessage = "SDT không đúng định dạng")]
        public string Phone { get; set; }
        public IFormFile Image { get; set; }
    }
}
