using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class AppUser:IdentityUser<Guid>
    {
        public AppUser(){}
        public AppUser(string userName)
        {
            this.UserName = userName;
        }
        public string Adrress { get; set; }
        public Employee Employee { get; set; }
        public Customer Customer { get; set; }
        public Supplier Supplier { get; set; }
        public List<Cart> Carts { get; set; }
        public List<ProductEvaluation> ProductEvaluations { get; set; }
        public List<ProductEvaluationReply> ProductEvaluationReplies { get; set; }
    }
}
