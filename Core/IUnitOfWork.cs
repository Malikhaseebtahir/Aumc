using System.Threading.Tasks;

namespace Aumc.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();         
    }
}