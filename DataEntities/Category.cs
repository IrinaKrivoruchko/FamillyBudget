using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class Category
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }

        public TransactionCard TransactionCard { get; set; }
        public TransactionCash TransactionCash { get; set; }
        public TransactionDeposit TransactionDeposit { get; set; }
    }
}
