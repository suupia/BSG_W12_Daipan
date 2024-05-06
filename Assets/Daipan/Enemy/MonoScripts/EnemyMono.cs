using Enemy;
using UnityEngine;
using VContainer;

public class EnemyMono : MonoBehaviour
{
    EnemyAttack _enemyAttack;
    EnemyOnHitNormal _enemyOnHitNormal;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) _enemyAttack.Attack();
        if (Input.GetKeyDown(KeyCode.S)) _enemyOnHitNormal.OnHit();
    }

    [Inject]
    public void Initialize(EnemyAttack enemyAttack, EnemyOnHitNormal enemyOnHitNormal)
    {
        _enemyAttack = enemyAttack;
        _enemyOnHitNormal = enemyOnHitNormal; 
    }
}
