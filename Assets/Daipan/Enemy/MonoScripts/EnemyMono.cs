using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class EnemyMono : MonoBehaviour
{
    EnemyAttack _enemyAttack;
    EnemyOnHitNormal _enemyOnHitNormal;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) _enemyAttack.Attack();
        if (Input.GetKeyDown(KeyCode.S)) _enemyOnHitNormal.OnHit();
    }
}
