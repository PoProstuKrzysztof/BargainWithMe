using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.Core.DTO;
public record class CatalogWithProductsDTO
{
    public CatalogDTO? Catalog { get; set; }

    public ICollection<ProductDTO>? Products { get; set; }
}