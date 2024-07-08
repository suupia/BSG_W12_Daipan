#nullable enable
using TMPro;
using UnityEngine;
using VContainer;

namespace Daipan.Stream.Scripts.Viewer.MonoScripts
{
    public sealed class StreamUIMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI viewerNumberText = null!;
        [SerializeField] TextMeshProUGUI isExcitingText = null!;
        [SerializeField] TextMeshProUGUI existIrrationalFactorsText = null!;

        ViewerNumber _viewerNumber = null!;

        void Update()
        {
            viewerNumberText.text = $"Current Viewers : {_viewerNumber.Number}";
        }


        [Inject]
        public void Initialize(
            ViewerNumber viewerNumber
            )
        {
            _viewerNumber = viewerNumber;
        }
    }
}