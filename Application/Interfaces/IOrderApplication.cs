using Application.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderApplication
    {
        Task<IEnumerable<OrderModel>> ListAsync();
        Task RegisterAsync(CreateOrderModel model);
        Task EditAsync(OrderModel model);
        Task<OrderModel> GetByIdAsync(int id);
        Task Finish(OrderModel model);
    }
}
