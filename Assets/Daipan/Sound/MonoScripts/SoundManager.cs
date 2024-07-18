#nullable enable
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Daipan.Sound.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Daipan.Sound.MonoScripts
{
    public sealed class SoundManager : MonoBehaviour, ISoundManager
    {
        [SerializeField] List<BgmParam> bgmParams = null!;
        [SerializeField] List<SeParam> seParams = null!;
        static SoundManager? _instance;
        public static ISoundManager? Instance => _instance;
        public static float BgmVolume
        {
            set => _bgmVolume = Mathf.Clamp(value / 7f, 0, 1);
            get => (int)(_bgmVolume * 7);
        }
        static float _bgmVolume;
        public static float SeVolume
        {
            set => _seVolume = Mathf.Clamp(value / 7f, 0, 1);
            get => (int)(_seVolume * 7);
        }
        static float _seVolume;

        public void Initialize()
        {
            if(_instance == null) 
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                Debug.Log("SoundManager is created");
            }
            else
            {
                Debug.Log("SoundManager is already created");
            } 
            
            foreach (var bgmParam in bgmParams)
            {
                bgmParam.audioSource.clip = bgmParam.audioClip;
            }
            
            foreach (var seParam in seParams)
            {
                seParam.audioSource.clip = seParam.audioClip;
            }
            BgmVolume = 4;
            SeVolume = 4;
        }

        public void PlayBgm(BgmEnum bgmEnum)
        {
            var bgmParam = bgmParams.Find(x => x.bgmEnum == bgmEnum);
            if (bgmParam == null)
            {
                Debug.LogError($"Not found BGM: {bgmEnum}");
                return;
            }
            
            const float fadeSec = 1f;

            // Stop other BGMs with fade-out
            foreach (var param in bgmParams)
            {
                if (param.bgmEnum != bgmEnum && param.audioSource.isPlaying)
                {
                    param.audioSource.DOFade(0, fadeSec).OnComplete(() => param.audioSource.Stop());
                }
            }

            Debug.Log("Sound is Good");
            // Play the selected BGM
            bgmParam.audioSource.volume = 0;
            bgmParam.audioSource.Play();
            bgmParam.audioSource.DOFade(_bgmVolume, fadeSec);
            
            Debug.Log($"Play BGM: {bgmEnum} ,volume: {bgmParam.audioSource.volume}");
        }

        public void PlaySe(SeEnum seEnum)
        {
            var seParam = seParams.Find(x => x.seEnum == seEnum);
            if (seParam == null)
            {
                Debug.LogError($"Not found SE: {seEnum}");
                return;
            }

            seParam.audioSource.clip = seParam.audioClip;
            seParam.audioSource.volume = _seVolume;
            seParam.audioSource.Play();
            Debug.Log($"Play SE: {seEnum}, volume: {seParam.audioSource.volume}, seParam.seEnum: {seParam.seEnum}, audioClip.name: {seParam.audioClip.name}");
        }
        
        public void FadOutBgm(float fadeSec)
        {
            foreach (var param in bgmParams)
            {
                Debug.Log($"param : {param.bgmEnum}, {param.audioSource.isPlaying}");
                if (param.audioSource.isPlaying)
                {
                    param.audioSource.DOFade(0, fadeSec).OnComplete(() => param.audioSource.Stop());
                }
            }
        } 
    }

    [Serializable]
    public sealed class BgmParam
    {
        public BgmEnum bgmEnum;
        public AudioSource audioSource = null!;
        public AudioClip audioClip = null!;
    }

    [Serializable]
    public sealed class SeParam
    {
        public SeEnum seEnum;
        public AudioSource audioSource = null!;
        public AudioClip audioClip = null!;
    }

    public enum BgmEnum
    {
        Title,
        Tutorial,
        Daipan,
        EndScene,
    }


    public enum SeEnum
    {
        SpawnComment,
        SpawnAntiComment,
        AttackDeflect,
        Attack,
        Daipan,
        
        // EndScene
        Hakononaka,  // 箱の中END
        Kansyasai,  // 配信者ちゃん感謝祭END
        NoobGamer,     // ゲーム下手配信者END
        ProGamer,      // プロゲーマーEND
        Seijo,    // 聖女END
        Enjou,      // 炎上END
        Genkai, // 限界配信者END
        Heibon, // 平凡な配信者END
    }
}