#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace the_wall.Models;

public class Comment
{
    [Key]
    public int CommentId { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "Comment length must be greater than ten characters")]
    public string CommentText { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int MessageId { get; set; }
    public int UserId { get; set; }

    public Message? Messages { get; set; }

    public User? Users { get; set; }
}