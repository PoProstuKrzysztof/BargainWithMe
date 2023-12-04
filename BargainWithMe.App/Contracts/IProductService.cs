using BargainWithMe.App.Shared.Entities;

namespace BargainWithMe.App.Contracts;

public interface IProductService
{
    Task<Catalog> GetAssignedCatalogAsync(Guid id);
}