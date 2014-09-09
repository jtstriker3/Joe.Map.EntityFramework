using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Joe.Map.EntityFramework.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            new Initialize();
            Joe.Initialize.Init.RunInitFunctions();
            var includeFunction = Joe.Map.ExpressionHelpers.GetIncludeMethod(typeof(String));
        }
    }
}
