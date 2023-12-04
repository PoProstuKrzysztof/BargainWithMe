using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.Core.Exceptions;

public sealed class NoRecordsException : Exception
{
    public NoRecordsException(string message) : base("There are no records in database.")
    {
    }

    public override string? StackTrace
    {
        get
        {
            return "";
        }
    }
}