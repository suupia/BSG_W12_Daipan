#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Daipan.Battle.scripts;
using Daipan.Core.Interfaces;
using Daipan.Player.Interfaces;
using Daipan.Sound.MonoScripts;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Daipan.End.MonoScripts
{
    public class EndMono : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource = null!;
        [SerializeField] List<EndSceneSEParam> _endSceneSEParams = new ();
        [SerializeField] Image _creditImage = null!;
        IGetEnterKey _getEnterKey = null!;

        bool _isShowedCredit;
        
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
            _creditImage.gameObject.SetActive(false); // 最初は非表示

            UnityEngine.Time.timeScale = 1; // timeScaleを戻す
        }
        
        void Start()
        {
            SoundManager.Instance?.FadOutBgm(0.5f);
        }

        async void Update()
        {
            if (_getEnterKey.GetEnterKeyDown())
            {
                if (!_isShowedCredit)
                {
                    _isShowedCredit = true;
                    FindObjectOfType<EndBackgroundViewMono>().endSceneText.gameObject.SetActive(false);
                    _creditImage.gameObject.SetActive(true);

                    audioSource.mute = true;
                    // audioSourceをフェードアウト
                    while (SoundManager.SeVolume > 0)
                    {
                        SoundManager.SeVolume -= 0.01f;
                        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                    }
                    SoundManager.Instance?.StopAllSe(); // エンドシーンの音はSE扱い

                    await UniTask.Delay(TimeSpan.FromSeconds(0.5f)); // すぐに遷移してしまうのを防ぐ
                    return;
                }
                else
                {
                    SceneTransition.TransitioningScene(SceneName.TitleScene);
                }
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
