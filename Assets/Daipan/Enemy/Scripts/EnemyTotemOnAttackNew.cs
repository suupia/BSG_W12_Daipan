#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyTotemOnAttackNew
    {
        const double AllowableSec = 0.1f;
        readonly SamePressChecker _samePressChecker;
        readonly List<PlayerColor> _canAttackPlayers;

        public EnemyTotemOnAttackNew(List<PlayerColor> canAttackPlayers)
        {
           _samePressChecker = new SamePressChecker(AllowableSec, canAttackPlayers.Count); 
           _canAttackPlayers = canAttackPlayers;
        }


        public void OnAttacked(Hp hp, IPlayerParamData playerParamData)
        {
            Debug.Log($"OnAttacked hp: { hp } playerParamData: { playerParamData }");
            var attackedPlayer = playerParamData.PlayerEnum();
            var index = _canAttackPlayers.IndexOf(attackedPlayer);
            if (index == -1) return;
            _samePressChecker.SetOn(index);
            if (!_samePressChecker.IsAllOn()) return; 
            PlayerAttackModule.Attack(hp, playerParamData);
        }
        
    }

 
}