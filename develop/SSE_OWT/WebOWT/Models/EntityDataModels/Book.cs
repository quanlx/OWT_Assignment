using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebOWT.Models.EntityDataModels
{
    public partial class Book
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Owner { get; set; }
        [Required]
        public string CoverPhoto { get; set; }
        [Required]
        public int CateId { get; set; }
        public virtual Category Cate { get; set; }
    }
}
