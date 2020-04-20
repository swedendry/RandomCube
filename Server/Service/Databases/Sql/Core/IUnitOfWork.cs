using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Service.Databases.Sql.Core
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        Task CommitAsync();
    }
}
