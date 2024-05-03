#nullable enable
using Daipan.Viewer.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Viewer.Tests
{
    public class PlayerTestInput : MonoBehaviour
    {
        DaipanExecutor _daipanExecutor = null!;
        DistributionStatus _distributionStatus = null!;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) _daipanExecutor.DaiPan();

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (_distributionStatus.IsExciting)
                    _distributionStatus.IsExciting = false;
                else
                    _distributionStatus.IsExciting = true;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (_distributionStatus.ExistIrrationalFactors)
                    _distributionStatus.ExistIrrationalFactors = false;
                else
                    _distributionStatus.ExistIrrationalFactors = true;
            }
        }

        [Inject]
        public void Construct(
            DaipanExecutor daipanExecutor,
            DistributionStatus distributionStatus)
        {
            _daipanExecutor = daipanExecutor;
            _distributionStatus = distributionStatus;
        }
    }
}