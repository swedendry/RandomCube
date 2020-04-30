using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Databases.Sql;
using Service.Services;
using System;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api/users/{userId}/entries")]
    public class EntriesController : Controller
    {
        private readonly IServerUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EntriesController(
            IServerUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> Update(string userId, [FromBody]int[] slots)
        {
            try
            {
                var entity = await _unitOfWork.Entries.GetAsync(x => x.UserId == userId, isTracking: true);
                if (entity == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                entity.Slots = slots;

                await _unitOfWork.CommitAsync();

                return Payloader.Success(slots);
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }
    }
}
