#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Player.Scripts;
using Daipan.Player.Scripts;
using UnityEngine;
using VContainer;

public class PlayerMono : MonoBehaviour, IHpSetter
{
    [SerializeField] HpGaugeMono hpGaugeMono = null!; 
    EnemyCluster _enemyCluster = null!;
    PlayerAttack _playerAttack = null!;
    PlayerHp _playerHp = null!;
    PlayerParamData _playerParamData = null!;

    public void Update()
    {
        var enemyMono = _enemyCluster.NearestEnemy(transform.position);
        
        hpGaugeMono.SetRatio(CurrentHp / (float)_playerParamData.GetCurrentHp());

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Wが押されたよ");
            AttackEnemyMono(enemyMono, EnemyEnum.W);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Sが押されたよ");
            AttackEnemyMono(enemyMono, EnemyEnum.S); 
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Aが押されたよ");
            AttackEnemyMono(enemyMono, EnemyEnum.A);
        }
    }

    void AttackEnemyMono(EnemyMono? enemyMono, EnemyEnum enemyEnum)
    {
        if (enemyMono == null)
        {
            Debug.Log($"攻撃対象がいないよ");
            return;
        } 
        if(enemyMono.EnemyEnum == enemyEnum || enemyMono.EnemyEnum == EnemyEnum.Boss)
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

    [Inject]
    public void Initialize(
        PlayerAttack playerAttack,
        EnemyCluster enemyCluster,
        PlayerParamDataBuilder  playerParamDataBuilder,
        PlayerParamData playerParamData,
        CommentSpawner commentSpawner
    )
    {
        _playerAttack = playerAttack;
        _enemyCluster = enemyCluster;

        _playerParamData = playerParamData;
        _playerHp = new PlayerHp(_playerParamData.GetCurrentHp(), this);
        _playerHp.OnDamage += (sender, args) => { commentSpawner.SpawnCommentByType(CommentEnum.Spiky); };
    }
}