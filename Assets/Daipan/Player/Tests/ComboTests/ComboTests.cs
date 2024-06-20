#nullable enable
using System.Collections.Generic;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using NUnit.Framework;

namespace Daipan.Player.Tests.ComboTests
{
    public class ComboTests
    {
        [Test]
        public void IncreaseComboTest()
        {
            var comboCounter = new ComboCounter(new ComboParamContainerMock(), new ViewerNumber());
            comboCounter.IncreaseCombo();
            Assert.AreEqual(1, comboCounter.ComboCount);
            comboCounter.IncreaseCombo();
            Assert.AreEqual(2, comboCounter.ComboCount);
        }
        
        [Test]
        public void ResetComboTest()
        {
            var comboCounter = new ComboCounter(new ComboParamContainerMock(), new ViewerNumber());
            comboCounter.IncreaseCombo();
            comboCounter.ResetCombo();
            Assert.AreEqual(0, comboCounter.ComboCount);
            
            comboCounter.IncreaseCombo();
            comboCounter.IncreaseCombo();
            comboCounter.ResetCombo();
            Assert.AreEqual(0, comboCounter.ComboCount);
            
        }

        [Test]
        public void MultiplyViewersWithSequentialCombo()
        {
            var comboCounter = new ComboCounter(new ComboParamContainerMock(), new ViewerNumber());
            for (int i = 0; i < 10; i++)
            {
                comboCounter.IncreaseCombo();
            }
            
        }
        
        class ComboParamContainerMock : IComboParamContainer
        {
            public IEnumerable<ComboParam> GetComboParams()
            {
                return new List<ComboParam>
                {
                    new ComboParam
                    {
                        comboThreshold = 10,
                        comboMultiplier = 1.05
                    },
                    new ComboParam
                    {
                        comboThreshold = 20,
                        comboMultiplier = 1.1
                    }
                };
            }
        }
    }
}