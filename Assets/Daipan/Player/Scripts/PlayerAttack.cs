#nullable enable
using Daipan.Battle.interfaces;
using Daipan.LevelDesign.Player.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttack
    {
        readonly PlayerParamData _playerParamData;

        public PlayerAttack(PlayerParamData playerParamData)
        {
            _playerParamData = playerParamData;
        }

        public void Attack(IHpSetter hpSetter)
        {
            Debug.Log("Attack Enemy");
            hpSetter.CurrentHp -= _playerParamData.GetAttack();
            Debug.Log($"_playerParamData.GetAttack() = {_playerParamData.GetAttack()}");
        }
    }
}