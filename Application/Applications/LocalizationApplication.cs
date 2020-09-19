using Application.Interfaces;
using Application.Model;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class LocalizationApplication : ILocalizationApplication
    {
        private readonly IContext _context;

        public LocalizationApplication(IContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LocalizationModel>> ListAsync()
        {
            var localizations = await _context.Localizations.ToListAsync();
            return localizations.Select(x => new LocalizationModel(x));
        }

        public async Task RegisterAsync(LocalizationModel model)
        {
            if (await _context.Localizations.AnyAsync(x =>
                            x.Address.Trim().ToLower() == model.Address.Trim().ToLower() &&
                            x.City.Trim().ToLower() == model.City.Trim().ToLower() &&
                            x.Neighborhood.Trim().ToLower() == model.Neighborhood.Trim().ToLower() &&
                            x.State == model.State))
            {
                throw new Exception("Localização já cadastrada");
            }
            var localization = new Localization(model.Address, model.Neighborhood, model.City, model.State.Value);
            try
            {
                await _context.Localizations.AddAsync(localization);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Erro. Por favor tente novamente");
            }
        }

        public async Task<LocalizationModel> GetByIdAsync(int id)
        {
            var loc = await _context.Localizations.FirstOrDefaultAsync(x => x.Id == id);
            if(loc == null)
            {
                throw new Exception("Localização não encontrada");
            }
            return new LocalizationModel(loc);
        }

        public async Task EditAsync(LocalizationModel model)
        {
            if (await _context.Localizations.AnyAsync(x => x.Address == model.Address && x.City == model.City))
                throw new Exception("Já existe essa localização");
            var localizations = await _context.Localizations
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            if (localizations == null)
            {
                throw new Exception("Poste de luz não encontrado");
            }
            localizations.Address = model.Address;
            localizations.Neighborhood = model.Neighborhood;
            localizations.City = model.City;
            localizations.State = model.State.Value;

            try
            {
                _context.Localizations.Update(localizations);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Erro. Por favor tente novamente");
            }
        }

    }
}
