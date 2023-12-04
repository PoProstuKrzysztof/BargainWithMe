using BargainWithMe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.Core.Contracts.RepositoryContracts;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<ICollection<Product>> GetAllProdutcsAsync();

    Task<ICollection<Product>> GetAllProductsByCatalogAsync(Guid catalogId);

    Task<Product> GetProductByGuidAsync(Guid id);

    void CreateProduct(Product product);

    void UpdateProduct(Product product);

    void DeleteProduct(Product product);
}