using Enemy;
using Stream.Player.Scripts;
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
            //_playerAttack.WAttack(_attackParameter.WAttackAmount);
            _enemyCluster.EnemyDamage("W", _attackParameter.WAttackAmount);
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
            _enemyCluster.EnemyDamage("S", _attackParameter.SAttackAmount);
            //_playerAttack.SAttack(_attackParameter.SAttackAmount);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Aが押されたよ");
            _enemyCluster.EnemyDamage("A", _attackParameter.AAttackAmount);
            //_playerAttack.AAttack(_attackParameter.AAttackAmount);
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