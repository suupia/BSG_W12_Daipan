#nullable enable
namespace Stream.Viewer.Scripts
{
    public class DaipanExecutor
    {
        readonly DaipanParameter _daipanParameter;
        readonly ViewerNumber _viewerNumber;
        readonly DistributionStatus _distributionStatus;

        public DaipanExecutor(
            DaipanParameter daipanParameter,
            ViewerNumber viewerNumber,
            DistributionStatus distributionStatus)
        {
            _daipanParameter = daipanParameter;
            _viewerNumber = viewerNumber;
            _distributionStatus = distributionStatus;
        }

        bool IsExciting => _distributionStatus.IsExciting;

        public void DaiPan()
        {
            if (IsExciting)
                _viewerNumber.IncreaseViewer(_daipanParameter.increaseNumberWhenExciting);
            else
                _viewerNumber.IncreaseViewer(_daipanParameter.increaseNumberByDaipan);
        }
    }
}