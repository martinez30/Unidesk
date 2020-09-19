using System.ComponentModel;

namespace Domain
{
    public enum StatusOrder
    {
        [Description("Aberto")]
        Open = 1,
        [Description("Finalizado")]
        Close = 2
    }
}
