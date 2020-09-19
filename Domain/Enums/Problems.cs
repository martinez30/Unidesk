using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain
{
    public enum Problems
    {
        [Description("Luz apagada durante a noite")]
        OffNight,
        [Description("Luz acessa durante o dia")]
        OnDay,
        [Description("Luz acende e apaga")]
        OnOffLoop,
        [Description("Fio partido")]
        BrokenWire,
    }
}
