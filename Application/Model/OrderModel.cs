using Domain;
using Infra;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace Application.Model
{
    public class OrderModel 
    {
        public CreateOrderModel Order { get; set; }
        public string ResponseDescription { get; set; }
        public List<string> Proviced_Services { get; set; }

        public OrderModel()
        {

        }

        public OrderModel(Order order)
        {
            Order = new CreateOrderModel(
                order.Id,
                order.InitialDate,
                $"{order.LightPole.Localization.Address} - {order.LightPole.Localization.Neighborhood} - {order.LightPole.Localization.City}/{order.LightPole.Localization.State}",
                order.LightPole.Id,
                order.Name,
                order.Problem,
                order.Status,
                order.RequestDescription
                );
            Proviced_Services = new List<string>();
            foreach (var item in order.Proviced_Services)
            {
                Proviced_Services.Add(item.Description);
            }
        }

    }
}
