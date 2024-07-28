#nullable enable

using Daipan.Enemy.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using NUnit.Framework;

public class EnemyNormalOnAttackedTest
{
    [Test]
    public void OnAttacked_ShouldDecreaseHpBy10_WhenCalled()
    {
        // Arrange
        var enemyNormalOnAttacked = new EnemyNormalOnAttacked();
        var hp = new Hp(100);
        var playerParamData = new MockPlayerParamData();
        
        // Act
        var afterHp = enemyNormalOnAttacked.OnAttacked(hp, playerParamData);
        
        // Assert
        Assert.AreEqual(90, afterHp.Value);
    }

    class MockPlayerParamData : IPlayerParamData
    {
        public UnityEngine.RuntimeAnimatorController? GetAnimator() => null;
        public PlayerColor PlayerEnum() => PlayerColor.None;
        public int GetAttack() => 10;
    }
}