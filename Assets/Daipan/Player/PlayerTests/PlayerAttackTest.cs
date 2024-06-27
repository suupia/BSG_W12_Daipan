using System.Collections;
using System.Collections.Generic;
using Daipan.Battle.interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;
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
        var playerParam = new MockPlayerParamData();
        var playerAttack = new PlayerAttack(playerParam);

        // Act
        playerAttack.Attack(enemy);

        // Assert
        Assert.AreEqual(90, enemy.CurrentHp);
    }
    class MockPlayerParamData : IPlayerParamData
    {
        public int GetAttack()
        {
            return 10;
        }

        public PlayerColor PlayerEnum()
        {
            return PlayerColor.None;
        }

        public RuntimeAnimatorController GetAnimator()
        {
            return null;
        }
    }
    class DummyEnemy : IHpSetter
    {
        public int CurrentHp { get; set; }
    }
}