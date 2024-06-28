#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using UnityEngine;

public class EnemyHighlightTestInputMono : MonoBehaviour
{
    [SerializeField] EnemyViewMono enemyViewMono = null!;

    void Start()
    {
        enemyViewMono.SetView(EnemyEnum.Red);
    }
}
