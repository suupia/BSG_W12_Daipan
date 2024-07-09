#nullable enable
using System;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.DebugInput.MonoScripts
{
    public sealed class DebugEndSceneInputMono : MonoBehaviour
    {
        EndSceneSelector _endSceneSelector = null!; 
       
        [Inject]
        public void Initialize(
            EndSceneSelector endSceneSelector
            )
        {
            _endSceneSelector = endSceneSelector;
        } 
        
        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.E))
            {
                _endSceneSelector.TransitToEndScene();
            }
            
            // todo : それぞれの遷移に対する条件を個別に設定してその通りに遷移するかテストする
            // var enumValues = Enum.GetValues(typeof(EndSceneEnum)).Cast<EndSceneEnum>().ToList();
            // for (int i = 0; i < enumValues.Count; i++)
            // {
            //     KeyCode keyCode = KeyCode.Alpha1 + i; // KeyCode.Alpha1 から順にキーコードを取得
            //     if (Input.GetKey(keyCode))
            //     {
            //         // 線条件を条件を外部で設定する必要がある
            //        _endSceneSelector.TransitToEndScene(enumValues[i]); 
            //     }
            // }
#endif
        }
        

    }
}