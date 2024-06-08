#nullable enable
using System.Collections.Generic;
using Daipan.Battle.interfaces;
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.InputSerial.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.LevelDesign.Player.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace Daipan.Player.MonoScripts
{
   public class PlayerMono : MonoBehaviour, IHpSetter
{
    [SerializeField] List<AbstractPlayerViewMono?> playerViewMonos = new();
    EnemyCluster _enemyCluster = null!;
    PlayerAttack _playerAttack = null!;
    PlayerHp _playerHp = null!;
    PlayerParamData _playerParamData = null!;
    InputSerialManager _inputSerialManager = null!;
    PlayerAttackEffectSpawner _playerAttackEffectSpawner = null!;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Wが押されたよ");
            AttackEnemyMono(PlayerColor.Red, EnemyEnum.W);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Aが押されたよ");
            AttackEnemyMono(PlayerColor.Blue, EnemyEnum.A);
        }

        if (Input.GetKeyDown(KeyCode.S) || _inputSerialManager.GetButtonBlue())
        {
            Debug.Log("Sが押されたよ");
            AttackEnemyMono(PlayerColor.Yellow, EnemyEnum.S);
        }

        // todo : 攻撃やHPの状況に応じて、AbstractPlayerViewMonoのメソッドを呼ぶ
        foreach (var playerViewMono in playerViewMonos) playerViewMono?.Idle();
    }

    void AttackEnemyMono(PlayerColor playerColor, EnemyEnum enemyEnum)
    {
        // そのレーンの敵を取得
        var enemyMono = _enemyCluster.NearestEnemy(enemyEnum, transform.position);
        // レーンの敵がいなければ、ボスを取得
        if (enemyMono == null) enemyMono = _enemyCluster.NearestEnemy(transform.position);
        if (enemyMono == null)
        {
            Debug.Log($"攻撃対象がいないよ");
            return;
        }

        if (enemyMono.EnemyEnum == enemyEnum || enemyMono.EnemyEnum == EnemyEnum.Boss)
        {
            Debug.Log($"EnemyType: {enemyMono.EnemyEnum}を攻撃");
            _playerAttack.Attack(enemyMono);
            
            // Animation
            foreach (var playerViewMono in playerViewMonos)
            {
                if (playerViewMono == null) continue;
                if (IsTargetEnemy(playerViewMono.playerColor, enemyEnum)) playerViewMono.Attack();
            }
        }
        else
        {
            Debug.Log($"攻撃対象が{enemyEnum}ではないよ");
        }
        
        
        var effect = _playerAttackEffectSpawner.SpawnEffect(transform.position, Quaternion.identity);
        effect.SetDomain(playerColor);
        effect.TargetPosition = () => enemyMono != null ? enemyMono.transform.position : null;
        

    }

    public void OnAttacked(EnemyEnum enemyEnum)
    {
        foreach (var playerViewMono in playerViewMonos)
        {
            if (playerViewMono == null) continue;
            if(IsTargetEnemy(playerViewMono.playerColor, enemyEnum)) playerViewMono.Damage();
        }
    }

    [Inject]
    public void Initialize(
        PlayerAttack playerAttack,
        EnemyCluster enemyCluster,
        PlayerParamDataBuilder playerParamDataBuilder,
        PlayerParamData playerParamData,
        CommentSpawner commentSpawner,
        InputSerialManager inputSerialManager,
        PlayerAttackEffectSpawner playerAttackEffectSpawner
    )
    {
        _playerAttack = playerAttack;
        _enemyCluster = enemyCluster;

        _playerParamData = playerParamData;
        _playerHp = new PlayerHp(_playerParamData.GetCurrentHp());
        _playerHp.OnDamage += (sender, args) => { commentSpawner.SpawnCommentByType(CommentEnum.Spiky); };

        _inputSerialManager = inputSerialManager;
        _playerAttackEffectSpawner = playerAttackEffectSpawner;
    }

    bool IsTargetEnemy(PlayerColor playerColor, EnemyEnum enemyEnum){
        return playerColor switch
        {
            PlayerColor.Red => enemyEnum == EnemyEnum.W,
            PlayerColor.Blue => enemyEnum == EnemyEnum.A,
            PlayerColor.Yellow => enemyEnum == EnemyEnum.S,
            _ => false
        };
    }

    public int CurrentHp
    {
        set => _playerHp.CurrentHp = value;
        get => _playerHp.CurrentHp;
    }

    public int MaxHp => _playerHp.MaxHp;
} 
}

