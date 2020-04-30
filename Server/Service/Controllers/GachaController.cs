using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Databases.Sql;
using Service.Extensions;
using Service.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api/users/{userId}/gacha")]
    public class GachaController : Controller
    {
        private readonly IServerUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GachaController(
            IServerUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Pick(string userId, [FromBody]GachaId gachaId)
        {
            try
            {
                var entity = await _unitOfWork.Users.GetAsync(x => x.Id == userId);
                if (entity == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                var cubes = await _unitOfWork.CubeDatas.GetManyAsync();
                var parts = 0;
                var random = new Random();
                switch (gachaId)
                {
                    case GachaId.Cube_Normal:
                        {
                            parts = random.Next(1, 2);
                        }
                        break;
                    case GachaId.Cube_Premium:
                        {
                            parts = random.Next(5, 10);
                        }
                        break;
                }

                var newCubes = cubes.RandomNoShuffle(parts);
                var group = newCubes.GroupBy(x => x.CubeId);
                foreach (var cube in group)
                {
                    var cubeId = cube.Key;
                    var count = cube.Count();


                }

                //await _unitOfWork.Users.AddAsync(newEntity);
                //await _unitOfWork.CommitAsync();

                return Payloader.Success(true);
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }
    }
}
