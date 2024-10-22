#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Core.Interfaces;
using Daipan.Player.Interfaces;
using Daipan.Sound.MonoScripts;
using UnityEngine;
using VContainer;

namespace Daipan.End.MonoScripts
{
    public class EndMono : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource = null!;
        [SerializeField] List<EndSceneSEParam> _endSceneSEParams = new ();
        IGetEnterKey _getEnterKey = null!;

        [Inject]
        public void Initialize(IGetEnterKey getEnterKey)
        {
            SoundManager.Instance?.StopAllBgm();
            Debug.Log("EndMono is created");
            
            foreach (var endSceneSeParam in _endSceneSEParams)
            {
                if (endSceneSeParam.endSceneEnum == EndSceneHolder.EndSceneEnum)
                {
                    audioSource.clip = endSceneSeParam.audioClip;
                    audioSource.Play();
                }
            }
            _getEnterKey = getEnterKey;

            UnityEngine.Time.timeScale = 1; // timeScaleを戻す
        }
        
        void Start()
        {
            SoundManager.Instance?.FadOutBgm(0.5f);
        } 
        void Update()
        {
            if (_getEnterKey.GetEnterKeyDown())
            {
                SceneTransition.TransitioningScene(SceneName.TitleScene);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SoundManager.Instance?.FadOutBgm(0.5f);
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
