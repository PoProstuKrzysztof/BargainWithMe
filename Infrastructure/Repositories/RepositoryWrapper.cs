using BargainWithMe.Core.Contracts.RepositoryContracts;
using BargainWithMe.Core.Services;
using BargainWithMe.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.Infrastructure.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly RepositoryContext _context;

    private ICatalogRepository _catalog;
    private IProductRepository _product;

    public RepositoryWrapper(RepositoryContext context)
    {
        _context = context;
    }

    public ICatalogRepository Catalog
    {
        get
        {
            if (_catalog == null)
            {
                _catalog = new CatalogRepository(_context);
            }

            return _catalog;
        }
    }

    public IProductRepository Product
    {
        get
        {
            if (_product == null)
            {
                _product = new ProductRepository(_context);
            }

            return _product;
        }
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}