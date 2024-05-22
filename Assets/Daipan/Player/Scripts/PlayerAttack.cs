#nullable enable
using Daipan.Battle.interfaces;
using Daipan.LevelDesign.Player.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttack
    {
        readonly PlayerParamsServer _playerParamsServer;

        [Inject]
        public PlayerAttack(PlayerParamsServer playerParamsServer)
        {
            _playerParamsServer = playerParamsServer;
        }

        public void Attack(int attackIndex)
        {
            Debug.Log($"Temp PlayerAttack Attack({attackIndex}) , Temp AttackPower : {_playerParamsServer.GetAttackAmount().AttackAmount}");
        }

        public void WAttack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _playerParamsServer.GetAttackAmount().WAttackAmount;
        }

        public void AAttack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _playerParamsServer.GetAttackAmount().AAttackAmount;
        }

        public void SAttack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _playerParamsServer.GetAttackAmount().SAttackAmount;
        }
    }
}