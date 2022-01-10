using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Core.entity
{
    /// <summary>
    /// Đối tượng tổng quát
    /// </summary>
    /// create by: TQHA (24/10/2021)
    public class BaseEntity
    {
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
