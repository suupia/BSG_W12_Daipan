#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Daipan.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Utility.Scripts
{
    static class EnumEnumerationChecker
    {
        static readonly HashSet<Type> CheckedEnums = new(); 
        public static void CheckEnum<TEnum,TEnumeration>() 
            where TEnum : Enum
            where TEnumeration : Enumeration
        {
            if (CheckedEnums.Contains(typeof(TEnum)))
                return;
            foreach (var type in Enum.GetValues(typeof(TEnum)).Cast<TEnum>())
            {
                var enumeration = Enumeration.GetAll<TEnumeration>().FirstOrDefault(x => x.Name == type.ToString());
                if (enumeration == null)
                {
                    Debug.LogWarning($"{typeof(TEnumeration)} with name {type.ToString()} not found.");
                    break;
                }
                if (enumeration.Equals(default(EnemyEnum)))
                    Debug.LogWarning($"EnemyEnum with name {type.ToString()} not found.");
            }

            CheckedEnums.Add(typeof(TEnum)); 
        }
    }
}
