using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalLearning.Services.RequestModel
{
    public class AccountRequest
    {
        public class CreateAccountModel
        {
            [Required(ErrorMessage = "Họ tên không được để trống.")]
            [MaxLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự.")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "Email không được để trống.")]
            [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Mật khẩu không được để trống.")]
            [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự.")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống.")]
            [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
            public string PasswordConfirm { get; set; }

            [Required(ErrorMessage = "Vai trò không được để trống.")]
            [RegularExpression("^(Student|Teacher|Admin)$", ErrorMessage = "Vai trò phải là Student, Teacher hoặc Admin.")]
            public string Role { get; set; }
        }

        public class UpdateAccountModel
        {
            [MaxLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự.")]
            public string? FullName { get; set; }

            [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
            public string? Email { get; set; }

            [MinLength(8)]
            public string? Password { get; set; }

            [RegularExpression("^(Student|Teacher|Admin)$", ErrorMessage = "Vai trò phải là Student, Teacher hoặc Admin.")]
            public string? Role { get; set; }
        }

        public class Login
        {
            [Required, EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }
    }
}
