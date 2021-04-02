using NUnit.Framework;
using Yungching_T1.Service.Implement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yungching_T1.Service.Implement.Tests
{
    [TestFixture()]
    public class TestServiceTests
    {
        private TestService testService;

        [SetUp]
        public void Set()
        {
            testService = new TestService();
        }

        [Test()]
        public void IsValidFileName_字尾不包含SLF_回傳False()
        {
            bool result = testService.IsValidLogFileName("filewithbadextension.foo");
            Assert.False(result);
            result = testService.IsValidLogFileName("");
            Assert.False(result);
        }

        [Test()]
        public void IsValidFileName_字尾包含SLF_回傳True()
        {
            bool result = testService.IsValidLogFileName("filewithbadextension.slf");
            Assert.True(result);
            result = testService.IsValidLogFileName("filewithbadextension.SLF");
            Assert.True(result);

        }

        [Test()]
        public void IsValidFileName_檔案null_Throws()
        {
            var ex = Assert.Catch<Exception>(() => testService.IsValidLogFileName(null));
            Console.WriteLine(ex.Message);
            StringAssert.Contains("Object reference not set to an instance of an object", ex.Message);
        }
    }

    
}