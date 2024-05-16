using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace url_shortener.Models;

public class ShortUrl
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(2048)]
    public string OriginalUrl { get; set; } = null!; 
    
    [Required]
    [StringLength(50)]
    public string ShortenedUrl { get; set; } = null!;
    
    [Required]
    public DateTime CreationDate { get; set; }
    
    [Required]
    public int CreatedBy { get; set; }
    
    [ForeignKey("CreatedBy")]
    public User Creator { get; set; } = null!;
}