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
        EnemySpecialViewMono? _enemySpecialViewMono;

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

        public void SetView(AbstractEnemyViewMono? enemyViewMono)
        {
            // [Precondition]
            if (enemyViewMono is EnemySpecialViewMono specialViewMono)
                _enemySpecialViewMono = specialViewMono;
            
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
                    _irritatedValue.IncreaseValue(_enemyLevelDesignParamData
                        .GetIncreaseIrritationGaugeOnSpecialEnemyKill());

            }

            return afterHp;
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