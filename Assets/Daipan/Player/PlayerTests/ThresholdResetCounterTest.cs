using System.Collections;
using System.Collections.Generic;
using Daipan.Player.LevelDesign.Interfaces;
using NUnit.Framework;
using UnityEngine;
using Daipan.Player.Scripts;

public class ThresholdResetCounterTest
{
    [Test]
    public void PlayerAttackedCounterTestWith10()
    {
        // Arrange
        var counter = new ThresholdResetCounter(10);

        // Act
        for(int i = 0; i < 10; i++)
        {
            counter.CountUp();
        }

        // Assert
        Assert.IsTrue(counter.IsOverThreshold);

        // Act
        counter.CountUp();

        // Assert
        Assert.IsFalse(counter.IsOverThreshold);
    }
    
    [Test]
    public void PlayerAttackedCounterTestWith5()
    {
        // Arrange
        var counter = new ThresholdResetCounter(10);

        // Act
        for(int i = 0; i < 9; i++)
        {
            counter.CountUp();
        }

        // Assert
        Assert.IsFalse(counter.IsOverThreshold);

        // Act
        counter.CountUp();

        // Assert
        Assert.IsTrue(counter.IsOverThreshold);
        
        // Act
        counter.CountUp();
        
        // Assert
        Assert.IsFalse(counter.IsOverThreshold);    
    }
}
