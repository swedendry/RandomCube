using Service.Databases.Sql.Core;
using Service.Databases.Sql.Models;

namespace Service.Databases.Sql.Repositories
{
    public interface ICubeRepository : IRepository<Cube>
    {

    }

    public class CubeRepository : Repository<ServerUnitOfWork, Cube>, ICubeRepository
    {
        public CubeRepository(ServerUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
