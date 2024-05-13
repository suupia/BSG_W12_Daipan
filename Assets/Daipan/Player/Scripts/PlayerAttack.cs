#nullable enable
using UnityEngine;
using VContainer;

namespace Stream.Player.Scripts
{
    // このクラスにPlayerの攻撃の処理を追加していってください。
    public sealed class PlayerAttack
    {
        readonly PlayerAttackParameter _parameter;

        [Inject]
        public PlayerAttack(PlayerAttackParameter parameter)
        {
            _parameter = parameter;
        }

        public void Attack(int attackIndex)
        {
            Debug.Log($"Temp PlayerAttack Attack({attackIndex}) , Temp AttackPower : {_parameter.AttackAmount}");
        }

        public void WAttack(int W)
        {
            Debug.Log($"W={W}");
        }
        public void AAttack(int A)
        {
            Debug.Log($"A={A}");
        }
        public void SAttack(int S)
        {
            Debug.Log($"S={S}");
        }
    }
}