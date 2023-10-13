using SmartyStreets.USStreetApi;

namespace AVSandbox.API.Models
{
    public class USStreetAddressVerificationResponse
    {
        public bool RecordFound { get; set; } = false;
        public Components Components { get; set; }
    }
    
}
