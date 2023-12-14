using System;
using System.ComponentModel.DataAnnotations;

namespace Tranning.Models
{
    public class Trainner_TopicModel
    {
        public List<Trainner_TopicDetail> Trainner_TopicDetailLists { get; set; }
    }
    public class Trainner_TopicDetail
    {
        [Required(ErrorMessage = "Please select a user.")]
        public int trainner_id { get; set; }

        [Required(ErrorMessage = "Please select a topic.")]
        public int topic_id { get; set; }

        // Other properties...

        [DataType(DataType.Date)]
        public DateTime? created_at { get; set; }

        [DataType(DataType.Date)]
        public DateTime? updated_at { get; set; }

        [DataType(DataType.Date)]
        public DateTime? deleted_at { get; set; }
    }
}
