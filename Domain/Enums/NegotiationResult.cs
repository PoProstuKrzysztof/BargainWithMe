using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.Core.Enums;

public enum NegotiationResult
{
    Success = 1,
    NotFound = 2,
    AttemptsLimitExceeded = 3,
    InvalidPrce = 4,
    TwiceAsDefaultPrice = 5,
    NotAccepted = 6
}