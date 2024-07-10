#nullable enable
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Scripts;

public class EnemyOnAttackedBuilder
{
    readonly IrritatedValue _irritatedValue;
    readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;

    public EnemyOnAttackedBuilder(
        IrritatedValue irritatedValue
        , EnemyLevelDesignParamData enemyLevelDesignParamData
    )
    {
        _irritatedValue = irritatedValue;
        _enemyLevelDesignParamData = enemyLevelDesignParamData;
    }

    public IEnemyOnAttacked SwitchEnemyOnAttacked(EnemyEnum enemyEnum)
    {
        return enemyEnum switch
        {
            EnemyEnum.Special =>  new EnemySpecialOnAttacked(enemyEnum, _irritatedValue, _enemyLevelDesignParamData),
            EnemyEnum.Totem2 => BuildTotemOnAttack(enemyEnum),
            EnemyEnum.Totem3 => BuildTotemOnAttack(enemyEnum),
            _ => new EnemyNormalOnAttacked(), 
        };
    }
    
    EnemyTotemOnAttacked BuildTotemOnAttack(EnemyEnum enemyEnum)
    {
        return new EnemyTotemOnAttacked(new List<PlayerColor>() { PlayerColor.Red, PlayerColor.Blue });
    }
}