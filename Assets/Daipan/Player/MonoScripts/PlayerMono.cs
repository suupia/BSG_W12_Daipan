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
        EnemyCluster _enemyCluster = null!;
        InputSerialManager _inputSerialManager = null!;
        PlayerAttackEffectSpawner _playerAttackEffectSpawner = null!;
        CommentSpawner _commentSpawner = null!;
        PlayerAttackedCounter _attackedCounterForAntiComment = null!;
        PlayerAttackEffectPointData _playerAttackEffectPointData = null!;
        IPlayerHpParamData _playerHpParamData = null!;
        public Hp Hp { get; set; } = null!;
        public void Update()
        {
            if (_inputSerialManager.GetButtonRed())
            {
                Debug.Log("Wが押されたよ");
                FireAttackEffect(PlayerColor.Red);
            }

            if (_inputSerialManager.GetButtonBlue())
            {
                Debug.Log("Aが押されたよ");
                FireAttackEffect(PlayerColor.Blue);
            }

            if (_inputSerialManager.GetButtonYellow())
            {
                Debug.Log("Sが押されたよ");
                FireAttackEffect(PlayerColor.Yellow);
            }

            // todo : 攻撃やHPの状況に応じて、AbstractPlayerViewMonoのメソッドを呼ぶ
            foreach (var playerViewMono in playerViewMonos) playerViewMono?.Idle();
        }

        void FireAttackEffect(PlayerColor playerColor)
        {
            // 一番近い敵を取得し、そこに向かってAttackEffectを発射する（敵がいなくても生成する）
            var targetEnemy = _enemyCluster.NearestEnemy(transform.position);

            // todo : AttackEffectの生成位置は仕様によって変更する。
            // とりあえずは、x座標は同じ色のプレイヤーのx座標、y座標はtargetEnemyのy座標に生成する
            var sameColorPlayerViewMono = playerViewMonos
                .FirstOrDefault(playerViewMono => playerViewMono?.playerColor == playerColor);
            if (sameColorPlayerViewMono == null)
            {
                Debug.LogWarning($"同じ色のプレイヤーがいません");
                return;
            }

            var spawnPosition = _playerAttackEffectPointData.GetAttackEffectSpawnedPoint();
            _playerAttackEffectSpawner.SpawnEffect(this, playerViewMonos,playerColor, spawnPosition, Quaternion.identity);
        }

        [Inject]
        public void Initialize(
            EnemyCluster enemyCluster
            ,PlayerAttackedCounter playerAttackedCounter
            ,InputSerialManager inputSerialManager
            ,PlayerAttackEffectSpawner playerAttackEffectSpawner
            ,IrritatedValue irritatedValue
            ,CommentSpawner commentSpawner
            ,PlayerAttackEffectPointData playerAttackEffectPointData
            ,WaveState waveState
            ,IPlayerHpParamData playerHpParamData
        )
        {
            _enemyCluster = enemyCluster;
            _commentSpawner = commentSpawner;
            _playerAttackEffectPointData = playerAttackEffectPointData;
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
                {
                    _commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
                }
                Debug.Log($"isThreshold{_attackedCounterForAntiComment.IsOverThreshold}");

                // View
                foreach (var playerViewMono in playerViewMonos)
                {
                    if (playerViewMono == null) continue;
                    if (PlayerAttackModule.GetTargetEnemyEnum(playerViewMono.playerColor).Contains(args.EnemyEnum))
                        playerViewMono.Damage();
                }
            };

            _inputSerialManager = inputSerialManager;
            _playerAttackEffectSpawner = playerAttackEffectSpawner;
        }

        public int MaxHp => _playerHpParamData.GetMaxHp();
    }
}