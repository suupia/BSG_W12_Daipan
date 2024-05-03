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
        DistributionStatus _distributionStatus;
        
        [Inject]
        public void Construct(
            DaipanExecutor daipanExecutor,
            DistributionStatus distributionStatus)
        {
            _daipanExecutor = daipanExecutor;
            _distributionStatus = distributionStatus;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _daipanExecutor.DaiPan();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if(_distributionStatus.IsExciting)
                    _distributionStatus.IsExciting = false;
                else
                    _distributionStatus.IsExciting = true;
            }
        }
    }
}