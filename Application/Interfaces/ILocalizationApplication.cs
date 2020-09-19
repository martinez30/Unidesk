using Application.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILocalizationApplication
    {
        Task<IEnumerable<LocalizationModel>> ListAsync();
        Task RegisterAsync(LocalizationModel model);
        Task<LocalizationModel> GetByIdAsync(int id);
        Task EditAsync(LocalizationModel model);

    }
}
