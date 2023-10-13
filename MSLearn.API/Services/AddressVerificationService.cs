using AVSandbox.API.Models;
using SmartyStreets;
using SmartyStreets.USStreetApi;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AVSandbox.API.Services
{
    public class AddressVerificationService : IAddressVerificationService
    {
        private readonly string authId = "";
        private readonly string authToken = "";
        private readonly SmartyStreets.USStreetApi.Client _usStreetApiClient;
        public AddressVerificationService()
        {
            _usStreetApiClient = new ClientBuilder(authId, authToken)
                .WithLicense(new List<string> { "us-core-enterprise-cloud" })
                .BuildUsStreetApiClient();
        }
        public Task<USStreetAddressVerificationResponse> VerifyUSStreetAddress(AddressVerificationRequest request)
        {
            var lookup = new Lookup
            {
                Street = request.StreetAddress,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode
            };

            try
            {
                _usStreetApiClient.Send(lookup);
            }
            catch (SmartyException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw new InternalServerErrorException(HttpStatusCode.BadRequest.ToString());
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new InternalServerErrorException(HttpStatusCode.BadRequest.ToString());
            }

            var candidates = lookup.Result;

            if (candidates.Count == 0)
            {
                Console.WriteLine("No candidates. This means the address is not valid.");
                return Task.FromResult(new USStreetAddressVerificationResponse() { RecordFound = false });
            }

            var firstCandidate = candidates[0];
            return Task.FromResult(new USStreetAddressVerificationResponse()
            {
                RecordFound = true,
                Components = firstCandidate.Components
            });
        }
    }
}
