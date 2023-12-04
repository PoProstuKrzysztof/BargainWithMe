using BargainWithMe.Core.Contracts.ServiceContracts;
using BargainWithMe.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BargainWithMe.Controllers;

[Route("api/negotiations")]
[ApiController]
public class NegotiationController : ControllerBase
{
    private readonly INegotiationService _negotiationService;

    public NegotiationController(INegotiationService negotiationService)
    {
        _negotiationService = negotiationService;
    }

    [HttpPost("/NegotiationPrice/{productId}/{proposedPrice}")]
    public async Task<IActionResult> NegotiatePrice(Guid productId, double proposedPrice)
    {
        var result = await _negotiationService.NegotiatePrice(productId, proposedPrice);
        await _negotiationService.Save();

        switch (result)
        {
            case NegotiationResult.Success:
                return Ok("Negotiation successful. Price updated.");

            case NegotiationResult.NotFound:
                return NotFound("Product not found.");

            case NegotiationResult.AttemptsLimitExceeded:
                return BadRequest("Negotiation attempts limit exceeded.");

            case NegotiationResult.InvalidPrce:
                return BadRequest("Invalid proposed price.");

            case NegotiationResult.TwiceAsDefaultPrice:
                return BadRequest("Proposed price is twice as default");

            case NegotiationResult.NotAccepted:
                return BadRequest("Proposed price wasn't accepted.Try again.");

            default:
                return StatusCode(500, "Internal server error");
        }
    }
}