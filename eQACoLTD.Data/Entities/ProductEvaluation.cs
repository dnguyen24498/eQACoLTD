using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class ProductReview
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public Guid? UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int StarScore { get; set; }

        public Product Product { get; set; }
        public AppUser AppUser { get; set; }

        public List<ProductReviewReply> ProductReviewReplies { get; set; }
    }
}
