using BargainWithMe.App.Shared.DTO;

namespace BargainWithMe.App.Contracts;

public interface ICatalogService
{
    Task<ICollection<ProductDTO>> GetAssignedProductsAsync(Guid id);
}