#nullable enable
namespace Daipan.Player.Scripts
{
    public record PlayerHpNew
    {
        public int Hp { get; private set; }

        public PlayerHpNew(int hp)
        {
            Hp = hp;
        }

    }
}