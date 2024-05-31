#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
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
            if (enemyMono.EnemyEnum == EnemyEnum.W || enemyMono.EnemyEnum == EnemyEnum.Boss)
            {
                Debug.Log($"EnemyType: {enemyMono.EnemyEnum}を攻撃");
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
            if (enemyMono.EnemyEnum == EnemyEnum.S || enemyMono.EnemyEnum == EnemyEnum.Boss)
            {
                Debug.Log($"EnemyType: {enemyMono.EnemyEnum}を攻撃");
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
            if (enemyMono.EnemyEnum == EnemyEnum.A || enemyMono.EnemyEnum == EnemyEnum.Boss)
            {
                Debug.Log($"EnemyType: {enemyMono.EnemyEnum}を攻撃");
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