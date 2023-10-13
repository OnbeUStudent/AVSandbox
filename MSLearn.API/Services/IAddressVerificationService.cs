using AVSandbox.API.Models;

namespace AVSandbox.API.Services
{
    public interface IAddressVerificationService
    {
        Task<USStreetAddressVerificationResponse> VerifyUSStreetAddress(AddressVerificationRequest request);
    }
}
