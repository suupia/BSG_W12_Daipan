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
        
        AudioSource _audioSource = null!;

        void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>(); 
        }


        public void PlaySe(SeEnum seEnum)
        {
            var seParam = seParams.Find(x => x.seEnum == seEnum);
            if (seParam == null)
            {
                Debug.LogError($"Not found SE: {seEnum}");
                return;
            }

            _audioSource.clip = seParam.audioClip;
            _audioSource.Play();
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