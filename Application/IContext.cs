using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public interface IContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<LightPole> LightPoles { get; set; }
        DbSet<Localization> Localizations { get; set; }
        DbSet<Service> Services { get; set; }
        
        IExecutionStrategy CreateExecutionStrategy();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
