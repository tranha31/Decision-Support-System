using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Core.entity
{
    public class ServiceResult
    {
        public object Data { get; set; }
        public string DevMsg { get; set; }
        public string UserMsg { get; set; }
        public int Code { get; set; }

        public Guid TraceId { get; set; }
    }
}
