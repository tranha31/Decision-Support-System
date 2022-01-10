using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Core.entity
{
    public class Vaccine : BaseEntity
    {
        public Guid VaccineId { get; set; }
        public string VaccineCode { get; set; }
        public string VaccineName { get; set; }
        public int Amount { get; set; }
    }
}
