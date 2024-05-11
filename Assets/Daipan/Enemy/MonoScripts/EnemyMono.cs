using Enemy;
using UnityEngine;
using VContainer;

public class EnemyMono : MonoBehaviour
{
    EnemyAttack _enemyAttack;
    public IEnemyOnHit enemyOnHit;
    EnemyParameter _enemyParameter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) _enemyAttack.Attack();
        if (Input.GetKeyDown(KeyCode.S)) enemyOnHit.OnHit(_enemyParameter.enemyType);
    }
    
    //?????[Inject]をつけると勝手にVContainerに呼び出される？
    [Inject]
    public void Initialize(EnemyAttack enemyAttack, IEnemyOnHit enemyOnHit_)
    {
        _enemyAttack = enemyAttack;
        enemyOnHit = enemyOnHit_;
    }

    public void PureInitialize(EnemyParameter enemyParameter)
    {
        _enemyParameter = enemyParameter;

        _enemyAttack.enemyAttackParameter = _enemyParameter.attackParameter;

        var enemyOnHit_ = enemyOnHit as EnemyOnHit;
        enemyOnHit_.ownEnemyType = _enemyParameter.enemyType;
    }
}
