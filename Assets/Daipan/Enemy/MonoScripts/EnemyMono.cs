using Enemy;
using UnityEngine;
using VContainer;

public class EnemyMono : MonoBehaviour
{
    EnemyAttack _enemyAttack;
    IEnemyOnHit _enemyOnHit;
    EnemyParameter _enemyParameter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) _enemyAttack.Attack();
        if (Input.GetKeyDown(KeyCode.S)) _enemyOnHit.OnHit(_enemyParameter.enemyType);
    }
    
    //?????[Inject]をつけると勝手にVContainerに呼び出される？
    [Inject]
    public void Initialize(EnemyAttack enemyAttack, IEnemyOnHit enemyOnHit)
    {
        _enemyAttack = enemyAttack;
        _enemyOnHit = enemyOnHit;
    }

    public void PureInitialize(EnemyParameter enemyParameter)
    {
        _enemyParameter = enemyParameter;

        _enemyAttack._enemyAttackParameter = _enemyParameter.attackParameter;
    }
}
