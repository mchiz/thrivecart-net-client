using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriveCart;

namespace ThriveCart {
    public struct TransactionsFilter {
        public TransactionsFilter( ) { }

        public TransactionType TransactionType = TransactionType.Any;
        public string CurrencyCode;
    }

}
