using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Databases.Sql;
using Service.Databases.Sql.Models;
using Service.Extensions;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IServerUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(
            IServerUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var entries = await _unitOfWork.Users.GetManyAsync();
                if (entries == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                return Payloader.Success(_mapper.Map<IEnumerable<UserViewModel>>(entries));
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var entity = await _unitOfWork.Users.GetAsync(x => x.Id == id,
                    eagerLoad: q => q.Include(x => x.Entry),
                    explicitLoad: q => q.Include(x => x.Cubes).ThenInclude(x => x.CubeData).Load(),
                    isTracking: true);

                if (entity == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                return Payloader.Success(_mapper.Map<UserViewModel>(entity));
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserBody body)
        {
            try
            {
                var entity = await _unitOfWork.Users.GetAsync(x => x.Id == body.Id);
                if (entity != null)
                    return Payloader.Fail(PayloadCode.Duplication);

                var newEntity = new User
                {
                    Id = body.Id,
                    Name = body.Name,
                    Money = 1000,
                };

                await UpdateCube(newEntity);

                await _unitOfWork.Users.AddAsync(newEntity);
                await _unitOfWork.CommitAsync();

                return Payloader.Success(_mapper.Map<UserViewModel>(newEntity));
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]UserViewModel body)
        {
            try
            {
                var entity = await _unitOfWork.Users.GetAsync(x => x.Id == id, isTracking: true);
                if (entity == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                //entity.Name = body.Name;
                entity.Money = body.Money;

                await _unitOfWork.CommitAsync();

                return Payloader.Success(_mapper.Map<UserViewModel>(entity));
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var entity = await _unitOfWork.Users.GetAsync(x => x.Id == id);
                if (entity == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                await _unitOfWork.Users.DeleteAsync(entity);
                await _unitOfWork.CommitAsync();

                return Payloader.Success(id);
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }

        private async Task UpdateCube(User user)
        {
            var allCubes = await _unitOfWork.CubeDatas.GetManyAsync(isTracking: true);
            var cubes = allCubes.Random(5);//ServerDefine.MAX_ENTRY_SLOT);

            cubes.ForEach((x, i) =>
            {
                user.Cubes.Add(new Cube()
                {
                    UserId = user.Id,
                    CubeId = x.CubeId,
                    CubeData = x,
                    Lv = 1,
                    Parts = 0,
                });
            });

            user.Entry = new Entry()
            {
                UserId = user.Id,
                Slots = user.Cubes.Take(ServerDefine.MAX_ENTRY_SLOT).Select(x => x.CubeId).ToArray(),
            };
        }
    }
}
