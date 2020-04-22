using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Databases.Sql;
using Service.Databases.Sql.Models;
using Service.Services;
using Service.Xmls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminServer.Controllers
{
    [Route("api/datas")]
    public class DatasController : Controller
    {
        private readonly IServerUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DatasController(
            IServerUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("parsing")]
        public async Task<IActionResult> Parsing()
        {
            try
            {
                await ParsingCubeData();

                return Payloader.Success("Parse Success");
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }

        [HttpPost("CubeData")]
        public async Task<IActionResult> ParsingCubeData(IFormFile xml = null)
        {
            try
            {
                var datas = XMLNAME.CubeData.FindAll<CubeDataXml.Data>(xml, new CubeDataXml());

                await _unitOfWork.CubeDatas.DeleteManyAsync();
                await _unitOfWork.CubeDatas.AddAsync(_mapper.Map<IEnumerable<CubeData>>(datas));
                await _unitOfWork.CommitAsync();

                return Payloader.Success(datas.Count);
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }
    }
}
