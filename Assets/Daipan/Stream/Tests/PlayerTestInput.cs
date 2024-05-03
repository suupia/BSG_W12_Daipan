#nullable enable
using Stream.Viewer.Scripts;
using UnityEngine;
using VContainer;

namespace Stream.Viewer.Tests
{
    public class PlayerTestInput : MonoBehaviour
    {
        DaipanExecutor _daipanExecutor = null!;
        StreamStatus _streamStatus = null!;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) _daipanExecutor.DaiPan();

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (_streamStatus.IsExciting)
                    _streamStatus.IsExciting = false;
                else
                    _streamStatus.IsExciting = true;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (_streamStatus.ExistIrrationalFactors)
                    _streamStatus.ExistIrrationalFactors = false;
                else
                    _streamStatus.ExistIrrationalFactors = true;
            }
        }

        [Inject]
        public void Construct(
            DaipanExecutor daipanExecutor,
            StreamStatus streamStatus)
        {
            _daipanExecutor = daipanExecutor;
            _streamStatus = streamStatus;
        }
    }
}