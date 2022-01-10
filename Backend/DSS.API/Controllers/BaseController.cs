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

    public class BaseController<TEntity> : Controller
    {
        #region fields
        IBaseService<TEntity> _baseService;
        #endregion

        #region Constructor
        public BaseController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _baseService.GetAll();
            if(result.Code == (int)DSSEnum.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }

        [HttpPost("dss")]
        public IActionResult GetListDSS(int k, List<ConditionDSS> conditions)
        {
            var result = _baseService.GetListDSS(k, conditions);
            if (result.Code == (int)DSSEnum.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
        /// <summary>
        /// Bo qua validate (do khong phai yeu cau cua de tai)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(TEntity entity)
        {
            var result = _baseService.Update(entity);
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
