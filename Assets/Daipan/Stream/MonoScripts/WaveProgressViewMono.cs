#nullable enable
using Daipan.Battle.scripts;
using Daipan.Core.Interfaces;
using Daipan.Stream.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace Daipan.Stream.MonoScripts
{
    public sealed class WaveProgressViewMono : MonoBehaviour, IUpdate
    {
        [SerializeField] Slider waveProgressSlider = null!;
        
        WaveProgress _waveProgress = null!;
        void IUpdate.Update()
        {
            waveProgressSlider.value = (float)_waveProgress.CurrentProgressRatio;
            // Debug.Log($"waveProgressSlider.value: {waveProgressSlider.value}");
        }

        [Inject]
        public void Initialize(
            WaveProgress timer
            )
        {
            _waveProgress = timer;
        }
    }
}