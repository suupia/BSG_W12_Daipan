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
        [IsSpecial(true)] SpecialRed,
        [IsSpecial(true)] SpecialBlue,
        [IsSpecial(true)] SpecialYellow,
        [IsTotem(true)]Totem2,
        [IsTotem(true)]Totem3,
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
        
        public static bool? IsTotem(this EnemyEnum self)
        {
            var fieldInfo = self.GetType().GetField(self.ToString());
            return fieldInfo?.GetCustomAttribute<IsTotemAttribute>()?.IsTotem;
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
    
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    internal class IsTotemAttribute : Attribute
    {
        public bool IsTotem { get; }

        public IsTotemAttribute(bool isTotem) : base()
        {
            IsTotem = isTotem;
        }
    }
}