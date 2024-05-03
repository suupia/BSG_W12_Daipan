#nullable enable
using System;
using Daipan.Viewer.Scripts;
using TMPro;
using UnityEngine;
using VContainer;

namespace Daipan.Viewer.MonoScripts
{
    public class ViewerUIMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI viewerNumberText = null!;
        
        ViewerNumber _viewerNumber = null!;

        [Inject]
        public void Initialize(ViewerNumber viewerNumber)
        {
            _viewerNumber = viewerNumber;
        }
        void Update()
        {
            viewerNumberText.text = _viewerNumber.Number.ToString(); 
        }
    }
}