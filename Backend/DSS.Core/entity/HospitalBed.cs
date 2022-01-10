using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Core.entity
{
    public class HospitalBed : BaseEntity
    {
        public Guid HospitalBedId { get; set; }
        public string HospitalBedCode { get; set; }
        public int Amount { get; set; }
    }
}
