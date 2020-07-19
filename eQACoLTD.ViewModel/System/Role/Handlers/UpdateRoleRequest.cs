using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eQACoLTD.ViewModel.System.Role.Handlers
{
    public class UpdateRoleRequest
    {
        [Required(ErrorMessage="Không được bỏ trống trường này")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
