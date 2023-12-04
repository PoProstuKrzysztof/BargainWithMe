using BargainWithMe.App.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.App.Shared.DTO;
public record class ProductDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public Price Price { get; set; } = new Price();

    public int? OnStock { get; set; } = 0;

    public Guid CatalogId { get; set; }
}