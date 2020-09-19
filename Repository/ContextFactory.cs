using Microsoft.EntityFrameworkCore;

namespace Infra.Repository
{
    public class ContextFactory : DesignDBFactory<Context>
    {
        protected override Context CreateNewInstance(DbContextOptions<Context> options)
        {
            return new Context(options);
        }
    }
}