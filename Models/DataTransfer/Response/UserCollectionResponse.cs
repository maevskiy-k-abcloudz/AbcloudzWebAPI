namespace AbcloudzWebAPI.Models.DataTransfer.Response
{
    public class UserCollectionResponse
    {
        public List<UserResponse> Users { get; set; } = new List<UserResponse>();

        public int TotalCount { get; set; } = 0;
    }
}
