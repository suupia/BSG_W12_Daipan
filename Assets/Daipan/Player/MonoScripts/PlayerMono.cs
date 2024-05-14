using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using UnityEditorInternal;
using UnityEngine;
using VContainer;

public class PlayerMono : MonoBehaviour
{
    PlayerAttack _playerAttack;
    EnemyCluster _enemyCluster;
    PlayerAttackParameter _attackParameter;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Wが押されたよ");
            _playerAttack.WAttack(8);
            _playerAttack.Attack(0);
            // var enemy = _enemyCluster.Where(e => e.IsAlive).FirstOrDefault();
            // if (enemy != null)
            // {
            //     enemy.Damage(10);
            // }
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Sが押されたよ");
            _playerAttack.SAttack(_attackParameter.SAttackAmount);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Aが押されたよ");
            _playerAttack.AAttack(8);
        }

  
    }

    // [Inject]を付けないと、VContainerからのInjectが行われないことに注意
    [Inject]
    public void Initialize(PlayerAttack playerAttack, EnemyCluster enemyCluster, PlayerAttackParameter attackParameter)
    {
        _playerAttack = playerAttack;
        _enemyCluster = enemyCluster;
        _attackParameter = attackParameter;
    }
}