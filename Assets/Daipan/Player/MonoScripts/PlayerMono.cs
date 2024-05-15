using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;

public class PlayerMono : MonoBehaviour
{
    PlayerAttackParameter _attackParameter;
    EnemyCluster _enemyCluster;
    PlayerAttack _playerAttack;

    public void Update()
    {
        
        var enemyMono = _enemyCluster.NearestEnemy(transform.position);
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Wが押されたよ");
            if(enemyMono.EnemyParameter.GetEnemyEnum == EnemyEnum.W)
            {
                Debug.Log($"EnemyType: {enemyMono.EnemyParameter.GetEnemyEnum}を攻撃");
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
            if (enemyMono.EnemyParameter.GetEnemyEnum == EnemyEnum.S)
            {
                Debug.Log($"EnemyType: {enemyMono.EnemyParameter.GetEnemyEnum}を攻撃");
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
            if (enemyMono.EnemyParameter.GetEnemyEnum == EnemyEnum.A)
            {
                Debug.Log($"EnemyType: {enemyMono.EnemyParameter.GetEnemyEnum}を攻撃");
                _playerAttack.AAttack(enemyMono);
            }
            else
            {
                Debug.Log("Aが押されたけど攻撃対象がいないよ");
            }
        }
    }

    [Inject]
    public void Initialize(
        PlayerAttack playerAttack,
        EnemyCluster enemyCluster,
        PlayerAttackParameter attackParameter
    )
    {
        _playerAttack = playerAttack;
        _enemyCluster = enemyCluster;
        _attackParameter = attackParameter;
    }
}