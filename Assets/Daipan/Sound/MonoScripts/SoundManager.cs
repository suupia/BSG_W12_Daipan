#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Sound.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Daipan.Sound.MonoScripts
{
    public sealed class SoundManager : MonoBehaviour, ISoundManager
    {
        [SerializeField] List<BgmParam> bgmParams = null!;
        [SerializeField] List<SeParam> seParams = null!;

        void Awake()
        {
            foreach (var bgmParam in bgmParams)
            {
                bgmParam.audioSource.clip = bgmParam.audioClip;
            }
            
            foreach (var seParam in seParams)
            {
                seParam.audioSource.clip = seParam.audioClip;
            }
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

            // Play the selected BGM
            bgmParam.audioSource.volume = 0;
            bgmParam.audioSource.Play();
            bgmParam.audioSource.DOFade(1, fadeSec);
        }

        public void PlaySe(SeEnum seEnum)
        {
            var seParam = seParams.Find(x => x.seEnum == seEnum);
            if (seParam == null)
            {
                Debug.LogError($"Not found SE: {seEnum}");
                return;
            }

            seParam.audioSource.Play();
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
        Battle,
        EndScene,
    }


    public enum SeEnum
    {
        SpawnComment,
        SpawnAntiComment,
        AttackDeflect,
        Attack,
    }
}