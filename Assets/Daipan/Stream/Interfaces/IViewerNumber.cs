namespace Daipan.Stream.Interfaces
{
    public interface IViewerNumber
    {
        public int Number { get; }
        public int Difference { get; }
        public void IncreaseViewer(int amount);
        public void DecreaseViewer(int amount);
        public void SetViewer(int amount);
    }
}