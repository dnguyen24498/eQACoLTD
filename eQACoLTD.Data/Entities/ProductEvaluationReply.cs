using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class ProductEvaluationReply
    {
        public string Id { get; set; }
        public string ProductEvaluationId { get; set; }
        public Guid? AppUserId { get; set; }
        public string Content { get; set; }

        public ProductEvaluation ProductEvaluation { get; set; }
        public AppUser AppUser { get; set; }
    }
}
