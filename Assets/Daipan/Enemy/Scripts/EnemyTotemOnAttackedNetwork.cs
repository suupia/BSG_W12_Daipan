#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Comment.Interfaces;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Sound.MonoScripts;
using Daipan.Stream.Interfaces;
using Daipan.Stream.Scripts;
using Daipna.StreamerNet.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyTotemOnAttackedNetwork : IEnemyOnAttacked, IDisposable
    {
        const double AllowableSec = 0.15f;
        readonly SamePressChecker _samePressChecker;
        readonly List<PlayerColor> _canAttackPlayers;

        public EnemyTotemOnAttackedNetwork(
            IEnemyMono enemyMono
            , ComboCounter comboCounter
            , ICommentSpawner commentSpawner
            , IPlayerAntiCommentParamData playerAntiCommentParamData
            , WaveState waveState
            , List<PlayerColor> canAttackPlayers
            , EnemyClusterNetwork enemyClusterNetwork
            , RpcReceiverNetWrapper rpcReceiverNetWrapper
        )
        {
            _samePressChecker = new SamePressChecker(AllowableSec, canAttackPlayers.Count
                , () =>
                {
                    comboCounter.IncreaseCombo();
                    SoundManager.Instance?.PlaySe(SeEnum.Attack);
                }, () =>
                {
                    comboCounter.ResetCombo();
                    rpcReceiverNetWrapper.RpcReceiverNet.SetFeverRPC(enemyClusterNetwork.GetPlayerRef(enemyMono));
                    SoundManager.Instance?.PlaySe(SeEnum.AttackDeflect);
                });
            _canAttackPlayers = canAttackPlayers;
        }

        public Hp OnAttacked(Hp hp, IPlayerParamData playerParamData)
        {
            Debug.Log($"OnAttacked hp: {hp.Value} playerParamData: {playerParamData}");
            var attackPlayer = playerParamData.PlayerEnum();
            var index = _canAttackPlayers.IndexOf(attackPlayer);
            if (index == -1) return hp;
            _samePressChecker.SetOn(index);
            if (!_samePressChecker.IsAllOn()) return hp;
            return new Hp(hp.Value - playerParamData.GetAttack());
        }

        public void Dispose()
        {
            _samePressChecker.Dispose();
        }
    }
}