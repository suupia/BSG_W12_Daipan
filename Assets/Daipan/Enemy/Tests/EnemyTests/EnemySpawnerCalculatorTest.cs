using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EnemySpawnerCalculatorTest 
{
    [Test]
    public void NormalizeEnemySpawnRatioWithBossTest()
    {
        var ratio = new List<double> { 1.0f, 2.0f, 3.0f };
        float bossRatio = 40;
        var expected = new List<double> { 10.0f, 20.0f, 30.0f, 40.0f };

        List<double> actual = Daipan.Enemy.Scripts.EnemySpawnCalculator.NormalizeEnemySpawnRatioWithBoss(ratio, bossRatio);

        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void NormalizeEnemySpawnRatioWithBossTest2()
    {
        var ratio = new List<double> { 1.0f, 2.0f, 2.0f };
        float bossRatio = 50;
        var expected = new List<double> { 10.0f, 20.0f, 20.0f, 50.0f };

        List<double> actual = Daipan.Enemy.Scripts.EnemySpawnCalculator.NormalizeEnemySpawnRatioWithBoss(ratio, bossRatio);

        Assert.AreEqual(expected, actual);
    }
}
