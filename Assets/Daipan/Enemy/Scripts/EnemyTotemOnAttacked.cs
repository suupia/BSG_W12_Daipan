#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Sound.Interfaces;
using Daipan.Sound.MonoScripts;
using Daipan.Stream.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyTotemOnAttacked : IEnemyOnAttacked, IDisposable
    {
        const double AllowableSec = 0.15f;
        readonly SamePressChecker _samePressChecker;
        readonly List<PlayerColor> _canAttackPlayers;

        public EnemyTotemOnAttacked(
            ComboCounter comboCounter
            , CommentSpawner commentSpawner
            , IPlayerAntiCommentParamData playerAntiCommentParamData
            , WaveState waveState
            , List<PlayerColor> canAttackPlayers
            , ISoundManager soundManager
        )
        {
            _samePressChecker = new SamePressChecker(AllowableSec, canAttackPlayers.Count
                ,  comboCounter.IncreaseCombo, () =>
                {
                    comboCounter.ResetCombo();
                    var spawnPercent =
                        playerAntiCommentParamData.GetAntiCommentPercentOnMissAttacks(waveState.CurrentWaveIndex);
                    if (spawnPercent / 100f > UnityEngine.Random.value)
                        commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
                    soundManager.PlaySe(SeEnum.AttackDeflect);
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