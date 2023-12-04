using BargainWithMe.App.Contracts;
using BargainWithMe.App.Shared.DTO;

namespace BargainWithMe.App.Services;

public class CatalogService : ICatalogService
{
    private readonly Uri _baseAddress = new("https://localhost:7180/Catalog");
    private readonly HttpClient _client = new();

    public CatalogService()
    {
        _client.BaseAddress = _baseAddress;
    }

    public async Task<ICollection<ProductDTO>> GetAssignedProductsAsync(Guid id)
    {
        ICollection<ProductDTO>? produtcsInCatalog = await
            _client.GetFromJsonAsync<ICollection<ProductDTO>>(_baseAddress + $"/catalog");
        return produtcsInCatalog;
    }
}