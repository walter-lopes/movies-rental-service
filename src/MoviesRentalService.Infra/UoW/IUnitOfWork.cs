using System;
using System.Threading.Tasks;

namespace MoviesRentalService.Infra.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
