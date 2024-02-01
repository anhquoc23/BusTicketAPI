using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusApi.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Tên Người dùng không để trống")]
        [DisplayName("Nhập Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật Khẩu Không được để trống")]
        [DisplayName("Nhập Password")]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
