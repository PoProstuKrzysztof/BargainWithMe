using BargainWithMe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.Core.Contracts.RepositoryContracts;

public interface ICatalogRepository : IRepositoryBase<Catalog>
{
    Task<IEnumerable<Catalog>> GetAllCatalogsAsync();

    Task<Catalog> GetProductByGuidAsync(Guid id);

    void CreateCatalog(Catalog catalog);

    void UpdateCatalog(Catalog catalog);

    void DeleteCatalog(Catalog catalog);
}