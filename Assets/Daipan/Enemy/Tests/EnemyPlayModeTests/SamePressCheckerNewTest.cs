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
    public IEnumerator FailureWhenButtonPressedAfterTimeout()
    {
        var counter = new Counter();
        // Arrange
        var checker = new SamePressCheckerNew(1.0, 3, () => counter.Value++, () => counter.Value = 0);

        // Act
        checker.SetOn(0);
        yield return new WaitForSeconds(1.1f);
        checker.SetOn(1);

        // Assert
        Assert.AreEqual(0, counter.Value);
    }

    [UnityTest]
    public IEnumerator FailureWhenNotAllButtonsPressedInTime()
    {
        var counter = new Counter();
        // Arrange
        var checker = new SamePressCheckerNew(1.0, 3, () => counter.Value++, () => counter.Value = 0);

        // Act
        checker.SetOn(0);
        yield return new WaitForSeconds(1.1f);
        checker.SetOn(1);
        yield return new WaitForSeconds(0.2f);
        checker.SetOn(2);

        // Assert
        Assert.AreEqual(0, counter.Value);
    }

    [UnityTest]
    public IEnumerator FailureWhenSameButtonIsPressedTwice()
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
    public IEnumerator FailureWhenSameButtonIsPressedTwiceWithShortDelay()
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

    [UnityTest]
    public IEnumerator NoSuccessWhenNotAllButtonsArePressed()
    {
        var counter = new Counter();
        // Arrange
        var checker = new SamePressCheckerNew(1.0, 3, () => counter.Value++, () => counter.Value = 0);

        // Act
        checker.SetOn(0);
        checker.SetOn(1);

        // Assert
        Assert.AreEqual(10, counter.Value);
        return null;
    }

    [UnityTest]
    public IEnumerator SuccessWhenAllButtonsArePressed()
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
    public IEnumerator SuccessWhenAllButtonsArePressedWithDelay()
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
    public IEnumerator SuccessWhenAllButtonsPressedBeforeTimeout()
    {
        var counter = new Counter();
        // Arrange
        var checker = new SamePressCheckerNew(1.0, 3, () => counter.Value++, () => counter.Value = 0);

        // Act
        checker.SetOn(0);
        yield return new WaitForSeconds(0.4f);
        checker.SetOn(1);
        yield return new WaitForSeconds(0.5f);
        checker.SetOn(2);

        // Assert
        Assert.AreEqual(11, counter.Value);
    }

    [UnityTest]
    public IEnumerator SuccessWhenRemainingButtonsPressedBeforeTimeout()
    {
        var counter = new Counter();
        // Arrange
        var checker = new SamePressCheckerNew(1.0, 3, () => counter.Value++, () => counter.Value = 0);

        // Act
        checker.SetOn(0);
        yield return new WaitForSeconds(0.8f);
        checker.SetOn(1);
        yield return new WaitForSeconds(0.1f);
        checker.SetOn(2);

        // Assert
        Assert.AreEqual(11, counter.Value);
    }
}