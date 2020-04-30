using Service.Databases.Sql.Core;
using Service.Databases.Sql.Models;
using Service.Databases.Sql.Repositories;

namespace Service.Databases.Sql
{
    public interface IServerUnitOfWork : IUnitOfWork
    {
        IRepository<Manager> Managers { get; }

        IRepository<CubeData> CubeDatas { get; }
        IRepository<SkillData> SkillDatas { get; }

        IRepository<User> Users { get; }
        IRepository<Entry> Entries { get; }
        ICubeRepository Cubes { get; }
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

        private IRepository<SkillData> skillDatas;
        public IRepository<SkillData> SkillDatas => skillDatas ??= new Repository<ServerUnitOfWork, SkillData>(this);

        private IRepository<User> users;
        public IRepository<User> Users => users ??= new Repository<ServerUnitOfWork, User>(this);

        private IRepository<Entry> entries;
        public IRepository<Entry> Entries => entries ??= new Repository<ServerUnitOfWork, Entry>(this);

        private ICubeRepository cubes;
        public ICubeRepository Cubes => cubes ??= new CubeRepository(this);
    }
}
