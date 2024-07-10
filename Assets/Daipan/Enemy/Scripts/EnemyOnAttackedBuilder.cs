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
        if(enemyEnum.IsSpecial() == true) return new EnemySpecialOnAttacked(enemyEnum, _irritatedValue, _enemyLevelDesignParamData);
        if(enemyEnum == EnemyEnum.Totem2 || enemyEnum == EnemyEnum.Totem3) return BuildTotemOnAttack(enemyEnum);
        return new EnemyNormalOnAttacked();
    }
    
    EnemyTotemOnAttacked BuildTotemOnAttack(EnemyEnum enemyEnum)
    { 
        // todo: 実装する
        return new EnemyTotemOnAttacked(new List<PlayerColor>() { PlayerColor.Red, PlayerColor.Blue });
    }
}