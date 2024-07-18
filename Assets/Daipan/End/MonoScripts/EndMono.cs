#nullable enable
using System;
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
        [SerializeField] AudioSource audioSource = null!;
        ISoundManager _soundManager = null!;
        [SerializeField] List<EndSceneSEParam> _endSceneSEParams = new List<EndSceneSEParam>();

        [Inject]
        public void Initialize(ISoundManager soundManager)
        {
            _soundManager = soundManager;
            _soundManager.StopAllBgm();
            Debug.Log("EndMono is created");
            
            foreach (var endSceneSEParam in _endSceneSEParams)
            {
                if (endSceneSEParam.endSceneEnum == EndSceneHolder.EndSceneEnum)
                {
                    audioSource.clip = endSceneSEParam.audioClip;
                    audioSource.Play();
                }
            }
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
    
    [Serializable]
    public sealed class EndSceneSEParam
    {
        public EndSceneEnum endSceneEnum;
        public AudioClip audioClip = null!;
    }

    public static class EndSceneHolder
    {
        public static EndSceneEnum EndSceneEnum { get; set; }
    }
}
