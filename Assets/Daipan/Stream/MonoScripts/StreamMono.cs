using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Stream.MonoScripts
{
    public sealed class StreamMono : MonoBehaviour
    {
        StreamStatus _streamStatus = null!;
        ViewerNumber _viewerNumber = null!;
        ViewerParam _viewerParam = null!;
        IrritatedValue _irritatedValue = null!;

        float OneSecTimer { get; set; }

        void Update()
        {
            OneSecTimer += Time.deltaTime;
            if (OneSecTimer > 1)
            {
                if (_streamStatus.IsIrritated)
                    _viewerNumber.DecreaseViewer(_viewerParam.decreaseNumberWhenIrradiated);
                else
                    _viewerNumber.IncreaseViewer(_viewerParam.increaseNumberPerSecond);
                

                OneSecTimer = 0;
            }

            if (_streamStatus.IsIrritated)
            {
                _irritatedValue.IncreaseValue(1/60.0f);
            }


        }

        [Inject]
        public void Initialize(
            ViewerParam viewerParam,
            ViewerNumber viewerNumber,
            IrritatedValue irritatedValue,
            StreamStatus streamStatus)
        {
            _viewerParam = viewerParam;
            _viewerNumber = viewerNumber;
            _irritatedValue = irritatedValue;
            _streamStatus = streamStatus;
        }
    }
}