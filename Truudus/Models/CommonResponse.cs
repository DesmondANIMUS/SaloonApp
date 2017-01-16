namespace Truudus.Models
{
    public class CommonResponse
    {
        public string response { get; set; }
    }

    public class CommonUserResponse
    {
        public string EndUsername { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string SalonName { get; set; }        
        public string ShortDesc { get; set; }
    }
}
