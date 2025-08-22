using System.ComponentModel.DataAnnotations;

namespace AbcloudzWebAPI.AbcloudsWebAPI.BusinessLayer.DTOs;

public class AddUserDto
{
    [Required]
    public string Firstname { get; set; }
    public string Lastname { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public DateTime DOB { get; set; }
}
