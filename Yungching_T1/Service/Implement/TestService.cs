using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yungching_T1.Service.Implement
{
    public class TestService
    {
        public bool IsValidLogFileName(string fileName)
        {
            if (fileName.EndsWith(".SLF", StringComparison.CurrentCultureIgnoreCase)) // INCORRECT HERE
            {
                return true;
            }
            return false;
        }
    }
}
