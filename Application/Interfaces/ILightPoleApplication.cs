using Application.Model;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILightPoleApplication
    {
        Task<IEnumerable<LightPoleModel>> ListAsync();
        Task RegisterAsync(LightPoleModel model);
        Task EditAsync(LightPoleModel model);
        Task<LightPoleModel> GetByIdAsync(int id);
    }
}
