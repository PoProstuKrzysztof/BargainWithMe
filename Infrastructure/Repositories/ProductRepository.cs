using BargainWithMe.Core.Contracts.RepositoryContracts;
using BargainWithMe.Core.Entities;
using BargainWithMe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.Infrastructure.Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<ICollection<Product>> GetAllProdutcsAsync()
    {
        return await FindAll()
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<Product> GetProductByGuidAsync(Guid value)
    {
        return await FindByCondition(x => x.Id == value).FirstOrDefaultAsync();
    }

    public void DeleteProduct(Product product)
    {
        Delete(product);
    }

    public void CreateProduct(Product product)
    {
        Create(product);
    }

    public void UpdateProduct(Product product)
    {
        Update(product);
    }

    public async Task<ICollection<Product>> GetAllProductsByCatalogAsync(Guid catalogGuid)
    {
        var productsInSpecificCatalog = FindAll()
            .Where(x => x.Catalog.Id == catalogGuid)
            .ToList();

        return productsInSpecificCatalog;
    }
}