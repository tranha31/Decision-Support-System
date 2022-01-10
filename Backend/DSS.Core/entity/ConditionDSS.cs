using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Core.entity
{
    /// <summary>
    /// Điều kiện ra quyết định
    /// </summary>
    public class ConditionDSS
    {
        /// <summary>
        /// Tên thuộc tính
        /// </summary>
        public string Property { get; set; }
        /// <summary>
        /// Trạng thái: 0-thấp, 1-cao
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Trọng số thuộc tính
        /// </summary>
        public double Weight { get; set; }
       
    }
}
