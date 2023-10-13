using Microsoft.AspNetCore.Mvc;
using AVSandbox.API.Models;
using AVSandbox.API.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;


namespace AVSandbox.API.Controllers
{

    [Route("api/usstreet")]
    public class USStreetVerificationController : Controller
    {
        private readonly IAddressVerificationService _addressVerificationService;

        public USStreetVerificationController(IAddressVerificationService addressVerificationService) {
            this._addressVerificationService = addressVerificationService;
        }

        [HttpPost]
        [Route("verify")]
        public async Task<USStreetAddressVerificationResponse> Verify(AddressVerificationRequest request)
        {
            var result = await _addressVerificationService.VerifyUSStreetAddress(request);
            if (result == null)
            {
                throw new ValidationException(HttpStatusCode.BadRequest.ToString());
            }
            else { 
                return result;
            }
        }
    }
}
