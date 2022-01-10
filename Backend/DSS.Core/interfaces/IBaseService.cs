using DSS.Core.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Core.interfaces
{
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Lay tat ca
        /// </summary>
        /// <returns></returns>
        ServiceResult GetAll();
        /// <summary>
        /// Update
        /// </summary>
        /// <returns></returns>
        ServiceResult Update(TEntity entity);
        /// <summary>
        /// Lay so luong cua bien moi truong
        /// </summary>
        /// <returns></returns>
        int GetAmount();
        /// <summary>
        /// Thuc hien dss
        /// </summary>
        /// <returns></returns>
        ServiceResult GetListDSS(int k, List<ConditionDSS> conditions);
        /// <summary>
        /// Thuc hien electre
        /// </summary>
        /// <param name="k"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        ServiceResult GetListElectre(List<ConditionDSS> conditions);
    }
}
