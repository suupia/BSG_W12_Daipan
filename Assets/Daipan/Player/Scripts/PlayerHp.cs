#nullable enable
using Daipan.Battle.interfaces;

namespace Daipan.Player.Scripts
{
    public class PlayerHp : IHpSetter
    {
        public int CurrentHp { get; set; }
    }
}