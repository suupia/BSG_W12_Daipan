#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public sealed class EnemyViewTestInputMono : MonoBehaviour
{
    [SerializeField] AbstractEnemyViewMono enemyViewMono = null!;
    [SerializeField] EnemyBoss1ViewMono enemyBoss1ViewMono = null!;
    [SerializeField] EnemyBoss2ViewMono enemyBoss2ViewMono = null!;
    [SerializeField] EnemyBoss3ViewMono enemyBoss3ViewMono = null!;
    [SerializeField] List<GameObject> activeFalseObjects = new();  // プレハブをオーバーロードしないようにするため
    
    [SerializeField] bool isHighlighted = false;
    [SerializeField, Range(0f, 1f)]
    float hpRatio = 0.5f;

    IEnumerable<AbstractEnemyViewMono> _views = new List<AbstractEnemyViewMono>();
    
    void Start()
    {
       enemyViewMono.SetDomain(new EnemyViewParamRed());
       enemyBoss1ViewMono.SetDomain(new EnemyBossViewParam());
       enemyBoss2ViewMono.SetDomain(new EnemyBossViewParam());
       enemyBoss3ViewMono.SetDomain(new EnemyBossViewParam());
       _views = new AbstractEnemyViewMono[] {enemyViewMono, enemyBoss1ViewMono, enemyBoss2ViewMono, enemyBoss3ViewMono};

       foreach (var activeFalseObject in activeFalseObjects)
       {
          activeFalseObject.SetActive(false); 
       }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            foreach (var view in _views)
            {
                view.Move();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (var view in _views)
            {
                view.Attack();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
              foreach (var view in _views)
              {
                 view.Died(() => Debug.Log("Do something when died")); 
              }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (var view in _views)
            {
                view.Daipaned(() => Debug.Log("Do something when daipaned"));
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            enemyViewMono.Daipaned(() =>
            {
                Debug.Log("Destroy enemy with daipan");
                Destroy(enemyViewMono.gameObject);
            });
            
            enemyBoss1ViewMono.Daipaned(() =>
            {
                Debug.Log("Destroy enemy with daipan");
                Destroy(enemyBoss1ViewMono.gameObject);
            });
        }
        
        enemyViewMono.SetHpGauge(hpRatio, 1);
        enemyBoss1ViewMono.SetHpGauge(hpRatio, 1);
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
