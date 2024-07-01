#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.Scripts;
using NUnit.Framework;

public class EnemyAttackTest
{
    // [Test]
    // public void IHpSetterShouldDecrease10WithAttack()
    // {
    //     // Arrange
    //     var player = new DummyPlayer();
    //     var enemyParamData = new MockEnemyParamData(); 
    //     var enemyAttack = new EnemyAttack(enemyParamData);
    //
    //     // Act
    //     enemyAttack.Attack(player);
    //
    //     // Assert
    //     Assert.AreEqual(90, player.CurrentHp);
    // }
    //
    [Test]
    public void PlayerHpShouldDecrease10WithAttack()
    {
        // Arrange
        var playerHp = new PlayerHpNew(100); 

        // Act
        var postAttackPlayerHp = EnemyAttackModule.Attack(new MockEnemyParamData(), playerHp);

        // Assert
        Assert.AreEqual(90, postAttackPlayerHp.Hp); 
    }
    
    class MockEnemyParamData : IEnemyParamData
    {
        public EnemyEnum GetEnemyEnum() => EnemyEnum.None;
        public int GetAttackAmount() => 10;
    }
}