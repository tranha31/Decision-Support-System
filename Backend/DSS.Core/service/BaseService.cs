using DSS.Core.entity;
using DSS.Core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DSS.Core.enums.Enum;

namespace DSS.Core.service
{
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        IBaseRepository<TEntity> _baseRepository;
        BaseServiceHelper<TEntity> _baseServiceHelper;
        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _baseServiceHelper = new BaseServiceHelper<TEntity>(_baseRepository);
        }
        #endregion
        public ServiceResult GetAll()
        {
            var result = _baseRepository.GetAll();
            return result;
        }

        public ServiceResult Update(TEntity entity)
        {
            var result = new ServiceResult();
            result = _baseRepository.Update(entity);
            return result;
        }
        /// <summary>
        /// Bat dau ra quyet dinh
        /// </summary>
        /// <param name="k"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public ServiceResult GetListDSS(int k, List<ConditionDSS> conditions)
        {
            var result = new ServiceResult();
            var tmpList = new int[k];
            var amount = GetAmount();
            if (amount == 0)
            {
                result.Code = (int)DSSEnum.ErrorSever;
                result.UserMsg = "Lỗi hệ thống";
            }
            else
            {
                var listDevide = _baseServiceHelper.DevideOptimal(k, amount, tmpList, 0);

                var listProvince = _baseServiceHelper.Topsis(conditions);
                if (listProvince == null)
                {
                    result.Code = (int)DSSEnum.ErrorSever;
                    result.UserMsg = "Lỗi hệ thống";
                }
                else
                {
                    Dictionary<string, object> result1 = new Dictionary<string, object>();
                    result1.Add("ListDevide", listDevide);
                    result1.Add("ListProvince", listProvince);
                    result.Data = result1;
                    result.Code = (int)DSSEnum.Success;
                }

            }
            return result;
        }

        /// <summary>
        /// Lay so luong 
        /// </summary>
        /// <returns></returns>
        public int GetAmount()
        {
            var result = _baseRepository.GetAmount();
            return result;
        }

        public ServiceResult GetListElectre(List<ConditionDSS> conditions)
        {
            var result = new ServiceResult();
            var entity = _baseServiceHelper.Electre(conditions) as List<object>;
            if(entity.Count() > 0)
            {
                result.Code = (int)DSSEnum.Success;
                result.Data = entity;
            }
            else
            {
                result.Code = (int)DSSEnum.NotFound;
                result.Data = null;
                result.UserMsg = "Không tìm được tỉnh thành phù hợp";
            }
            return result;
        }
    }
}
