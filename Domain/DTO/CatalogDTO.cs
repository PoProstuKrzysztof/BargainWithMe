using BargainWithMe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.Core.DTO;
public record class CatalogDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    
}