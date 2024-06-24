#nullable enable
using UnityEngine;
using VContainer;
using Daipan.Stream.Scripts;
using R3;
using TMPro;

namespace Daipan.Stream.MonoScripts
{
    public class ViewerViewMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI viewerText = null!;

        [Inject]
        public void Initialize(ViewerNumber viewerNumber)
        {
            Observable.EveryValueChanged(viewerNumber, x => viewerNumber.Number)
                .Subscribe(_ => UpdateViewerText(viewerNumber.Number))
                .AddTo(this);
        }

        void UpdateViewerText(int viewerNumber)
        {
            viewerText.text = $"{viewerNumber}";
        }
    }
}