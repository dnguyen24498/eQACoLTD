using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eQACoLTD.ViewModel.System.Account.Queries
{
    public class LoginAccountDto
    {
        [Required(ErrorMessage = "Tên đăng nhập không được trống")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }

    }
}
