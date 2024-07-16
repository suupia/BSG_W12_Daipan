using System;
using System.Collections;
using System.Collections.Generic;
using Daipan.Enemy.Scripts;
using R3;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SamePressCheckerTests
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
        var checker = new SamePressChecker(1.0, 3, () => counter.Value++, () => counter.Value = 0);

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
        var checker = new SamePressChecker(1.0, 3, () => counter.Value++, () => counter.Value = 0);

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
        var checker = new SamePressChecker(1.0, 3, () => counter.Value++, () => counter.Value = 0);

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
        var checker = new SamePressChecker(1.0, 3, () => counter.Value++, () => counter.Value = 0);

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
        var checker = new SamePressChecker(1.0, 3, () => counter.Value++, () => counter.Value = 0);

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
        var checker = new SamePressChecker(1.0, 3, () => counter.Value++, () => counter.Value = 0);

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
        var checker = new SamePressChecker(1.0, 3, () => counter.Value++, () => counter.Value = 0);

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
        var checker = new SamePressChecker(1.0, 3, () => counter.Value++, () => counter.Value = 0);

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
        var checker = new SamePressChecker(1.0, 3, () => counter.Value++, () => counter.Value = 0);

        // Act
        checker.SetOn(0);
        yield return new WaitForSeconds(0.8f);
        checker.SetOn(1);
        yield return new WaitForSeconds(0.1f);
        checker.SetOn(2);

        // Assert
        Assert.AreEqual(11, counter.Value);
    }

    [UnityTest]
    public IEnumerator SuccessWhenAllButtonsPressedBeforeTimeoutAndValueRemainsConstant()
    {
        var counter = new Counter();
        // Arrange
        var checker = new SamePressChecker(1.0, 3, () => counter.Value++, () => counter.Value = 0);

        // Act
        checker.SetOn(0);
        yield return new WaitForSeconds(0.8f);
        checker.SetOn(1);
        yield return new WaitForSeconds(0.1f);
        checker.SetOn(2);

        // Assert
        Assert.AreEqual(11, counter.Value);

        // Act
        yield return new WaitForSeconds(0.2f);

        // Assert
        Assert.AreEqual(11, counter.Value);
    }

    [UnityTest]
    public IEnumerator IsAllOn_ShouldReturnFalse_WhenInitialized_SamePressCheckerNew()
    {
        // Arrange
        var checker = new SamePressChecker(1.0, 3, () => { }, () => { });

        // Act
        var result = checker.IsAllOn();

        // Assert
        Assert.False(result);
        return null;
    }

    [UnityTest]
    public IEnumerator IsAllOn_ShouldReturnTrue_WhenAllFlagsAreOn_SamePressCheckerNew()
    {
        // Arrange
        var checker = new SamePressChecker(1.0, 3, () => { }, () => { });

        // Act
        checker.SetOn(0);
        checker.SetOn(1);
        checker.SetOn(2);
        var result = checker.IsAllOn();

        // Assert
        Assert.True(result);
        return null;
    }

    [UnityTest]
    public IEnumerator IsAllOn_ShouldReturnFalse_WhenOneFlagTurnsOffAfterAllowableSec_SamePressCheckerNew()
    {
        // Arrange
        var checker = new SamePressChecker(1.0, 3, () => { }, () => { });
        checker.SetOn(0);
        checker.SetOn(1);
        checker.SetOn(2);

        // Act
        yield return new WaitForSeconds(1.5f);
        var result = checker.IsAllOn();

        // Assert
        Assert.False(result);
    }

    [UnityTest]
    public IEnumerator IsOn_ShouldResetTimer_WhenCalledAgainBeforeExpiration_SamePressCheckerNew()
    {
        // Arrange
        var checker = new SamePressChecker(1.0, 1, () => { }, () => { });

        // Act
        checker.SetOn(0);
        yield return new WaitForSeconds(0.5f); // Wait for less than allowableSec
        checker.SetOn(0); // Reset the timer
        yield return new WaitForSeconds(0.6f); // Wait for less than allowableSec
        var result = checker.IsAllOn();

        // Assert
        Assert.True(result); // The timer should have been reset, so the flag should still be true
    }
}