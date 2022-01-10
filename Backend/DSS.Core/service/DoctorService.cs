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
    public class DoctorService : BaseService<Doctor>, IDoctorService
    {
        IBaseRepository<Doctor> _baseRepository;
        BaseServiceHelper<Doctor> _baseServiceHelper;
        public DoctorService(IBaseRepository<Doctor> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _baseServiceHelper = new BaseServiceHelper<Doctor>(_baseRepository);
        }

        public int GetAmountDoctor(List<Guid> provinceExcept)
        {
            var result = _baseRepository.GetAmountDoctor(provinceExcept);
            return result;
        }

        /// <summary>
        /// DSS ds province
        /// </summary>
        /// <param name="k"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public ServiceResult GetListDoctorDSS(int k, List<ConditionDSS> conditions, List<Guid> provinceExcept)
        {
            var result = new ServiceResult();
            var tmpList = new int[k];
            var amount = GetAmountDoctor(provinceExcept);
            if (amount == -1)
            {
                result.Code = (int)DSSEnum.ErrorSever;
                result.UserMsg = "Lỗi hệ thống";
            }
            else
            {
                var listDevide = _baseServiceHelper.DevideOptimal(k, amount, tmpList, 0);

                var listProvince = _baseServiceHelper.TopsisDoctor(conditions, provinceExcept);
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
    }
}
