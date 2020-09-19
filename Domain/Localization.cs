using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Localization : BaseEntity
    {
        public string Address { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public State State { get; set; }

        public Localization()
        {
        }

        public Localization(string address, string neighborhood, string city, State state)
        {
            Address = address;
            Neighborhood = neighborhood;
            City = city;
            State = state;
        }

    }
}
