#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Enemy.Scripts;
using Daipan.InputSerial.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;
using Daipan.Comment.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using R3;

namespace Daipan.Player.MonoScripts
{
    public sealed class PlayerMono : MonoBehaviour
    {
        [SerializeField] List<AbstractPlayerViewMono?> playerViewMonos = new();
        InputSerialManager _inputSerialManager = null!;
        CommentSpawner _commentSpawner = null!;
        PlayerAttackedCounter _attackedCounterForAntiComment = null!;
        IPlayerHpParamData _playerHpParamData = null!;
        IAttackExecutor _attackExecutor = null!;
        public Hp Hp { get; set; } = null!;

        public void Update()
        {
            if (_inputSerialManager.GetButtonRed())
            {
                Debug.Log("Wが押されたよ");
                _attackExecutor.FireAttackEffect(this,PlayerColor.Red);
            }

            if (_inputSerialManager.GetButtonBlue())
            {
                Debug.Log("Aが押されたよ");
                _attackExecutor.FireAttackEffect(this,PlayerColor.Blue);
            }

            if (_inputSerialManager.GetButtonYellow())
            {
                Debug.Log("Sが押されたよ");
                _attackExecutor.FireAttackEffect(this,PlayerColor.Yellow);
            }

            // todo : 攻撃やHPの状況に応じて、AbstractPlayerViewMonoのメソッドを呼ぶ
            foreach (var playerViewMono in playerViewMonos) playerViewMono?.Idle();
        }

        // void FireAttackEffect(PlayerColor playerColor)
        // {
        //     // todo : AttackEffectの生成位置は仕様によって変更する。
        //     // とりあえずは、x座標は同じ色のプレイヤーのx座標、y座標はtargetEnemyのy座標に生成する
        //     var sameColorPlayerViewMono = playerViewMonos
        //         .FirstOrDefault(playerViewMono => playerViewMono?.playerColor == playerColor);
        //     if (sameColorPlayerViewMono == null)
        //     {
        //         Debug.LogWarning($"同じ色のプレイヤーがいません");
        //         return;
        //     }
        //
        //     var spawnPosition = _playerAttackEffectPointData.GetAttackEffectSpawnedPoint();
        //     _playerAttackEffectSpawner.SpawnEffect(this, playerViewMonos, playerColor, spawnPosition,
        //         Quaternion.identity);
        // }

        [Inject]
        public void Initialize(
            PlayerAttackedCounter playerAttackedCounter
            , InputSerialManager inputSerialManager
            , IrritatedValue irritatedValue
            , CommentSpawner commentSpawner
            , WaveState waveState
            , IAttackExecutor attackExecutor
            , IPlayerHpParamData playerHpParamData
        )
        {
            _commentSpawner = commentSpawner;
            _playerHpParamData = playerHpParamData;
            Observable.EveryValueChanged(waveState, x => x.CurrentWave)
                .Subscribe(_ => Hp = new Hp(playerHpParamData.GetMaxHp()))
                .AddTo(this);

            _attackedCounterForAntiComment = playerAttackedCounter;

            EnemyAttackModule.AttackEvent += (sender, args) =>
            {
                // Domain
                irritatedValue.IncreaseValue(args.DamageValue);

                // AntiComment
                _attackedCounterForAntiComment.CountUp();
                if (_attackedCounterForAntiComment.IsOverThreshold)
                    _commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
                Debug.Log($"isThreshold{_attackedCounterForAntiComment.IsOverThreshold}");

                // View
                foreach (var playerViewMono in playerViewMonos)
                {
                    if (playerViewMono == null) continue;
                    if (PlayerAttackModule.GetTargetEnemyEnum(playerViewMono.playerColor).Contains(args.EnemyEnum))
                        playerViewMono.Damage();
                }
            };
            
            attackExecutor.SetPlayerViewMonos(playerViewMonos);
            _attackExecutor = attackExecutor;
            
            _inputSerialManager = inputSerialManager;
            
        }

        public int MaxHp => _playerHpParamData.GetMaxHp();
    }
}