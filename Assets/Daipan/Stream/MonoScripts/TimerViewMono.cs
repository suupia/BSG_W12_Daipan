#nullable enable
using Daipan.Stream.Scripts;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Daipan.Stream.MonoScripts
{
    public class TimerViewMono : MonoBehaviour
    {
        [SerializeField] Slider timerSlider = null!;
        
        StreamTimer _streamTimer = null!;
        void Update()
        {
            timerSlider.value = (float)_streamTimer.CurrentProgressRatio;
        }

        [Inject]
        public void Initialize(StreamTimer timer)
        {
            _streamTimer = timer;
        }
    }
}