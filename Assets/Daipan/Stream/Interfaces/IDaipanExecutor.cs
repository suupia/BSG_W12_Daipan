#nullable enable
namespace Daipan.Stream.Interfaces
{
    public interface IDaipanExecutor
    {
        public int DaipanCount { get; }
        public void Daipan();
    }
}