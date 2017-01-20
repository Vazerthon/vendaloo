using System.Collections.Generic;

namespace Vendaloo.Models
{
    public class TransactionResult
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public IEnumerable<IMoney> Change { get; set; }
    }
}
