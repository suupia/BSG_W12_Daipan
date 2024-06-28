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
    [Test]
    public void IHpSetterShouldDecrease10WithAttack()
    {
        // Arrange
        var player = new DummyPlayer();
        var enemyParamData = new MockEnemyParamData(); 
        var enemyAttack = new EnemyAttack(enemyParamData);

        // Act
        enemyAttack.Attack(player);

        // Assert
        Assert.AreEqual(90, player.CurrentHp);
    }
    class DummyPlayer : IPlayerHp
    {
        public DummyPlayer()
        {
            MaxHp = 100;
            CurrentHp = 100;
        }
        public int MaxHp { get; }
        public int CurrentHp { get; set; }
        public void SetHp(DamageArgs damageArgs)
        {
            CurrentHp -= damageArgs.DamageValue;
        } 
    }
    
    class MockEnemyParamData : IEnemyParamData
    {
        public EnemyEnum GetEnemyEnum() => EnemyEnum.None;
        public int GetAttackAmount() => 10;
    }
}