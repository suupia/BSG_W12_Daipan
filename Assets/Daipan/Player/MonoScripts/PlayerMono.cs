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
        IPlayerHpParamData _playerHpParamData = null!;
        IPlayerInput _playerInput = null!;
        public Hp Hp { get; set; } = null!;

        public void Update()
        {
            _playerInput.Update();

            // todo : 攻撃やHPの状況に応じて、AbstractPlayerViewMonoのメソッドを呼ぶ
            foreach (var playerViewMono in playerViewMonos) playerViewMono?.Idle();
        }


        [Inject]
        public void Initialize(
            PlayerAttackedCounter playerAttackedCounter
            , IrritatedValue irritatedValue
            , CommentSpawner commentSpawner
            , WaveState waveState
            , IPlayerHpParamData playerHpParamData
            , IPlayerInput playerInput
        )
        {
            _playerHpParamData = playerHpParamData;
            Observable.EveryValueChanged(waveState, x => x.CurrentWave)
                .Subscribe(_ => Hp = new Hp(playerHpParamData.GetMaxHp()))
                .AddTo(this);

            EnemyAttackModule.AttackEvent += (sender, args) =>
            {
                // Domain
                irritatedValue.IncreaseValue(args.DamageValue);

                // AntiComment
                playerAttackedCounter.CountUp();
                if (playerAttackedCounter.IsOverThreshold)
                    commentSpawner.SpawnCommentByType(CommentEnum.Spiky);

                // View
                foreach (var playerViewMono in playerViewMonos)
                {
                    if (playerViewMono == null) continue;
                    if (PlayerAttackModule.GetTargetEnemyEnum(playerViewMono.playerColor).Contains(args.EnemyEnum))
                        playerViewMono.Damage();
                }
            };
            playerInput.SetPlayerMono(this, playerViewMonos);
            _playerInput = playerInput;
        }

        public int MaxHp => _playerHpParamData.GetMaxHp();
    }
}