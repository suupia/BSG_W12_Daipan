#nullable enable
using Stream.Viewer.Scripts;
using UnityEngine;
using VContainer;

namespace Stream.Viewer.Tests
{
    public class PlayerTestInput : MonoBehaviour
    {
        [SerializeField] CustomButton isExcitedButton = null!;
        [SerializeField] CustomButton isIrritatedButton = null!;
        [SerializeField] CustomButton daiPanButton = null!;
        DaipanExecutor _daipanExecutor = null!;
        StreamStatus _streamStatus = null!;

        void Awake()
        {
            isExcitedButton.OnClick += () =>
            {
                _streamStatus.IsExcited = !_streamStatus.IsExcited;
                Debug.Log("IsExcited : " + _streamStatus.IsExcited);
            };

            isIrritatedButton.OnClick += () =>
            {
                _streamStatus.IsIrritated = !_streamStatus.IsIrritated;
                Debug.Log("IsIrritated : " + _streamStatus.IsIrritated);
            };


            daiPanButton.OnClick += () => { _daipanExecutor.DaiPan(); };
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