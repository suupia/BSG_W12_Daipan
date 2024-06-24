using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Daipan.Player.Scripts;

public class PlayerAttackedCounterTest
{
    [Test]
    public void PlayerAttackedCounterTestWith10()
    {
        // Arrange
        var counter = new PlayerAttackedCounter(10);

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
}
