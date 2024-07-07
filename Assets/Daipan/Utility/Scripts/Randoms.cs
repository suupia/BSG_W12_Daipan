using Daipan.LevelDesign.Enemy.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Daipan.Extensions;
using UnityEngine;

namespace Daipan.Utility.Scripts
{
    public static class Randoms
    {
        /// <summary>
        /// 割合に応じてランダムなインデックスを返す。
        /// </summary>
        /// <param name="ratios">各要素の割合が入ったリスト。</param>
        /// <param name="random">[0,1)の範囲のランダムな値。</param>
        public static int RandomByRatios(List<double> ratios, double random)
        {
            // [Precondition]
            if (!ratios.Any())
            {
                Debug.LogWarning("ratios must not be empty.");
                return -1;
            }
            
            if (random is < 0 or >= 1)
            {
                Debug.LogWarning("rand must be between 0 and 1.");
                return -1;
            }

            if (ratios.Sum() <= 0)
            {
                Debug.LogWarning("Sum of ratios must be greater than 0.");
                return -1;
            }

            var accumulatedRatios = ratios.Scan(0.0, (acc, ratio) => acc + ratio).ToList();
            var normalizedAccumulatedRatios =
                accumulatedRatios.Select(ratio => ratio / accumulatedRatios.Last()).ToList();
            var result = normalizedAccumulatedRatios.Select((ratio, index) => new { ratio, index })
                .FirstOrDefault(pair => random < pair.ratio);
            // Debug.Log($"normalizedAccumulatedRatios: {string.Join(",", normalizedAccumulatedRatios)}, random: {random}, result: {result}");
            if (result == null)
            {
                Debug.LogWarning(
                    $"Failed to get random index. normalizedAccumulatedRatios: {string.Join(",", normalizedAccumulatedRatios)}, random: {random}");
                return -1;
            }

            return result.index - 1;
        }
    }
}