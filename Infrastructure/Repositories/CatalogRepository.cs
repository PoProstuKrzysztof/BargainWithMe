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

public class CatalogRepository : RepositoryBase<Catalog>, ICatalogRepository
{
    public CatalogRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateCatalog(Catalog catalog)
    {
        Create(catalog);
    }

    public void DeleteCatalog(Catalog catalog)
    {
        Delete(catalog);
    }

    public void UpdateCatalog(Catalog catalog)
    {
        Update(catalog);
    }

    public async Task<IEnumerable<Catalog>> GetAllCatalogsAsync()
    {
        return await FindAll()
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Catalog> GetProductByGuidAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id)
            .FirstOrDefaultAsync();
    }
}