#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.Scripts;
using NUnit.Framework;

public sealed class EnemyAttackTest
{
    [Test]
    public void PlayerHpShouldDecrease10WithAttack()
    {
        // Arrange
        var playerHp = new Hp(100); 

        // Act
        var postAttackPlayerHp = EnemyAttackModule.Attack(new MockEnemyParamData(), playerHp);

        // Assert
        Assert.AreEqual(90, postAttackPlayerHp.Value); 
    }
    
    sealed class MockEnemyParamData : IEnemyParamData
    {
        public EnemyEnum GetEnemyEnum() => EnemyEnum.None;
        public int GetAttackAmount() => 10;
    }
}