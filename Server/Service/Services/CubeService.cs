using Service.Databases.Sql;

namespace Service.Services
{
    public interface IPlayerService
    {
    }

    public class CubeService : IPlayerService
    {
        private readonly IServerUnitOfWork _unitOfWork;

        public CubeService(IServerUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
