using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Core.entity
{
    public class BreathMachine : BaseEntity
    {
        public Guid BreathMachineId { get; set; }
        public string BreathMachineCode { get; set; }
        public string BreathMachineName { get; set; }
        public int Amount { get; set; }
    }
}
