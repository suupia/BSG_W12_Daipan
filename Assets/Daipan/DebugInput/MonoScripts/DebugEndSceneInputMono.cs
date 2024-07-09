#nullable enable
using System;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Battle.Scripts;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.DebugInput.MonoScripts
{
    public sealed class DebugEndSceneInputMono : MonoBehaviour
    {
        ResultState _resultState = null!; 
        [Inject]
        public void Initialize(
            ResultState resultState
            )
        {
            _resultState = resultState;
        } 
        
        void Update()
        {
#if UNITY_EDITOR
            
            if (Input.GetKeyDown(KeyCode.E))
            {
               _resultState.ShowResult(); 
            }
         
#endif
        }
        

    }
}