using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tranning.Models
{
    public class Trainee_CourseModel
    {
        public List<Trainee_courseDetail> Trainee_CourseDetailLists { get; set; }
    }

    public class Trainee_courseDetail
    {
        [Required(ErrorMessage = "Please select a Trainee.")]
        public int trainee_id { get; set; }
        [Required(ErrorMessage = "Please select a Course.")]
        public int course_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
    }
}
