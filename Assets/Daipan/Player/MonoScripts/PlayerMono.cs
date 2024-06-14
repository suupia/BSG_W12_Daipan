#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
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
using Daipan.Stream.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace Daipan.Player.MonoScripts
{
   public class PlayerMono : MonoBehaviour, IPlayerHp
{
    [SerializeField] List<AbstractPlayerViewMono?> playerViewMonos = new();
    EnemyCluster _enemyCluster = null!;
    readonly Dictionary<PlayerColor, PlayerAttack> _playerAttacks = new();
    PlayerHp _playerHp = null!;
    PlayerParamData _playerParamData = null!;
    InputSerialManager _inputSerialManager = null!;
    PlayerAttackEffectSpawner _playerAttackEffectSpawner = null!;
    PlayerParamDataContainer _playerParamDataContainer = null!;

    public void Update()
    {
        if (_inputSerialManager.GetButtonRed())
        {
            Debug.Log("Wが押されたよ");
            OnAttackEffectHit(PlayerColor.Red);
        }
        
        if (_inputSerialManager.GetButtonBlue())
        {
            Debug.Log("Aが押されたよ");
            OnAttackEffectHit(PlayerColor.Blue);
        }

        if (_inputSerialManager.GetButtonYellow())
        {
            Debug.Log("Sが押されたよ");
            OnAttackEffectHit(PlayerColor.Yellow);
        }

        // todo : 攻撃やHPの状況に応じて、AbstractPlayerViewMonoのメソッドを呼ぶ
        foreach (var playerViewMono in playerViewMonos) playerViewMono?.Idle();
    }

    void OnAttackEffectHit(PlayerColor playerColor)
    {
        var spawnPosition = playerViewMonos
            .Where(playerViewMono => playerViewMono?.playerColor == playerColor)
            .Select(playerViewMono => playerViewMono?.transform.position)
            .FirstOrDefault(); 
        var effect = _playerAttackEffectSpawner.SpawnEffect(spawnPosition ?? transform.position, Quaternion.identity);
        effect.SetDomain(_playerParamDataContainer.GetPlayerParamData(playerColor));
        effect.TargetEnemyMono = () => GetNearestEnemy(GetTargetEnemyEnum(playerColor));
        effect.OnHit += (sender, args) => AttackEnemy(playerColor, args.EnemyMono);
    }
    
    EnemyEnum GetTargetEnemyEnum(PlayerColor playerColor)
    {
        return playerColor switch
        {
            PlayerColor.Red => EnemyEnum.Red,
            PlayerColor.Blue => EnemyEnum.Blue,
            PlayerColor.Yellow => EnemyEnum.Yellow,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    EnemyMono? GetNearestEnemy(EnemyEnum enemyEnum)
    {
        // そのレーンの敵を取得
        var enemyMono = _enemyCluster.NearestEnemy(enemyEnum, transform.position);
        // レーンの敵がいなければ、ボスを取得
        if (enemyMono == null) enemyMono = _enemyCluster.NearestEnemy(transform.position);
        if (enemyMono == null)
        {
            Debug.Log($"攻撃対象がいないよ");
            return null;
        }

        return enemyMono;
    }
    
    void AttackEnemy(PlayerColor playerColor, EnemyMono? enemyMono)
    {
        Debug.Log($"Attack enemyMono: {enemyMono}");
        if (enemyMono == null) return;
        var targetEnemyEnum = GetTargetEnemyEnum(playerColor);

        if (enemyMono.EnemyEnum == targetEnemyEnum || enemyMono.EnemyEnum == EnemyEnum.RedBoss)
        {
            Debug.Log($"EnemyType: {enemyMono.EnemyEnum}を攻撃");
            _playerAttacks[playerColor].Attack(enemyMono);
            
            // Animation
            foreach (var playerViewMono in playerViewMonos)
            {
                if (playerViewMono == null) continue;
                if (IsTargetEnemy(playerViewMono.playerColor, targetEnemyEnum)) playerViewMono.Attack();
            }
        }
        else
        {
            Debug.Log($"攻撃対象が{targetEnemyEnum}ではないよ");
        }

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
        PlayerParamDataContainer playerParamDataContainer, 
        EnemyCluster enemyCluster,
        PlayerHpParamData playerHpParamData,
        CommentSpawner commentSpawner,
        InputSerialManager inputSerialManager,
        PlayerAttackEffectSpawner playerAttackEffectSpawner,
        IrritatedValue irritatedValue
    )
    {
        _playerParamDataContainer = playerParamDataContainer;
        foreach(PlayerColor playerColor in Enum.GetValues(typeof(PlayerColor)))
        {
            if(playerColor == PlayerColor.None) continue;
            _playerAttacks[playerColor] = new PlayerAttack(playerParamDataContainer.GetPlayerParamData(playerColor));
        }
        _enemyCluster = enemyCluster;

        _playerHp = new PlayerHp(playerHpParamData.GetCurrentHp());
        _playerHp.OnDamage += (sender, args) => { irritatedValue.IncreaseValue(args.DamageValue); };

        _inputSerialManager = inputSerialManager;
        _playerAttackEffectSpawner = playerAttackEffectSpawner;
    }

    bool IsTargetEnemy(PlayerColor playerColor, EnemyEnum enemyEnum){
        return playerColor switch
        {
            PlayerColor.Red => enemyEnum == EnemyEnum.Red,
            PlayerColor.Blue => enemyEnum == EnemyEnum.Blue,
            PlayerColor.Yellow => enemyEnum == EnemyEnum.Yellow,
            _ => false
        };
    }

    public int CurrentHp => _playerHp.CurrentHp;
    public void SetHp(DamageArgs damageArgs) => _playerHp.SetHp(damageArgs);

    public int MaxHp => _playerHp.MaxHp;
} 
}

