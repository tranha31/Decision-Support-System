using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Core.entity
{
    public class TestKit : BaseEntity
    {
        public Guid TestKitId { get; set; }
        public string TestKitCode { get; set; }
        public string TestKitName { get; set; }
        public int Amount { get; set; }
    }
}
