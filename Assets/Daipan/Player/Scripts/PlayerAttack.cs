#nullable enable
using Daipan.Battle.interfaces;
using UnityEngine;
using VContainer;

namespace Daipan.Stream.Scripts
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

        }
        public void AAttack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _parameter.AttackAmount;
        }
        public void SAttack(int S)
        {

        }
    }
}