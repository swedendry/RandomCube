using Service.Databases.Sql.Core;
using Service.Databases.Sql.Models;
using System.Threading.Tasks;

namespace Service.Databases.Sql.Repositories
{
    public interface ICubeRepository : IRepository<Cube>
    {
        Task<Cube> UpdateLv(string userId, int cubeId, byte lv);
    }

    public class CubeRepository : Repository<ServerUnitOfWork, Cube>, ICubeRepository
    {
        public CubeRepository(ServerUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<Cube> UpdateLv(string userId, int cubeId, byte addLv)
        {
            var data = await _unitOfWork.CubeDatas.GetAsync(x => x.CubeId == cubeId, isTracking: true);
            if (data == null)
                return null;

            var cube = await _unitOfWork.Cubes.GetAsync(x => x.UserId == userId && x.CubeId == cubeId, isTracking: true);
            if (cube == null)
            {
                cube = new Cube()
                {
                    UserId = userId,
                    CubeId = cubeId,
                    Lv = addLv,
                    Parts = 0,
                    CubeData = data,
                };

                await AddAsync(cube);
            }
            else
            {
                cube.Lv += addLv;
            }

            return cube;
        }
    }
}
