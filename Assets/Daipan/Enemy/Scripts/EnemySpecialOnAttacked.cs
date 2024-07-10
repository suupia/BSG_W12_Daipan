#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;

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

            hp.Decrease(playerParamData.GetAttack());
        }


        static bool IsSameColor(EnemyEnum enemyEnum, PlayerColor playerColor)
        {
            return enemyEnum switch
            {
                EnemyEnum.Red => playerColor == PlayerColor.Red,
                EnemyEnum.Blue => playerColor == PlayerColor.Blue,
                EnemyEnum.Yellow => playerColor == PlayerColor.Yellow,
                _ => false
            };
        }
    }
}