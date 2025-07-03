namespace AbcloudzWebAPI.DTO
{
    public class PagedResponse<T>
    {
        public ICollection<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
