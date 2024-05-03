#nullable enable
namespace Daipan.Viewer.Scripts
{
    public class DaipanExecutor
    {
        readonly DaipanParameter _daipanParameter;
        readonly ViewerNumber _viewerNumber;
        readonly ViewerStatus _viewerStatus;

        public DaipanExecutor(DaipanParameter daipanParameter, ViewerNumber viewerNumber)
        {
            _daipanParameter = daipanParameter;
            _viewerNumber = viewerNumber;
        }

        bool IsExciting => _viewerStatus.IsExciting;

        public void DaiPan()
        {
            if (IsExciting)
                _viewerNumber.IncreaseViewer(_daipanParameter.increaseNumberByDaipan * 2);
            else
                _viewerNumber.IncreaseViewer(_daipanParameter.increaseNumberByDaipan);
        }
    }
}