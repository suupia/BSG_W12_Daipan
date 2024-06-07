#nullable enable
using System.Collections.Generic;
using Daipan.Battle.interfaces;
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.LevelDesign.Player.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

public class PlayerMono : MonoBehaviour, IHpSetter
{
    [SerializeField] List<AbstractPlayerViewMono?> playerViewMonos = new();
    EnemyCluster _enemyCluster = null!;
    PlayerAttack _playerAttack = null!;
    PlayerHp _playerHp = null!;
    PlayerParamData _playerParamData = null!;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Wが押されたよ");
            AttackEnemyMono(EnemyEnum.W);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Sが押されたよ");
            AttackEnemyMono(EnemyEnum.S);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Aが押されたよ");
            AttackEnemyMono(EnemyEnum.A);
        }

        // todo : 攻撃やHPの状況に応じて、AbstractPlayerViewMonoのメソッドを呼ぶ
        foreach (var playerViewMono in playerViewMonos) playerViewMono?.Idle();
    }

    void AttackEnemyMono(EnemyEnum enemyEnum)
    {
        var enemyMono = _enemyCluster.NearestEnemy(enemyEnum, transform.position);
        if (enemyMono == null)
        {
            Debug.Log($"攻撃対象がいないよ");
            return;
        }

        if (enemyMono.EnemyEnum == enemyEnum || enemyMono.EnemyEnum == EnemyEnum.Boss)
        {
            Debug.Log($"EnemyType: {enemyMono.EnemyEnum}を攻撃");
            _playerAttack.Attack(enemyMono);
        }
        else
        {
            Debug.Log($"攻撃対象が{enemyEnum}ではないよ");
        }
    }

    public int CurrentHp
    {
        set => _playerHp.CurrentHp = value;
        get => _playerHp.CurrentHp;
    }

    public int MaxHp => _playerHp.MaxHp;

    [Inject]
    public void Initialize(
        PlayerAttack playerAttack,
        EnemyCluster enemyCluster,
        PlayerParamDataBuilder playerParamDataBuilder,
        PlayerParamData playerParamData,
        CommentSpawner commentSpawner
    )
    {
        _playerAttack = playerAttack;
        _enemyCluster = enemyCluster;

        _playerParamData = playerParamData;
        _playerHp = new PlayerHp(_playerParamData.GetCurrentHp());
        _playerHp.OnDamage += (sender, args) => { commentSpawner.SpawnCommentByType(CommentEnum.Spiky); };
    }
}