using Application.Interfaces;
using Application.Model;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IContext _context;

        public OrderApplication(IContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderModel>> ListAsync()
        {
            var orders = await _context.Orders
                .Include(x => x.LightPole)
                    .ThenInclude(x => x.Localization)
                .Include(x=> x.Proviced_Services)
                .ToListAsync();
            return orders.Select(x => new OrderModel(x));
        }
        public async Task RegisterAsync(CreateOrderModel model)
        {
            var lightPole = await _context.LightPoles.FirstOrDefaultAsync(x =>
                           x.Id == model.LightPoleId
                            );
            if (lightPole == null)
            {
                throw new Exception("Poste de luz não encontrado");
            }

            if (await _context.Orders.AnyAsync(x =>
                                x.LightPole.Id == lightPole.Id &&
                                x.Status == StatusOrder.Open
                                ))
            {
                throw new Exception("Ordem já criada");
            }
            var order = new Order(model.InitialDate,model.Name, model.RequestDescription, model.Problem, lightPole);
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Erro. Por favor tente novamente");
            }
        }
        public async Task<OrderModel> GetByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(x => x.LightPole)
                    .ThenInclude(x => x.Localization)
                .Include(x=> x.Proviced_Services)
                .FirstOrDefaultAsync(x => x.Id == id);
          
            if (order == null)
            {
                throw new Exception("Não existe essa ordem de Serviço");
            }

            return new OrderModel(order);
        }
        public async Task EditAsync(OrderModel model)
        {
            var orderCompare = await _context.Orders
                .FirstOrDefaultAsync(x => x.Id == model.Order.Id);


            if (orderCompare.Status == StatusOrder.Close)
            {
                throw new Exception("Ordem de Serviço foi finalizada. Não é possível editá-la");
            }

            orderCompare.Name = model.Order.Name;
            orderCompare.Problem = model.Order.Problem;
            orderCompare.RequestDescription = model.Order.RequestDescription;

            try
            {
                _context.Orders.Update(orderCompare);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Erro. Por favor tente novamente");
            }
        }
        public async Task Finish(OrderModel model)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == model.Order.Id);
            if (order == null)
            {
                throw new Exception("Ordem de Serviço não existente");
            }
            order.ResponseDescription = model.ResponseDescription;
            order.Status = StatusOrder.Close;
            order.FinishDate = DateTime.Now;

            try
            {
                order.Proviced_Services = new List<Service>();
                foreach (var item in model.Proviced_Services.Where(x=> x != null))
                {
                    var service = new Service(order, item);
                    order.Proviced_Services.Add(service);
                }
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Erro. Por favor tente novamente");
            }
        }

    }
}
