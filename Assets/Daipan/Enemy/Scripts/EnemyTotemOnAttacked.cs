#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyTotemOnAttacked : IEnemyOnAttacked
    {
        const double AllowableSec = 0.1f;
        readonly SamePressChecker _samePressChecker;
        readonly List<PlayerColor> _canAttackPlayers;

        public EnemyTotemOnAttacked(List<PlayerColor> canAttackPlayers)
        {
           _samePressChecker = new SamePressChecker(AllowableSec, canAttackPlayers.Count); 
           _canAttackPlayers = canAttackPlayers;
        }


        public Hp OnAttacked(Hp hp, IPlayerParamData playerParamData)
        {
            Debug.Log($"OnAttacked hp: { hp.Value } playerParamData: { playerParamData }");
            var attackedPlayer = playerParamData.PlayerEnum();
            var index = _canAttackPlayers.IndexOf(attackedPlayer);
            if (index == -1) return hp;
            _samePressChecker.SetOn(index);
            if (!_samePressChecker.IsAllOn()) return hp;
            return new Hp(hp.Value - playerParamData.GetAttack());
        }
        
    }

 
}