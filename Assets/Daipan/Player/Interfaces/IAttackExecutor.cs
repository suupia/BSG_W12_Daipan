#nullable enable
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Interfaces
{
    public interface IAttackExecutor
    {
        public void SetPlayerViewMonos(List<AbstractPlayerViewMono?> playerViewMonos);
        public void FireAttackEffect(IMonoBehaviour playerMono, PlayerColor playerColor);
    }
}