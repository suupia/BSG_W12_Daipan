#nullable enable
using Stream.Viewer.Scripts;
using TMPro;
using UnityEngine;
using VContainer;

namespace Stream.Viewer.MonoScripts
{
    public class ViewerUIMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI viewerNumberText = null!;

        ViewerNumber _viewerNumber = null!;

        void Update()
        {
            viewerNumberText.text = _viewerNumber.Number.ToString();
        }

        [Inject]
        public void Initialize(ViewerNumber viewerNumber)
        {
            _viewerNumber = viewerNumber;
        }
    }
}