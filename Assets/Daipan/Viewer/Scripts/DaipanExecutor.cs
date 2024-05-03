#nullable enable
namespace Daipan.Viewer.Scripts
{
    public class DaipanExecutor
    {
        readonly DaipanParameter _daipanParameter;
        readonly ViewerNumber _viewerNumber;
        readonly ViewerStatus _viewerStatus;

        public DaipanExecutor(
            DaipanParameter daipanParameter,
            ViewerNumber viewerNumber,
            ViewerStatus viewerStatus)
        {
            _daipanParameter = daipanParameter;
            _viewerNumber = viewerNumber;
            _viewerStatus = viewerStatus;
        }

        bool IsExciting => _viewerStatus.IsExciting;

        public void DaiPan()
        {
            if (IsExciting)
                _viewerNumber.IncreaseViewer(_daipanParameter.increaseNumberWhenExciting);
            else
                _viewerNumber.IncreaseViewer(_daipanParameter.increaseNumberByDaipan);
        }
    }
}