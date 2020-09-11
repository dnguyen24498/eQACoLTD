using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eQACoLTD.ViewModel.System.Account.Handlers
{
    public class TwoStepRequest
    {
        [Required]
        [DataType(DataType.Text)]
        public string TwoFactorCode { get; set; }
        public bool RememberLogin { get; set; }
    }
}
