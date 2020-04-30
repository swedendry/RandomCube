using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Databases.Sql;
using Service.Databases.Sql.Models;
using Service.Services;
using System;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api/users/{userId}/cubes")]
    public class CubesController : Controller
    {
        private readonly IServerUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CubesController(
            IServerUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPut("{cubeId}/lv")]
        public async Task<IActionResult> UpdateLv(string userId, int cubeId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetAsync(x => x.Id == userId, isTracking: true);
                if (user == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                var lv = 0;
                var cube = await _unitOfWork.Cubes.GetAsync(x => x.UserId == userId && x.CubeId == cubeId);
                if (cube != null)
                    lv = cube.Lv;

                var price = ServerDefine.Lv2Price((byte)lv);

                if (user.Money < price)
                    return Payloader.Fail(PayloadCode.Not);

                user.Money -= price;

                var newCube = await _unitOfWork.Cubes.UpdateLv(userId, cubeId, 1);

                await _unitOfWork.CommitAsync();

                return Payloader.Success(new UpdateCubeLvBody()
                {
                    Cube = _mapper.Map<CubeViewModel>(newCube),
                    Money = user.Money,
                });
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }
    }
}
