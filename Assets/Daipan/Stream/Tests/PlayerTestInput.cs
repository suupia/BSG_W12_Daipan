#nullable enable
using Stream.Viewer.Scripts;
using UnityEngine;
using VContainer;

namespace Stream.Viewer.Tests
{
    public class PlayerTestInput : MonoBehaviour
    {
        [SerializeField] CustomButton isExcitedButton;
        [SerializeField] CustomButton isIrritatedButton;
        DaipanExecutor _daipanExecutor = null!;
        StreamStatus _streamStatus = null!;

        void Awake()
        {
            isExcitedButton.AddListener(() =>
            {
                _streamStatus.IsExciting = !_streamStatus.IsExciting;
                Debug.Log("IsExciting : " + _streamStatus.IsExciting);
            });

            isIrritatedButton.AddListener(() =>
            {
                _streamStatus.ExistIrrationalFactors = !_streamStatus.ExistIrrationalFactors;
                Debug.Log("ExistIrrationalFactors : " + _streamStatus.ExistIrrationalFactors);
            });
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) _daipanExecutor.DaiPan();
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