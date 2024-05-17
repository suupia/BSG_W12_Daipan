#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.Player.Scripts;
using UnityEngine;
using VContainer;

public class PlayerMono : MonoBehaviour, IHpSetter
{
    EnemyCluster _enemyCluster = null!;
    PlayerAttack _playerAttack = null!;
    PlayerHp _playerHp = null!;
    public PlayerParameter Parameter { get; private set; } = null!;

    public void Update()
    {
        var enemyMono = _enemyCluster.NearestEnemy(transform.position);

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Wが押されたよ");
            if (enemyMono.Parameter.GetEnemyEnum == EnemyEnum.W)
            {
                Debug.Log($"EnemyType: {enemyMono.Parameter.GetEnemyEnum}を攻撃");
                _playerAttack.WAttack(enemyMono);
            }
            else
            {
                Debug.Log("Wが押されたけど攻撃対象がいないよ");
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Sが押されたよ");
            if (enemyMono.Parameter.GetEnemyEnum == EnemyEnum.S)
            {
                Debug.Log($"EnemyType: {enemyMono.Parameter.GetEnemyEnum}を攻撃");
                _playerAttack.SAttack(enemyMono);
            }
            else
            {
                Debug.Log("Sが押されたけど攻撃対象がいないよ");
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Aが押されたよ");
            if (enemyMono.Parameter.GetEnemyEnum == EnemyEnum.A)
            {
                Debug.Log($"EnemyType: {enemyMono.Parameter.GetEnemyEnum}を攻撃");
                _playerAttack.AAttack(enemyMono);
            }
            else
            {
                Debug.Log("Aが押されたけど攻撃対象がいないよ");
            }
        }
    }

    public int CurrentHp
    {
        set => _playerHp.CurrentHp = value;
        get => _playerHp.CurrentHp;
    }

    [Inject]
    public void Initialize(
        PlayerAttack playerAttack,
        EnemyCluster enemyCluster,
        PlayerParameter playerParameter,
        CommentSpawner commentSpawner
    )
    {
        _playerAttack = playerAttack;
        _enemyCluster = enemyCluster;
        Parameter = playerParameter;
        _playerHp = new PlayerHp(playerParameter.hp.maxHp, this);
        _playerHp.OnDamage += (sender, args) => { commentSpawner.SpawnComment(CommentEnum.Spiky); };
    }
}