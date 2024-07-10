#nullable enable
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemySpecialOnAttack
    {
        readonly IrritatedValue _irritatedValue;

        public EnemySpecialOnAttack(
           IrritatedValue irritatedValue 
            )
        {
           _irritatedValue = irritatedValue; 
        }
        

        public Hp OnAttacked(Hp hp, EnemyEnum enemyEnum, IPlayerParamData playerParamData )
        {
            if (!IsSameColor(enemyEnum, playerParamData.PlayerEnum()))
            {
                // 違う色のときはイライラゲージを増やす
                _irritatedValue.IncreaseValue(playerParamData.GetAttack());
            }
            return PlayerAttackModule.Attack(hp, playerParamData);
            
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