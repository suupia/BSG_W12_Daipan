#nullable enable
using System;
using Daipan.Viewer.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Viewer.Tests
{
    public class PlayerTestInput : MonoBehaviour
    {
        DaipanExecutor _daipanExecutor;
        ViewerStatus _viewerStatus;
        
        [Inject]
        public void Construct(
            DaipanExecutor daipanExecutor,
            ViewerStatus viewerStatus)
        {
            _daipanExecutor = daipanExecutor;
            _viewerStatus = viewerStatus;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _daipanExecutor.DaiPan();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if(_viewerStatus.IsExciting)
                    _viewerStatus.IsExciting = false;
                else
                    _viewerStatus.IsExciting = true;
            }
        }
    }
}