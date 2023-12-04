using BargainWithMe.Core.DTO;
using BargainWithMe.Core.Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.Core.Mapper;

[Mapper]
public partial class Mapper
{
    //Products
    public partial ProductDTO MapProductToProductDTO(Product product);

    public partial Product MapProductDtoToProduct(ProductDTO productDto);

    //Catalogs
    public partial CatalogDTO MapCatalogToCatalogDTO(Catalog catalog);

    public partial Catalog MapCatalogDtoToCatalog(CatalogDTO catalogDto);
}