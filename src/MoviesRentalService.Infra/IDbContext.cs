using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesRentalService.Infra
{
    public interface IDbContext : IDisposable
    {
        IMongoDatabase Context { get; }

        Task<int> SaveChanges();

        void AddCommand(Func<Task> func);
    }
}
