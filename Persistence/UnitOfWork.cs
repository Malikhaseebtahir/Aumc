using Aumc.Core;
using System.Threading.Tasks;

namespace Aumc.Persistence
{
    public class UnitOfWork: IUnitOfWork
    {
        public ApplicationDbContext _context { get; }
        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task CompleteAsync()
        {
            await this._context.SaveChangesAsync();
        }
    }
}