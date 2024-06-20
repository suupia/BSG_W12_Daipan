using System.Collections;
using System.Collections.Generic;
using Daipan.Battle.interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.Scripts;
using NUnit.Framework;
using UnityEngine;

public class PlayerAttackTest
{
    [Test]
    public void IHpSetterShouldDecrease10WithAttack()
    {
        // Arrange
        var enemy = new DummyEnemy() { CurrentHp = 100 };
        var playerParamDto = new PlayerParamData()
        {
            GetAttack = () => 10,
        };
        var playerAttack = new PlayerAttack(playerParamDto);

        // Act
        playerAttack.Attack(enemy);

        // Assert
        Assert.AreEqual(90, enemy.CurrentHp);
    }

    class DummyEnemy : IHpSetter
    {
        public int CurrentHp { get; set; }
    }
}