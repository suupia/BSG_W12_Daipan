using UnityEngine;

namespace Daipan.LevelDesign.Enemy.Scripts;

public class EnemyDefeatConfig
{
    readonly EnemyDefeatParamManager _enemyDefeatParamManager;
    readonly EnemyDefeatPositionMono _enemyDefeatPositionMono;

    public EnemyDefeatConfig(
        EnemyDefeatParamManager enemyDefeatParamManager,
        EnemyDefeatPositionMono enemyDefeatPositionMono)
    {
        _enemyDefeatParamManager = enemyDefeatParamManager;
        _enemyDefeatPositionMono = enemyDefeatPositionMono;
    }

    public int GetSlowDefeatThreshold()
    {
        return _enemyDefeatParamManager.enemySlowDefeatParam.slowDefeatThreshold;
    }

    public Vector3 GetEnemyDefeatQuickPosition()
    {
        return _enemyDefeatPositionMono.enemyDefeatQuickPosition.position;
    }

    public Vector3 GetEnemyDefeatSlowPosition()
    {
        return _enemyDefeatPositionMono.enemyDefeatSlowPosition.position;
    }
}