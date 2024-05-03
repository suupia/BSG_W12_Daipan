#nullable enable
using Stream.Viewer.Scripts;
using TMPro;
using UnityEngine;
using VContainer;

namespace Stream.Viewer.MonoScripts
{
    public class StreamUIMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI viewerNumberText = null!;
        [SerializeField] TextMeshProUGUI isExcitingText = null!;
        [SerializeField] TextMeshProUGUI existIrrationalFactorsText = null!;

        StreamStatus _streamStatus = null!;
        ViewerNumber _viewerNumber = null!;

        void Update()
        {
            viewerNumberText.text = $"Current Viewers : {_viewerNumber.Number}";
            isExcitingText.text = $"IsExciting : {_streamStatus.IsExcited}";
            existIrrationalFactorsText.text = $"ExistIrrationalFactors : {_streamStatus.IsIrritated}";
        }


        [Inject]
        public void Initialize(
            ViewerNumber viewerNumber,
            StreamStatus streamStatus)
        {
            _viewerNumber = viewerNumber;
            _streamStatus = streamStatus;
        }
    }
}