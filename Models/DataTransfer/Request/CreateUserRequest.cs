namespace AbcloudzWebAPI.Models.DataTransfer.Request
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string UserEmail { get; set; }

        public string Password { get; set; }
    }
}
