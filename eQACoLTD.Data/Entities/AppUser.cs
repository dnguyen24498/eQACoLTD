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
        public DateTime DateCreated { get; set; }

        public Employee Employee { get; set; }
        public Customer Customer { get; set; }
        public List<Cart> Carts { get; set; }
        public List<ProductReview> ProductReviews { get; set; }
        public List<ProductReviewReply> ProductReviewReplies { get; set; }
    }
}
