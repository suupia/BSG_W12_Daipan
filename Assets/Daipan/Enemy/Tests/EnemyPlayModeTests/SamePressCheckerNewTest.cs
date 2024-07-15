using System;
using System.Collections;
using System.Collections.Generic;
using Daipan.Enemy.Scripts;
using R3;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SamePressCheckerNewTests
{
    class Counter
    {
        public int Value { get; set; } = 10;
    }
    [UnityTest]
    public IEnumerator None()
    {
        var counter = new Counter(); 
        // Arrange
        var checker = new SamePressCheckerNew(1.0, 3, () => counter.Value++, () => counter.Value = 0);

        // Act
       checker.SetOn(0);
       checker.SetOn(1);

        // Assert
        Assert.AreEqual(10, counter.Value); 
        return  null;
    }

    [UnityTest]
    public IEnumerator Success()
    {
        var counter = new Counter(); 
        // Arrange
        var checker = new SamePressCheckerNew(1.0, 3, () => counter.Value++, () => counter.Value = 0);

        // Act
        checker.SetOn(0);
        checker.SetOn(1);
        checker.SetOn(2);

        // Assert
        Assert.AreEqual(11, counter.Value);
        return null;
    }
    [UnityTest]
    public IEnumerator Success2()
    {
        var counter = new Counter(); 
        // Arrange
        var checker = new SamePressCheckerNew(1.0, 3, () => counter.Value++, () => counter.Value = 0);

        // Act
        checker.SetOn(0);
        yield return new WaitForSeconds(0.2f);  
        checker.SetOn(1);
        yield return new WaitForSeconds(0.2f);
        checker.SetOn(2);

        // Assert
        Assert.AreEqual(11, counter.Value);
    }
    
    [UnityTest]
    public IEnumerator Failure()
    {
        var counter = new Counter(); 
        // Arrange
        var checker = new SamePressCheckerNew(1.0, 3, () => counter.Value++, () => counter.Value = 0);

        // Act
        checker.SetOn(0);
        checker.SetOn(0);

        // Assert
        Assert.AreEqual(0, counter.Value);
        return null;
    }

    [UnityTest]
    public IEnumerator Failure2()
    {
        var counter = new Counter(); 
        // Arrange
        var checker = new SamePressCheckerNew(1.0, 3, () => counter.Value++, () => counter.Value = 0);

        // Act
        checker.SetOn(0);
        yield return new WaitForSeconds(0.2f);
        checker.SetOn(0);

        // Assert
        Assert.AreEqual(0, counter.Value);
    }

   
}
