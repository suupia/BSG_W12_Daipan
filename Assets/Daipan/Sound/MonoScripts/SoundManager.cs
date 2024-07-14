#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Sound.Interfaces;
using UnityEngine;

namespace Daipan.Sound.MonoScripts
{
    public sealed class SoundManager : MonoBehaviour, ISoundManager
    {
        [SerializeField] List<SeParam> seParams = null!;
        
        AudioSource audioSource = null!;

        void Awake()
        {
            audioSource = gameObject.AddComponent<AudioSource>(); 
        }


        public void PlaySe(SeEnum seEnum)
        {
            var seParam = seParams.Find(x => x.seEnum == seEnum);
            if (seParam == null)
            {
                Debug.LogError($"Not found SE: {seEnum}");
                return;
            }

            audioSource.clip = seParam.audioClip;
            audioSource.Play();
        }
    }

    [Serializable]
    public sealed class SeParam
    {
        public SeEnum seEnum;
        public AudioClip audioClip = null!;
    }

    public enum SeEnum
    {
        SpawnComment,
        SpawnAntiComment,
    }
}