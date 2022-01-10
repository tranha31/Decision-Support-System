using DSS.Core.entity;
using DSS.Core.interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DSS.Core.enums.Enum;

namespace DSS.API.Controllers
{
    /// <summary>
    /// Khai báo đường dẫn
    /// Create by: TQHa (22/7/2021)
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        #region fields
        IDoctorService _doctorService;
        #endregion

        #region constructor

        /// <summary>
        /// Khởi tạo đối tượng 
        /// crrate by: TQHa (27/7/2021)
        /// </summary>
        public DoctorController(IDoctorService doctorService) 
        {
            _doctorService = doctorService;

        }
        #endregion

        [HttpPost("electre")]
        public IActionResult GetListElectre(List<ConditionDSS> conditions)
        {
            var result = _doctorService.GetListElectre(conditions);
            if (result.Code == (int)DSSEnum.Success || result.Code == (int)DSSEnum.NotFound)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }

        [HttpPost("dss")]
        public IActionResult GetListDSS(int k, ConditionDSSDoctor conditionDSSDoctor)
        {
            var result = _doctorService.GetListDoctorDSS(k, conditionDSSDoctor.Conditions, conditionDSSDoctor.ProvinceExcept);
            if (result.Code == (int)DSSEnum.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
    }
}
