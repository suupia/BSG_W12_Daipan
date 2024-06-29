#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using UnityEngine;

public class EnemyViewTestInputMono : MonoBehaviour
{
    [SerializeField] AbstractEnemyViewMono enemyViewMono = null!;

    void Start()
    {
       enemyViewMono.SetDomain(new EnemyViewParamRed());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            enemyViewMono.Move();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            enemyViewMono.Died( () => Debug.Log("Do something when died"));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            enemyViewMono.Daipaned( () => Debug.Log("Do something when daipaned"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            enemyViewMono.Daipaned(() =>
            {
                Debug.Log("Destroy enemy with daipan");
                Destroy(enemyViewMono.gameObject);
            });
        }
    }

    class EnemyViewParamRed : IEnemyViewParamData
    {
        public EnemyEnum GetEnemyEnum() => EnemyEnum.Red;
        public Color GetBodyColor() => Color.red;
        public Color GetEyeColor() => new(226f / 255f, 248f / 255f, 227f / 255f);
        public Color GetEyeBallColor() => Color.red;
        public Color GetLineColor() =>new(111f / 255f, 87f / 255f, 107f / 255f);
    }
}
