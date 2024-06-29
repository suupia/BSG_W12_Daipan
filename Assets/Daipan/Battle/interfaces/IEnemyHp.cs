#nullable enable

namespace Daipan.Battle.interfaces
{
    public interface IEnemyHp
    {
        int MaxHp { get; }
        int CurrentHp { get; }
        void DecreaseHp(int DamageValue);
    }
}