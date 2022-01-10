using DSS.Core.entity;
using DSS.Core.interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSS.API.Controllers
{
    /// <summary>
    /// Khai báo đường dẫn
    /// Create by: TQHa (22/7/2021)
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class ProvinceController : BaseController<Province>
    {
        #region fields
        IBaseService<Province> _baseService;
        #endregion

        #region constructor

        /// <summary>
        /// Khởi tạo đối tượng 
        /// crrate by: TQHa (27/7/2021)
        /// </summary>
        public ProvinceController(IBaseService<Province> baseService) : base(baseService)
        {
            _baseService = baseService;

        }
        #endregion

    }
}
