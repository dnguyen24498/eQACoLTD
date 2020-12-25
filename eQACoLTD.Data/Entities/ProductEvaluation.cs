using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class ProductEvaluation
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public Guid? AppUserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Stars { get; set; }

        public Product Product { get; set; }
        public AppUser AppUser { get; set; }

        public List<ProductEvaluationReply> ProductReviewReplies { get; set; }
    }
}
