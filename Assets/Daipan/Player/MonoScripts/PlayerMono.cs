using Enemy;
using Stream.Player.Scripts;
using UnityEditorInternal;
using UnityEngine;
using VContainer;

public class PlayerMono : MonoBehaviour
{
    PlayerAttack _playerAttack;
    EnemyCluster _enemyCluster;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Wが押されたよ");
            _playerAttack.Attack(0);
            // var enemy = _enemyCluster.Where(e => e.IsAlive).FirstOrDefault();
            // if (enemy != null)
            // {
            //     enemy.Damage(10);
            // }
        }

        // AとSの処理はまかせます by すーぴあ
    }

    // [Inject]を付けないと、VContainerからのInjectが行われないことに注意
    [Inject]
    public void Initialize(PlayerAttack playerAttack, EnemyCluster enemyCluster)
    {
        _playerAttack = playerAttack;
        _enemyCluster = enemyCluster;
    }
}