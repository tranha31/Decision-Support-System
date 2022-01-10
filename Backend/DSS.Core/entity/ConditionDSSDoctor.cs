using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Core.entity
{
    public class ConditionDSSDoctor
    {
        /// <summary>
        /// DS dk
        /// </summary>
        public List<ConditionDSS> Conditions { get; set; }
        /// <summary>
        /// DS tinh loai bo
        /// </summary>
        public List<Guid> ProvinceExcept{get; set;}
    }
}
