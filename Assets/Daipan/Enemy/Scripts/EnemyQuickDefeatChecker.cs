using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts;

public class EnemyQuickDefeatChecker
{
    readonly EnemyDefeatConfig _enemyDefeatConfig;

    public EnemyQuickDefeatChecker(EnemyDefeatConfig enemyDefeatConfig)
    {
        _enemyDefeatConfig = enemyDefeatConfig;
    }

    float QuickDefeatCoordinate => _enemyDefeatConfig.GetEnemyDefeatQuickPosition().x;

    public bool IsQuickDefeat(Vector3 currentPosition)
    {
        return QuickDefeatCoordinate < currentPosition.x;
    }
}