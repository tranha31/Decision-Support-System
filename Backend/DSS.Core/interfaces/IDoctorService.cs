using DSS.Core.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Core.interfaces
{
    public interface IDoctorService : IBaseService<Doctor>
    {
        /// <summary>
        /// Lay ds bs duoc dieu chinh
        /// </summary>
        /// <returns></returns>
        int GetAmountDoctor(List<Guid> provinceExcept);
        /// <summary>
        /// Lay ds ra quyet dinh voi bac sy
        /// </summary>
        /// <param name="k"></param>
        /// <param name="conditions"></param>
        /// <param name="provinceExcept"></param>
        /// <returns></returns>
        ServiceResult GetListDoctorDSS(int k, List<ConditionDSS> conditions, List<Guid> provinceExcept);
    }
}
