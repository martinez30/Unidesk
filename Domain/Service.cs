using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Service : BaseEntity
    {
        public Order Order { get; set; }
        public string Description { get; set; }

        protected Service() { }

        public Service(Order order, string description)
        {
            Order = order;
            Description = description;
        }

    }
}
