using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public bool EnableLocalLogin { get; set; } = true;

        public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
        public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

        public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;
        public string ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;

    }
}
