using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Databases.Sql;
using Service.Databases.Sql.Models;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Controllers
{
    [Route("api/managers")]
    public class ManagersController : Controller
    {
        private readonly IServerUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManagersController(
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
                var entries = await _unitOfWork.Managers.GetManyAsync();
                if (entries == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                return Payloader.Success(_mapper.Map<IEnumerable<ManagerViewModel>>(entries));
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
                var entity = await _unitOfWork.Managers.GetAsync(u => u.Id == id);
                if (entity == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                return Payloader.Success(_mapper.Map<ManagerViewModel>(entity));
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ManagerViewModel body)
        {
            try
            {
                var entity = await _unitOfWork.Managers.GetAsync(u => u.Id == body.Id);
                if (entity != null)
                    return Payloader.Fail(PayloadCode.Duplication);

                var newEntity = new Manager
                {
                    Id = body.Id,
                    Password = body.Password,
                };

                await _unitOfWork.Managers.AddAsync(newEntity);
                await _unitOfWork.CommitAsync();

                return Payloader.Success(_mapper.Map<ManagerViewModel>(newEntity));
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]ManagerViewModel body)
        {
            try
            {
                var entity = await _unitOfWork.Managers.GetAsync(u => u.Id == id, isTracking: true);
                if (entity == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                entity.Password = body.Password;

                await _unitOfWork.CommitAsync();

                return Payloader.Success(_mapper.Map<ManagerViewModel>(entity));
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
                var entity = await _unitOfWork.Managers.GetAsync(u => u.Id == id);
                if (entity == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                await _unitOfWork.Managers.DeleteAsync(entity);
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
