#nullable enable
using System.Collections.Generic;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using NUnit.Framework;


public sealed class ComboTests
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
        // Arrange
        var viewerNumber = new ViewerNumber();
        var comboCounter = new ComboCounter(new ComboParamContainerMock(), viewerNumber);
        viewerNumber.IncreaseViewer(1000);

        // Act 1
        for (var i = 0; i < 10; i++) comboCounter.IncreaseCombo();
        // Assert 1
        Assert.AreEqual(10, comboCounter.ComboCount);
        Assert.AreEqual(1100, viewerNumber.Number);

        // Act 2
        for (var i = 0; i < 10; i++) comboCounter.IncreaseCombo();
        // Assert 2
        Assert.AreEqual(20, comboCounter.ComboCount);
        Assert.AreEqual(1650, viewerNumber.Number);

        // Act 3
        comboCounter.ResetCombo();
        for (var i = 0; i < 10; i++) comboCounter.IncreaseCombo();
        // Assert 3
        Assert.AreEqual(10, comboCounter.ComboCount);
        Assert.AreEqual(1815, viewerNumber.Number);
    }

    sealed class ComboParamContainerMock : IComboParamContainer
    {
        public IEnumerable<ComboParam> GetComboParams()
        {
            return new List<ComboParam>
            {
                new()
                {
                    comboThreshold = 10,
                    comboMultiplier = 1.1
                },
                new()
                {
                    comboThreshold = 20,
                    comboMultiplier = 1.5
                }
            };
        }
    }
}