using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.Infrastructure.Exceptions;

public sealed class AmountBelowZeroException : Exception
{
    public AmountBelowZeroException(string message) : base("Amount can't be below 0.")
    { }

    public override string? StackTrace
    {
        get
        {
            return "";
        }
    }
}