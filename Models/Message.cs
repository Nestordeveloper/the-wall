#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace the_wall.Models;

public class Message
{
    [Key]
    public int MessageId { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "Message length must be greater than ten characters")]
    public string MessageText { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int UserId { get; set; }
}