namespace AVSandbox.API.Models
{
    public class AddressVerificationRequest
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

    }
}
