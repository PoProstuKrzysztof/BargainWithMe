using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.App.Shared.Entities;

public class Catalog
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; }

    [NotMapped]
    [AllowNull]
    public int? ProductsInCatalog { get; set; } = 0;

    //Relationships
    public ICollection<Product>? Products { get; set; } = new List<Product>();
}