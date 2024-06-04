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
            if(Enum.GetValues(typeof(TEnum)).Length != Enumeration.GetAll<TEnumeration>().Count())
            {
                Debug.LogWarning($"The number of {typeof(TEnum)} and {typeof(TEnumeration)} is different.");
                return false;
            }
            
            foreach (var type in Enum.GetValues(typeof(TEnum)).Cast<TEnum>())
            {
                var enumeration = Enumeration.GetAll<TEnumeration>().FirstOrDefault(x => x.Name == type.ToString());
                if (enumeration == null)
                {
                    Debug.LogWarning($"{typeof(TEnumeration)} with name {type.ToString()} not found.");
                    return false;
                }
            }

            return true;
        }
    }
}
