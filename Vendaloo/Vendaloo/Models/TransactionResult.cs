using System.Collections;
using System.Collections.Generic;

namespace Vendaloo.Models
{
    public class TransactionResult
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public IEnumerable<Coin> Change { get; set; }
    }
}
