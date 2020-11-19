using System.ComponentModel.DataAnnotations;

namespace eQACoLTD.ViewModel.System.Account.Queries
{
    public class TwoStepDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string TwoFactorCode { get; set; }

        public bool RememberLogin { get; set; }
    }
}