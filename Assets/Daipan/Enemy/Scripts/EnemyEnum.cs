#nullable enable
using System;
using System.Reflection;

namespace Daipan.Enemy.Scripts
{
    public enum EnemyEnum
    {
        None,
        Red,
        Blue,
        Yellow,
        [IsBoss(true)] RedBoss,
        [IsBoss(true)] BlueBoss,
        [IsBoss(true)] YellowBoss,
        [IsSpecial(true)] Special
    }

    public static class AnyTypesExtensions
    {
        public static bool? IsBoss(this EnemyEnum self)
        {
            var fieldInfo = self.GetType().GetField(self.ToString());
            return fieldInfo?.GetCustomAttribute<IsBossAttribute>()?.IsBoss;
        }

        public static bool? IsSpecial(this EnemyEnum self)
        {
            var fieldInfo = self.GetType().GetField(self.ToString());
            return fieldInfo?.GetCustomAttribute<IsSpecialAttribute>()?.IsSpecial;
        }
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    internal class IsBossAttribute : Attribute
    {
        public bool IsBoss { get; }

        public IsBossAttribute(bool isBoss) : base()
        {
            IsBoss = isBoss;
        }
    }


    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    internal class IsSpecialAttribute : Attribute
    {
        public bool IsSpecial { get; }

        public IsSpecialAttribute(bool isSpecial) : base()
        {
            IsSpecial = isSpecial;
        }
    }
}