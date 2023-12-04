using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.Core.Contracts.RepositoryContracts;

public interface IRepositoryWrapper
{
    ICatalogRepository Catalog { get; }
    IProductRepository Product { get; }

    Task Save();
}