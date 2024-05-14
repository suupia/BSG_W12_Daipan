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
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Wが押されたよ");
            _playerAttack.WAttack(8);
            _playerAttack.Attack(0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Sが押されたよ");
            _playerAttack.SAttack(_attackParameter.SAttackAmount);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Aが押されたよ");

            var enemyMono = _enemyCluster.NearestEnemy(transform.position);
            if (enemyMono.EnemyParameter.enemyType == EnemyType.A)
            {
                Debug.Log($"EnemyType: {enemyMono.EnemyParameter.enemyType}を攻撃");
                enemyMono.EnemyOnHit.OnHit();
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