using Service.Databases.Sql.Core;
using Service.Databases.Sql.Models;

namespace Service.Databases.Sql
{
    public interface IServerUnitOfWork : IUnitOfWork
    {
        IRepository<Manager> Managers { get; }

        IRepository<CubeData> CubeDatas { get; }

        IRepository<User> Users { get; }
        IRepository<Entry> Entries { get; }
        IRepository<Cube> Cubes { get; }
    }

    public class ServerUnitOfWork : UnitOfWork, IServerUnitOfWork
    {
        public ServerUnitOfWork(ServerContext context)
            : base(context)
        { }

        private IRepository<Manager> managers;
        public IRepository<Manager> Managers => managers ??= new Repository<ServerUnitOfWork, Manager>(this);

        private IRepository<CubeData> cubeDatas;
        public IRepository<CubeData> CubeDatas => cubeDatas ??= new Repository<ServerUnitOfWork, CubeData>(this);

        private IRepository<User> users;
        public IRepository<User> Users => users ??= new Repository<ServerUnitOfWork, User>(this);

        private IRepository<Entry> entries;
        public IRepository<Entry> Entries => entries ??= new Repository<ServerUnitOfWork, Entry>(this);

        private IRepository<Cube> cubes;
        public IRepository<Cube> Cubes => cubes ??= new Repository<ServerUnitOfWork, Cube>(this);
    }
}
