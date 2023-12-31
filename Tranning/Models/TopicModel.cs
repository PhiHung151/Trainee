﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Tranning.Validations;


namespace Tranning.Models
{
    public class TopicModel
    {
        public List<TopicDetail> TopicDetailLists { get; set; }
    }

    public class TopicDetail
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Enter Course, please")]
        public int course_id { get; set; }

        [Required(ErrorMessage = "Enter name, please")]
        public string name { get; set; }

        public string? description { get; set; }
        public string? documents { get; set; }
        public string? attach_file { get; set; }
        [Required(ErrorMessage = "Choose Status, please")]
        public int status { get; set; }

        [Required(ErrorMessage = "Choose file, please")]
        [AllowedExtensionFile(new string[] { ".png", ".jpg", ".jpeg", ".mp4" })]
        [AllowedSizeFile(3 * 1024 * 1024)]
        public IFormFile? Photo { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public DateTime? deleted_at { get; set; }
    }
}