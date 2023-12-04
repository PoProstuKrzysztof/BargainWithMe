using BargainWithMe.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainWithMe.Core.Contracts.ServiceContracts;

public interface INegotiationService
{
    Task<NegotiationResult> NegotiatePrice(Guid productId, double proposedPrice);

    Task Save();
}