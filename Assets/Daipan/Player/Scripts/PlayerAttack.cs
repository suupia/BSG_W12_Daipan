#nullable enable
using UnityEngine;
using VContainer;

namespace Daipan.Player.Scripts
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
        public void Attack()
        {
            Debug.Log($"Temp PlayerAttack Attack() , Temp AttackPower : {_parameter.AttackAmount}");
        }
    }
}