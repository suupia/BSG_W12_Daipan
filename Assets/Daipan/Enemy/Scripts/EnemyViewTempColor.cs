#nullable enable
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public static class EnemyViewTempColor
    {
        public static Color GetTempColor(EnemyEnum enemyEnum)
        {
            return enemyEnum switch
            {
                EnemyEnum.Red => Color.red,
                EnemyEnum.Blue => Color.blue,
                EnemyEnum.Yellow => Color.yellow,
                EnemyEnum.RedBoss => Color.Lerp(Color.red, Color.black, 0.5f), // 半分暗くする
                EnemyEnum.SpecialRed => Color.Lerp(Color.red, Color.yellow, 0.5f), // 赤と黄色の中間色
                EnemyEnum.SpecialBlue => Color.Lerp(Color.blue, Color.yellow, 0.5f), // 青と黄色の中間色
                EnemyEnum.SpecialYellow => Color.Lerp(Color.yellow, Color.red, 0.5f), // 黄色と赤の中間色
                _ => Color.white
            }; 
        }
    } 
}

