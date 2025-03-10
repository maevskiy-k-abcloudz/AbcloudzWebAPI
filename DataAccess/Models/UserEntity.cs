namespace AbcloudzWebAPI.DataAccess.Models
{
    public class UserEntity
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public string UserEmail { get; set; }
    }
}
