using BargainWithMe.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BargainWithMe.Core.Entities;

// I used strongly typed record type to improve data manipulations
[Owned]
public record Price
{
    public double Amount { get; init; }

    public Price()
    {
    }
    public Price(double value)
    {
        Amount = value < 0 ? throw new AmountBelowZeroException("Amount must be greater than 0") : value;
    }

    public Price ApplyNewPrice(double value)
    {
        if (value < 0) throw new AmountBelowZeroException("Amount must be greater than 0");

        return this with { Amount = value };
    }
}