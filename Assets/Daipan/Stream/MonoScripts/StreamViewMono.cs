using Daipan.Stream.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Daipan.Stream.MonoScripts
{
    public sealed class StreamViewMono : MonoBehaviour
    {
        [SerializeField] Image irritatedGauge = null!;
        [SerializeField] Image viewerGauge = null!;
        [SerializeField] TextMeshProUGUI viewerNumberText = null!;

        IrritatedValue _irritatedValue = null!;
        ViewerNumber _viewerNumber = null!;

        void Update()
        {
            irritatedGauge.fillAmount = _irritatedValue.Ratio;
            viewerGauge.fillAmount = _viewerNumber.Ratio;
            viewerNumberText.text = $"Viewers : {_viewerNumber.Number}";
        }

        [Inject]
        public void Initialize(
            IrritatedValue irritatedValue,
            ViewerNumber viewerNumber
        )
        {
            _irritatedValue = irritatedValue;
            _viewerNumber = viewerNumber;
        }
    }
}