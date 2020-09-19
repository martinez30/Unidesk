using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class LightPole : BaseEntity
    {
        public Localization Localization { get; set; }
        public int? Number { get; set; }
        public string SerialNumber { get; set; }
        public bool Active { get; set; }

        protected LightPole() { }

        public LightPole(Localization localization, int? number, bool active, string serialNumber)
        {
            Localization = localization;
            Number = number;
            Active = active;
            SerialNumber = serialNumber;
        }

    }
}
