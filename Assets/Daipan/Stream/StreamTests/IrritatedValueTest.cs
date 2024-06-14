#nullable enable
using Daipan.Stream.Scripts;
using NUnit.Framework;

namespace Daipan.Stream.StreamTests
{
    public class IrritatedValueTest
    {
        [Test]
        public void IncrementTest()
        {
            var irritatedValue = new IrritatedValue(100, new IrritatedParams());
            irritatedValue.IncreaseValue(10);
            Assert.AreEqual(10, irritatedValue.Value);
            irritatedValue.IncreaseValue(100);
            Assert.AreEqual(100, irritatedValue.Value);
        }
        
        [Test]
        public void DecrementTest()
        {
            var irritatedValue = new IrritatedValue(100, new IrritatedParams());
            irritatedValue.IncreaseValue(100);
            irritatedValue.DecreaseValue(10);
            Assert.AreEqual(90, irritatedValue.Value);
            irritatedValue.DecreaseValue(100);
            Assert.AreEqual(0, irritatedValue.Value);
        }
    }
}