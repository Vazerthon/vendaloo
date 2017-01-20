using System.Collections.Generic;
using Vendaloo.Models;

namespace Vendaloo.Contracts
{
    public interface IManageMoney
    {
        IEnumerable<IMoney> GetAllowedDenominations();

        IEnumerable<IMoney> GetValueAsMoney(decimal value);
    }
}
