using System.ComponentModel.DataAnnotations;

namespace Myweb.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation property
        public User User { get; set; }
    }

}
