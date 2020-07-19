using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eQACoLTD.ViewModel.System.User.Handlers
{
    public class ChangeUserPasswordRequest
    {

        [Required(ErrorMessage ="Mật khẩu không được để trống")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string NewPassword { get; set; }
        [Compare("NewPassword",ErrorMessage ="Mật khẩu không khớp")]
        public string ConfirmNewPassword { get; set; }

    }
}
