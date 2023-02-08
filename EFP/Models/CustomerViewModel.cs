using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFP.Models
{
    public class CustomerViewModel
    {
        public Customer Cust { get; set; }
        public List<CustomerFinance> Finances { get; set; }
    }
}
