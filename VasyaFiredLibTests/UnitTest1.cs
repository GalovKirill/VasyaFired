using System.Runtime.InteropServices;
using NUnit.Framework;
using VasyaFiredLib;

namespace VasyaFiredLibTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var class1 = new ConditionRule();
            Assert.AreEqual(28, Marshal.SizeOf(class1));
        }
    }
}