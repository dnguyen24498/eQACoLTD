using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eQACoLTD.ViewModel.System.Account.Handlers
{
    public class RegisterAccountRequest
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email không được trống")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Định dạng email không chính xác")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
