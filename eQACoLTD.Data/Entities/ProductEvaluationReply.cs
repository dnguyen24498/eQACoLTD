using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class ProductReviewReply
    {
        public string Id { get; set; }
        public string ProductReviewId { get; set; }
        public Guid? UserId { get; set; }
        public string Content { get; set; }

        public ProductEvaluation ProductEvaluation { get; set; }
        public AppUser AppUser { get; set; }
    }
}
