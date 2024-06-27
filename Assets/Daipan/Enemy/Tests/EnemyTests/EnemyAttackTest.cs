#nullable enable
using System;
using Daipan.Battle.interfaces;
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
        var enemyParamData = new EnemyParamData()
        {
            GetEnemyEnum = () => EnemyEnum.None,
            GetAttackAmount = () => 10
        };
        var enemyAttack = new EnemyAttack(enemyParamData);

        // Act
        enemyAttack.Attack(player);

        // Assert
        Assert.AreEqual(90, player.CurrentHp);
    }
    class DummyPlayer : IPlayerHp
    {
        public event EventHandler<DamageArgs>? OnDamage;
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
}