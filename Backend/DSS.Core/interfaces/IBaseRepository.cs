using DSS.Core.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Core.interfaces
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lay tat ca
        /// </summary>
        /// <returns></returns>
        ServiceResult GetAll();
        /// <summary>
        /// Cap nhat
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ServiceResult Update(TEntity entity);
        /// <summary>
        /// Lay so luong bien moi truong
        /// </summary>
        /// <returns></returns>
        int GetAmount();
        /// <summary>
        /// Lay so luong bac sy
        /// </summary>
        /// <param name="provinceExcept"></param>
        /// <returns></returns>
        int GetAmountDoctor(List<Guid> provinceExcept);
        /// <summary>
        /// lay tat ca tinh
        /// </summary>
        /// <returns></returns>
        ServiceResult GetProvince();
        /// <summary>
        /// Lay tinh cho ra quyet dinh phan phoi bac sy
        /// </summary>
        /// <param name="provinceExcept"></param>
        /// <returns></returns>
        ServiceResult GetProvinceForDoctor(List<Guid> provinceExcept);
    }
}
