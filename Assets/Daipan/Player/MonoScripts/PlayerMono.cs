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
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace Daipan.Player.MonoScripts
{
   public class PlayerMono : MonoBehaviour, IPlayerHp
{
    [SerializeField] List<AbstractPlayerViewMono?> playerViewMonos = new();
    EnemyCluster _enemyCluster = null!;
    PlayerHp _playerHp = null!;
    InputSerialManager _inputSerialManager = null!;
    PlayerAttackEffectSpawner _playerAttackEffectSpawner = null!;
    PlayerParamDataContainer _playerParamDataContainer = null!;

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

    void FireAttackEffect( PlayerColor playerColor)
    {
        // 一番近い敵を取得し、そこに向かってAttackEffectを発射する（敵がいなくても生成する）
        var targetEnemy = _enemyCluster.NearestEnemy(GetTargetEnemyEnum(playerColor), transform.position); 
        
        // todo : AttackEffectの生成位置は仕様によって変更する。
        // とりあえずは、x座標は同じ色のプレイヤーのx座標、y座標はtargetEnemyのy座標に生成する
        var sameColorPlayerViewMono = playerViewMonos
            .FirstOrDefault(playerViewMono => playerViewMono?.playerColor == playerColor);
        if (sameColorPlayerViewMono == null)
        {
            Debug.LogWarning($"同じ色のプレイヤーがいません");
            return;
        }
        
        // todo : y座標はレーンの座標を使用したい
        var spawnPositionY = targetEnemy != null ? targetEnemy.transform.position.y : sameColorPlayerViewMono.transform.position.y;
        var spawnPosition = new Vector3(sameColorPlayerViewMono.transform.position.x,spawnPositionY, 0);
        
        var effect = _playerAttackEffectSpawner.SpawnEffect(spawnPosition , Quaternion.identity);
        effect.SetDomain(_playerParamDataContainer.GetPlayerParamData(playerColor));
        effect.SetTargetEnemyMono(targetEnemy);
        effect.OnHit += (sender, args) => AttackEnemy(_playerParamDataContainer, playerViewMonos, playerColor, args.EnemyMono);
        
    }
    
    
    static EnemyEnum GetTargetEnemyEnum(PlayerColor playerColor)
    {
        return playerColor switch
        {
            PlayerColor.Red => EnemyEnum.Red,
            PlayerColor.Blue => EnemyEnum.Blue,
            PlayerColor.Yellow => EnemyEnum.Yellow,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    static void AttackEnemy(PlayerParamDataContainer playerParamDataContainer, List<AbstractPlayerViewMono?> playerViewMonos, 
        PlayerColor playerColor, EnemyMono? enemyMono)
    {
        Debug.Log($"Attack enemyMono: {enemyMono}");
        if (enemyMono == null) return;
        var targetEnemyEnum = GetTargetEnemyEnum(playerColor);

        if (enemyMono.EnemyEnum == targetEnemyEnum || enemyMono.EnemyEnum == EnemyEnum.RedBoss)
        {
            Debug.Log($"EnemyType: {enemyMono.EnemyEnum}を攻撃");
            enemyMono.CurrentHp -= playerParamDataContainer.GetPlayerParamData(playerColor).GetAttack();
            
            // Animation
            foreach (var playerViewMono in playerViewMonos)
            {
                if (playerViewMono == null) continue;
                if (GetTargetEnemyEnum(playerViewMono.playerColor) == targetEnemyEnum) playerViewMono.Attack();
            }
        }
        else
        {
            Debug.Log($"攻撃対象が{targetEnemyEnum}ではないよ");
        }

    }

    [Inject]
    public void Initialize(
        PlayerParamDataContainer playerParamDataContainer, 
        EnemyCluster enemyCluster,
        PlayerHpParamData playerHpParamData,
        InputSerialManager inputSerialManager,
        PlayerAttackEffectSpawner playerAttackEffectSpawner,
        IrritatedValue irritatedValue
    )
    {
        _playerParamDataContainer = playerParamDataContainer;
        _enemyCluster = enemyCluster;

        _playerHp = new PlayerHp(playerHpParamData.GetCurrentHp());
        _playerHp.OnDamage += (sender, args) =>
        {
            // Domain
            irritatedValue.IncreaseValue(args.DamageValue);
            
            // View
            foreach (var playerViewMono in playerViewMonos)
            {
                if (playerViewMono == null) continue;
                if(GetTargetEnemyEnum(playerViewMono.playerColor) == args.enemyEnum) playerViewMono.Damage();
            } 
        };

        _inputSerialManager = inputSerialManager;
        _playerAttackEffectSpawner = playerAttackEffectSpawner;
    }

    public int CurrentHp => _playerHp.CurrentHp;
    public void SetHp(DamageArgs damageArgs) => _playerHp.SetHp(damageArgs);

    public int MaxHp => _playerHp.MaxHp;
} 
}

