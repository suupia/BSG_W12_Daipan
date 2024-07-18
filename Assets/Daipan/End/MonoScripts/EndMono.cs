#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Sound.Interfaces;
using UnityEngine;
using VContainer;

namespace Daipan.End.MonoScripts
{
    public class EndMono : MonoBehaviour
    {
        ISoundManager _soundManager = null!;

        [Inject]
        public void Initialize(ISoundManager soundManager)
        {
            _soundManager = soundManager;
            // _soundManager.FadOutBgm(0.5f);
            _soundManager.StopAllBgm();
            Debug.Log("EndMono is created");
        }
        
        void Start()
        {
            _soundManager.FadOutBgm(0.5f);
        } 
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneTransition.TransitioningScene(SceneName.TitleScene);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _soundManager.FadOutBgm(0.5f);
            }
        }
    }
 
}
