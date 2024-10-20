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
        readonly IrritatedGaugeValue _irritatedGaugeValue;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;

        public EnemySpecialOnAttacked(
            EnemyEnum enemyEnum
            , IrritatedGaugeValue irritatedGaugeValue
            , EnemyLevelDesignParamData enemyLevelDesignParamData
        )
        {
            _enemyEnum = enemyEnum;
            _irritatedGaugeValue = irritatedGaugeValue;
            _enemyLevelDesignParamData = enemyLevelDesignParamData;
        }

        public Hp OnAttacked(Hp hp, IPlayerParamData playerParamData)
        {
            var afterHp = new Hp(hp.Value - playerParamData.GetAttack());
            if (!IsSameColor(_enemyEnum, playerParamData.PlayerEnum()))
            {
                Debug.Log(
                    $"_enemyEnum: {_enemyEnum}, playerParamData.PlayerEnum(): {playerParamData.PlayerEnum()} IsSameColor: {IsSameColor(_enemyEnum, playerParamData.PlayerEnum())}");

                // 違う色のときに倒したのなら、イライラゲージを増やす
                if (afterHp.Value <= 0)
                    _irritatedGaugeValue.IncreaseValue(_enemyLevelDesignParamData
                        .GetIncreaseIrritationGaugeOnSpecialEnemyKill());

            }

            return afterHp;
        }

        public static bool IsSameColor(EnemyEnum enemyEnum, PlayerColor playerColor)
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