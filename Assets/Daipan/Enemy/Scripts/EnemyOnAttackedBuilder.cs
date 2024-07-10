#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Scripts
{
   public class EnemyOnAttackedBuilder
   {
       readonly IrritatedValue _irritatedValue;
       readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;
   
       public EnemyOnAttackedBuilder(
           IrritatedValue irritatedValue
           , EnemyLevelDesignParamData enemyLevelDesignParamData
       )
       {
           _irritatedValue = irritatedValue;
           _enemyLevelDesignParamData = enemyLevelDesignParamData;
       }
   
       public IEnemyOnAttacked SwitchEnemyOnAttacked(EnemyEnum enemyEnum)
       {
           if(enemyEnum.IsSpecial() == true) return new EnemySpecialOnAttacked(enemyEnum, _irritatedValue, _enemyLevelDesignParamData);
           if(enemyEnum == EnemyEnum.Totem2 || enemyEnum == EnemyEnum.Totem3) return BuildTotemOnAttack(enemyEnum);
           return new EnemyNormalOnAttacked();
       }
       
       EnemyTotemOnAttacked BuildTotemOnAttack(EnemyEnum enemyEnum)
       { 
           return enemyEnum switch {
               // todo : 一旦Viewとの兼ね合いで色を固定
               EnemyEnum.Totem2 => new EnemyTotemOnAttacked( new List<PlayerColor> { PlayerColor.Red, PlayerColor.Blue }),
               EnemyEnum.Totem3 => new EnemyTotemOnAttacked(new List<PlayerColor>(){ PlayerColor.Red, PlayerColor.Blue, PlayerColor.Yellow }),
               _ => throw new System.ArgumentException("Invalid totem type")
           };
       }
   
       static List<PlayerColor> RandomPlayerColor(int length)
       {
           var colors = new List<PlayerColor> { PlayerColor.Red, PlayerColor.Blue, PlayerColor.Yellow };
           colors.Shuffle();
           return colors.Take(length).ToList();
       }
   
   }
   public static class ListExtensions {
       /// <summary>
       /// Shuffles the element order of the specified list.
       /// </summary>
       public static void Shuffle<T>(this IList<T> list) {
           var count = list.Count;
           var last = count - 1;
           for (var i = 0; i < last; ++i) {
               var rand = UnityEngine.Random.Range(i, count);
               (list[i], list[rand]) = (list[rand], list[i]);
           }
       }
   } 
}

