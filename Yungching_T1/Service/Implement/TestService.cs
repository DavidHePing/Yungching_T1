using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yungching_T1.Service.Interface;

namespace Yungching_T1.Service.Implement
{
    public class TestService : ITestService
    {
        public bool IsValidLogFileName(string fileName)
        {
            if (fileName.EndsWith(".SLF", StringComparison.CurrentCultureIgnoreCase)) // INCORRECT HERE
            {
                return true;
            }
            return false;
        }

        public void LogError(string message)
        {           
            throw new NotImplementedException();
        }
    }
}
