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
            var ratios = new [] { 1.0, 2.0, 3.0 };
           
            var accumulatedRatios = ratios.Scan(0.0, (acc, ratio ) => acc + ratio).ToArray();
            Debug.Log($"accumulatedRatios: {string.Join(",",accumulatedRatios)}");
            Assert.AreEqual( new [] { 0.0, 1.0, 3.0, 6.0 }, accumulatedRatios);
        }

        [Test]
        public void RandomByRatiosTest1()
        {
            // Arrange
            var ratios = new List<double> { 1.0, 2.0, 3.0 };

            var index = Randoms.RandomByRatios(ratios, 0.0);
            Assert.AreEqual(0, index);

            var index2 = Randoms.RandomByRatios(ratios, 0.5);
            Assert.AreEqual(1, index2);

            var index3 = Randoms.RandomByRatios(ratios, 0.9);
            Assert.AreEqual(2, index3);

            var index4 = Randoms.RandomByRatios(ratios, 1.0);
            Assert.AreEqual(-1, index4);
        }

    }
}