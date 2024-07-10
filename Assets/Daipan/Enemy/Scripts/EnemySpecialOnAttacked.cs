#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemySpecialOnAttacked : IEnemyOnAttacked
    {
        readonly EnemyEnum _enemyEnum;
        readonly IrritatedValue _irritatedValue;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;

        public EnemySpecialOnAttacked(
            EnemyEnum enemyEnum
           , IrritatedValue irritatedValue 
           , EnemyLevelDesignParamData enemyLevelDesignParamData
            )
        {
            _enemyEnum = enemyEnum;
           _irritatedValue = irritatedValue;
           _enemyLevelDesignParamData = enemyLevelDesignParamData;
        }


        public void OnAttacked(Hp hp, IPlayerParamData playerParamData)
        {
            if (!IsSameColor(_enemyEnum, playerParamData.PlayerEnum()))
            {
                // 違う色のときはイライラゲージを増やす
                _irritatedValue.IncreaseValue(_enemyLevelDesignParamData
                    .GetIncreaseIrritationGaugeOnSpecialEnemyKill());
            }
            
            // todo : IsSmaColorは現時点だと常にfalse。なぜなら、enemyEnumはSpecialが入っているから。
            if (!IsSameColor(_enemyEnum, playerParamData.PlayerEnum())) Debug.Log("IsSameColor is false");

            hp.Decrease(playerParamData.GetAttack());
        }


        static bool IsSameColor(EnemyEnum enemyEnum, PlayerColor playerColor)
        {
            return enemyEnum switch
            {
                EnemyEnum.SpecialRed => playerColor == PlayerColor.Red,
                EnemyEnum.SpecialBlue => playerColor == PlayerColor.Blue,
                EnemyEnum.SpecialYellow => playerColor == PlayerColor.Yellow,
                _ => false
            };
        }
    }
}