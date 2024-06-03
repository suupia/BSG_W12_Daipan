#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Daipan.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Utility.Scripts
{
    public static class EnumEnumerationChecker
    {
        public static bool CheckEnum<TEnum,TEnumeration>() 
            where TEnum : Enum
            where TEnumeration : Enumeration
        {
            foreach (var type in Enum.GetValues(typeof(TEnum)).Cast<TEnum>())
            {
                var enumeration = Enumeration.GetAll<TEnumeration>().FirstOrDefault(x => x.Name == type.ToString());
                if (enumeration == null)
                {
                    Debug.LogWarning($"{typeof(TEnumeration)} with name {type.ToString()} not found.");
                    return false;
                }

                if (enumeration.Equals(default(EnemyEnum)))
                {
                    Debug.LogWarning($"EnemyEnum with name {type.ToString()} not found.");
                    return false;
                }
            }

            return true;
        }
    }
}
