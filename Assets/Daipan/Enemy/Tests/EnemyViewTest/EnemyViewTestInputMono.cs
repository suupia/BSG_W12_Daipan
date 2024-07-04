#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using UnityEngine;

public sealed class EnemyViewTestInputMono : MonoBehaviour
{
    [SerializeField] AbstractEnemyViewMono enemyViewMono = null!;
    [SerializeField] EnemyBossViewMono enemyBossViewMono = null!;
    [SerializeField] List<GameObject> activeFalseObjects = new();  // プレハブをオーバーロードしないようにするため
    
    [SerializeField] bool isHighlighted = false;
    [SerializeField, Range(0f, 1f)]
    float hpRatio = 0.5f;

    void Start()
    {
       enemyViewMono.SetDomain(new EnemyViewParamRed());
       enemyBossViewMono.SetDomain(new EnemyBossViewParam());

       foreach (var activeFalseObject in activeFalseObjects)
       {
          activeFalseObject.SetActive(false); 
       }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            enemyViewMono.Move();
            enemyBossViewMono.Move();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            enemyViewMono.Attack();
            enemyBossViewMono.Attack();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            enemyViewMono.Died( () => Debug.Log("Do something when died"));
              enemyBossViewMono.Died( () => Debug.Log("Do something when died"));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            enemyViewMono.Daipaned( () => Debug.Log("Do something when daipaned"));
            enemyBossViewMono.Daipaned( () => Debug.Log("Do something when daipaned"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            enemyViewMono.Daipaned(() =>
            {
                Debug.Log("Destroy enemy with daipan");
                Destroy(enemyViewMono.gameObject);
            });
            
            enemyBossViewMono.Daipaned(() =>
            {
                Debug.Log("Destroy enemy with daipan");
                Destroy(enemyBossViewMono.gameObject);
            });
        }
        
        enemyViewMono.SetHpGauge(hpRatio, 1);
        enemyBossViewMono.SetHpGauge(hpRatio, 1);
        enemyViewMono.Highlight(isHighlighted);
        
        
    }

    class EnemyViewParamRed : IEnemyViewParamData
    {
        public EnemyEnum GetEnemyEnum() => EnemyEnum.Red;
        public Color GetBodyColor() => Color.red;
        public Color GetEyeColor() => new(226f / 255f, 248f / 255f, 227f / 255f);
        public Color GetEyeBallColor() => Color.red;
        public Color GetLineColor() =>new(111f / 255f, 87f / 255f, 107f / 255f);
    }

    class EnemyBossViewParam : IEnemyViewParamData
    {
        public EnemyEnum GetEnemyEnum() => EnemyEnum.RedBoss;
        public Color GetBodyColor() => Color.red;
        public Color GetEyeColor() => new(226f / 255f, 248f / 255f, 227f / 255f);
        public Color GetEyeBallColor() => Color.red;
        public Color GetLineColor() => new(111f / 255f, 87f / 255f, 107f / 255f);
    }
}
