#nullable enable
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Player.MonoScripts;

namespace Daipan.Enemy.Scripts;

public class EnemyOnAttackedBuilder
{
    readonly EnemyNormalOnAttacked _enemyNormalOnAttacked;
    readonly EnemySpecialOnAttacked _enemySpecialOnAttacked;

    
    public IEnemyOnAttacked SwitchEnemyOnAttacked(EnemyEnum enemyEnum)
    {
        return enemyEnum switch
        {
            EnemyEnum.Special =>  _enemySpecialOnAttacked,
            EnemyEnum.Totem2 => BuildTotemOnAttack(enemyEnum),
            EnemyEnum.Totem3 => BuildTotemOnAttack(enemyEnum),
            _ => _enemyNormalOnAttacked
        };
    }
    
    EnemyTotemOnAttackNew BuildTotemOnAttack(EnemyEnum enemyEnum)
    {
        return new EnemyTotemOnAttackNew(new List<PlayerColor>() { PlayerColor.Red, PlayerColor.Blue });
    }
}