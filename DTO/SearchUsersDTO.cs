namespace AbcloudzWebAPI.DTO
{
    public class SearchUsersDTO
    {
        public string? Term { get; set; }
        public int Page { get; set; }
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
