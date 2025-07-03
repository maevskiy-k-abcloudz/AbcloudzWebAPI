using System.ComponentModel.DataAnnotations;

namespace AbcloudzWebAPI.DTO
{
    public class CreateUserDTO
    {
        [Phone]
        public required string PhoneNumber { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [MinLength(6)]
        public required string Password { get; set; }
    }

    public class UpdateUserDTO
    {
        public required Guid Id { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
    }                
}
