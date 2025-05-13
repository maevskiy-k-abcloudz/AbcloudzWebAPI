using AbcloudzWebAPI.Contracts.Enums;
using AbcloudzWebAPI.Infrastructure.Attributes;

namespace AbcloudzWebAPI.Contracts.Models.User.GetAll;

public class GetAllUserRequest
{
    public string? Identifier { get; set; }
    public bool? IsActive { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public Sorting Sorting { get; set; } = Sorting.Asc;
    public SortingType SortingType { get; set; } = SortingType.Id;
}