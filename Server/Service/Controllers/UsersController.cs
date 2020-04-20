using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Databases.Sql;
using Service.Databases.Sql.Models;
using Service.Services;
using System;
using System.Collections.Generic;
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
                var entity = await _unitOfWork.Users.GetAsync(u => u.Id == id);
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
                var entity = await _unitOfWork.Users.GetAsync(u => u.Id == body.Id);
                if (entity != null)
                    return Payloader.Fail(PayloadCode.Duplication);

                var newEntity = new User
                {
                    Id = body.Id,
                    Name = body.Name,
                };

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
                var entity = await _unitOfWork.Users.GetAsync(u => u.Id == id, isTracking: true);
                if (entity == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                entity.Name = body.Name;

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
                var entity = await _unitOfWork.Users.GetAsync(u => u.Id == id);
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
    }
}
