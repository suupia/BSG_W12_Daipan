#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Extensions;
using NUnit.Framework;
using Daipan.Utility.Scripts;
using UnityEngine;

namespace Daipan.Utility.Tests
{
    public class RandomByRatiosTest
    {
        [Test]
        public void AccumulateRatiosTest1()
        {
            var ratios = new[] { 1.0, 2.0, 3.0 };

            var accumulatedRatios = ratios.Scan(0.0, (acc, ratio) => acc + ratio).ToArray();
            Debug.Log($"accumulatedRatios: {string.Join(",", accumulatedRatios)}");
            Assert.AreEqual(new[] { 0.0, 1.0, 3.0, 6.0 }, accumulatedRatios);
        }

        [Test]
        public void RandomByRatiosTest1()
        {
            // Arrange
            var ratios = new List<double> { 0.2, 0.2, 0.6 };

            // Act and Assert
            var index0 = Randoms.RandomByRatios(ratios, 0.0);
            Assert.AreEqual(0, index0);

            var index1 = Randoms.RandomByRatios(ratios, 0.1);
            Assert.AreEqual(0, index1);

            var index2 = Randoms.RandomByRatios(ratios, 0.2);
            Assert.AreEqual(1, index2);

            var index3 = Randoms.RandomByRatios(ratios, 0.3);
            Assert.AreEqual(1, index3);

            var index4 = Randoms.RandomByRatios(ratios, 0.4);
            Assert.AreEqual(2, index4);

            var index5 = Randoms.RandomByRatios(ratios, 0.5);
            Assert.AreEqual(2, index5);

            var index6 = Randoms.RandomByRatios(ratios, 0.6);
            Assert.AreEqual(2, index6);

            var index7 = Randoms.RandomByRatios(ratios, 0.7);
            Assert.AreEqual(2, index7);

            var index8 = Randoms.RandomByRatios(ratios, 0.8);
            Assert.AreEqual(2, index8);

            var index9 = Randoms.RandomByRatios(ratios, 0.99);
            Assert.AreEqual(2, index9);

            var index10 = Randoms.RandomByRatios(ratios, 1.0);
            Assert.AreEqual(-1, index10);
        }


    }
}