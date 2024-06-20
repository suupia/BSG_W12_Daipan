#nullable enable
using Daipan.Player.Scripts;
using NUnit.Framework;

namespace Daipan.Player.Tests.ComboTests
{
    public class ComboTests
    {
        [Test]
        public void IncreaseComboTest()
        {
            var comboCounter = new ComboCounter();
            comboCounter.IncreaseCombo();
            Assert.AreEqual(1, comboCounter.ComboCount);
            comboCounter.IncreaseCombo();
            Assert.AreEqual(2, comboCounter.ComboCount);
        }
        
        [Test]
        public void ResetComboTest()
        {
            var comboCounter = new ComboCounter();
            comboCounter.IncreaseCombo();
            comboCounter.ResetCombo();
            Assert.AreEqual(0, comboCounter.ComboCount);
            
            comboCounter.IncreaseCombo();
            comboCounter.IncreaseCombo();
            comboCounter.ResetCombo();
            Assert.AreEqual(0, comboCounter.ComboCount);
            
        }
    }
}