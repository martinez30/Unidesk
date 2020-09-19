using Application.Interfaces;
using Application.Model;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class LightPoleApplication : ILightPoleApplication
    {
        private readonly IContext _context;

        public LightPoleApplication(IContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LightPoleModel>> ListAsync()
        {
            var lightPoles = await _context.LightPoles
                .Include(x=> x.Localization)
                .ToListAsync();
            return lightPoles.Select(x => new LightPoleModel(x));
        }
        public async Task RegisterAsync(LightPoleModel model)
        {
            if(await _context.LightPoles.AnyAsync(x => x.SerialNumber == model.SerialNumber))
            {
                throw new Exception("Poste de luz já instalado");
            }
            var localization = await _context.Localizations.FirstOrDefaultAsync(x =>x.Id == model.LocId);
            if(localization == null)
            {
                throw new Exception("Localização não encontrada");
            }
            var lightPole = new LightPole(localization,model.NumberHouse, model.Active, model.SerialNumber);
            try
            {
                await _context.LightPoles.AddAsync(lightPole);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Erro. Por favor tente novamente");
            }
        }
        public async Task EditAsync(LightPoleModel model)
        {
            if (await _context.LightPoles.AnyAsync(x => x.SerialNumber == model.SerialNumber && x.Id != model.Id))
                throw new Exception("Já existe um posto de luz com esse código de série");
            var lightPole = await _context.LightPoles
                .Include(x=> x.Localization)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if(lightPole == null)
            {
                throw new Exception("Poste de luz não encontrado");
            }
            lightPole.Number = model.NumberHouse;
            lightPole.SerialNumber = model.SerialNumber;
            lightPole.Active = model.Active;

            try
            {
                _context.LightPoles.Update(lightPole);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Erro. Por favor tente novamente");
            }
        }
        public async Task<LightPoleModel> GetByIdAsync(int id)
        {
            var lightPole = await _context.LightPoles
                .Include(x=> x.Localization)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (lightPole == null)
            {
                throw new Exception("Localização não encontrada");
            }
            return new LightPoleModel(lightPole);
        }
    }
}
