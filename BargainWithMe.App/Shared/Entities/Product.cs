using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.App.Shared.Entities;

public class Product
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; }
    public Price Price { get; set; }

    [NotMapped]
    public int? NegotiationAttempts { get; private set; }

    public int? OnStock { get; set; } = 0;

    public Product()
    {
        NegotiationAttempts = 0;
    }

    public Product(string name, Price price, int onStock)
    {
        Name = name;
        Price = price;
        OnStock = onStock;
        NegotiationAttempts = 0;
    }

    public void ChangeProductPrice(double newPrice)
    {
        Price = Price.ApplyNewPrice(newPrice);
    }

    //public bool NegotiationPrice(double proposedPrice)
    //{
    //    if(NegotiationAttempts > 3)
    //    {
    //        return false;
    //    }

    //    var negotiationService = new PriceNegotiationService();
    //    var negotiationResult = negotiationService.NegotiationPrice(this, proposedPrice);

    //    if (!negotiationResult)
    //    {
    //        NegotiationAttempts++;
    //    }
    //}

    public bool IsRejectedAfterThreeAttempts(double proposedPrice)
    {
        return proposedPrice <= 0 || proposedPrice >= 2 * Price.Amount;
    }

    public override string ToString()
    {
        return $"Product: {Name}, Price: {Price.Amount}, Stock: {OnStock}";
    }

    //Relationships
    public Guid CatalogId { get; set; }

    public Catalog? Catalog { get; set; }
}