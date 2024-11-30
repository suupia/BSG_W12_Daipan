#nullable enable

using Daipan.Battle.Scripts;
using static Daipan.Battle.Scripts.ResultState;

namespace Daipan.Battle.interfaces
{
    public interface IResultState
    {
        public ResultEnum CurrentResultEnum { get; }
        public void ShowResult(bool isClear);
        public void ShowDetails();
    }
}