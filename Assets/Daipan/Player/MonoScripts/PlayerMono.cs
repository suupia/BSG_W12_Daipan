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
            _enemyCluster.EnemyDamage(_attackParameter.WAttackAmount);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Sが押されたよ");
            _playerAttack.SAttack(_attackParameter.SAttackAmount);
            _enemyCluster.EnemyDamage(_attackParameter.SAttackAmount);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Aが押されたよ");
            _enemyCluster.EnemyDamage(_attackParameter.AAttackAmount);

            var enemyMono = _enemyCluster.NearestEnemy(transform.position);
            Debug.Log($"enemyMono.EnemyParameter.GetEnemyEnum: {enemyMono.EnemyParameter.GetEnemyEnum}");
            if (enemyMono.EnemyParameter.GetEnemyEnum == EnemyEnum.A)
            {
                Debug.Log($"EnemyType: {enemyMono.EnemyParameter.GetEnemyEnum}を攻撃");
                _playerAttack.AAttack(enemyMono);
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