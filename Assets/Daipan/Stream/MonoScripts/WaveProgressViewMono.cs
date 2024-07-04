#nullable enable
using Daipan.Battle.scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace Daipan.Stream.MonoScripts
{
    public sealed class WaveProgressViewMono : MonoBehaviour
    {
        [SerializeField] Slider waveProgressSlider = null!;
        
        StreamTimer _streamTimer = null!;
        void Update()
        {
            waveProgressSlider.value = (float)_streamTimer.CurrentProgressRatio;
        }

        [Inject]
        public void Initialize(
            StreamTimer timer
            )
        {
            _streamTimer = timer;
        }
    }
}