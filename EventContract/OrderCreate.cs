using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventContract
{
    public class OrderCreate
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }

    public override string ToString()
        {
            return $"OrderId: {OrderId}, Customer: {CustomerName}, Total: ₹{TotalAmount}, CreatedAt: {CreatedAt}";
        }
    }
}
