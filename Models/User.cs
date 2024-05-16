using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace url_shortener.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(30)]
    public string Login { get; set; } = null!;
    
    [Required]
    [StringLength(30)]
    public string Password { get; set; } = null!;
    
    [Required]
    [StringLength(20)]
    public string Role { get; set; } = null!;
    
    public ICollection<ShortUrl> ShortUrls { get; set; } = null!;
}