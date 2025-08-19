namespace AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Models;

public class UserSearchModel
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public DateTime? DOB { get; set; }

    public string? SortBy { get; set; }
    public int? Take { get; set; }
    public int? Skip { get; set; }
}
