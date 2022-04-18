using System;
using System.ComponentModel.DataAnnotations;

namespace assignment_log_and_reg.Models
{
  public class User
  {

    [Key]
    public int UserId { get; set; }


    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

  }
}