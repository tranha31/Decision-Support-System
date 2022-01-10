using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Core.enums
{
    public class Enum
    {
        /// <summary>
        /// Mã trạng thái service
        /// </summary>
        public enum DSSEnum
        {
            Success = 100,
            Valid = 200,
            NotValid = 300,
            ErrorSever = 500,
            Duplicate = 400,
            NotFound = 404,

        }
    }
}
