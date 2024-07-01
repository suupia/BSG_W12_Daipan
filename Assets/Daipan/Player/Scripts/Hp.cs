#nullable enable
namespace Daipan.Player.Scripts
{
    public record Hp
    {
        public int Value { get; private set; }

        public Hp(int value)
        {
            Value = value;
        }

    }
}