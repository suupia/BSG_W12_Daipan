using System.Collections;
using System.Collections.Generic;
using Daipan.Player.LevelDesign.Interfaces;
using NUnit.Framework;
using UnityEngine;
using Daipan.Player.Scripts;

public class PlayerAttackedCounterTest
{
    [Test]
    public void PlayerAttackedCounterTestWith10()
    {
        // Arrange
        var counter = new PlayerAttackedCounter(new MockPlayerHpParamData());

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
    
    class MockPlayerHpParamData : IPlayerHpParamData 
    {
        public int GetCurrentHp() => 100;
        public int SetCurrentHp(int value) => 100;
        public int GetAntiCommentThreshold() => 10;
    }
}
