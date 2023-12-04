using BargainWithMe.Core.Contracts.RepositoryContracts;
using BargainWithMe.Core.Contracts.ServiceContracts;
using BargainWithMe.Core.Enums;
using BargainWithMe.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.Core.Services;

public class NegotiationService : INegotiationService
{
    /// <summary>
    /// I used enums to better understand the results of negotiations.
    /// </summary>
    private readonly RepositoryContext _context;

    private readonly IProductRepository _productRepository;

    public NegotiationService(IProductRepository productRepository, RepositoryContext context)
    {
        _productRepository = productRepository;
        _context = context;
    }

    /// <summary>
    ///   Negotiation of proposed price if it passes conditions like:
    /// -- proposed price below or equal to 0 are noc accepted
    /// -- proposed price is twiche the default price of the product
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="proposedPrice"></param>
    /// <returns></returns>
    public async Task<NegotiationResult> NegotiatePrice(Guid productId, double proposedPrice)
    {
        try
        {
            var product = await _productRepository.GetProductByGuidAsync(productId);
            if (product is null)
            {
                return NegotiationResult.NotFound;
            }

            if (proposedPrice <= 0)
            {
                return NegotiationResult.InvalidPrce;
            }

            if (proposedPrice >= product.Price.Amount * 2)
            {
                return NegotiationResult.TwiceAsDefaultPrice;
            }

            if (product.NegotiationAttempts >= 3)
            {
                return NegotiationResult.AttemptsLimitExceeded;
            }

            bool employeeResult = SimulateEmployeeReponse();

            if (employeeResult)
            {
                product.ChangeProductPrice(proposedPrice);

                _productRepository.UpdateProduct(product);
            }
            else
            {
                product.AddNegotiationAttempts();
                _productRepository.UpdateProduct(product);

                return NegotiationResult.NotAccepted;
            }

            return NegotiationResult.Success;
        }
        catch (Exception)
        {
            throw new Exception("Error occured in negotiation service.");
        }
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    private bool SimulateEmployeeReponse()
    {
        var randomValue = new Random();
        // I decided to assign more chances of acceptation, for now its 70% that it will be accepted
        return randomValue.NextDouble() < 0.9;
    }
}