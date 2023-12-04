using BargainWithMe.App.Contracts;
using BargainWithMe.App.Shared.Entities;

namespace BargainWithMe.App.Services;

public class ProductService : IProductService
{
    public Task<Catalog> GetAssignedCatalogAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}