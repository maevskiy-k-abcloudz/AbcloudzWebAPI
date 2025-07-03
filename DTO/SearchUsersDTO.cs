using System.ComponentModel.DataAnnotations;

namespace AbcloudzWebAPI.DTO
{
    public class SearchUsersDTO
    {
        public string? Term { get; set; }
        [Range(1, int.MaxValue)]
        public int Page { get; set; }
        [Range(0, int.MaxValue)]
        public int PageSize { get; set; }
        public UserSortFields Field { get; set; }
        public bool IsAscending { get; set; }
    }

    public enum UserSortFields
    {
        PhoneNumber,
        Email,
        FirstName,
        LastName
    }
}
