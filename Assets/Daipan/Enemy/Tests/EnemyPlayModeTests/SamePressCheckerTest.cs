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
    [UnityTest]
    public IEnumerator IsAllOn_ShouldReturnFalse_WhenInitialized()
    {
        // Arrange
        var checker = new SamePressChecker(1.0, 3);

        // Act
        var result = checker.IsAllOn();

        // Assert
        Assert.False(result);
        return  null;
    }

    [UnityTest]
    public IEnumerator IsAllOn_ShouldReturnTrue_WhenAllFlagsAreOn()
    {
        // Arrange
        var checker = new SamePressChecker(1.0, 3);

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
    public IEnumerator IsAllOn_ShouldReturnFalse_WhenOneFlagTurnsOffAfterAllowableSec()
    {
        // Arrange
        var checker = new SamePressChecker(1.0, 3);
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
    public IEnumerator IsOn_ShouldResetTimer_WhenCalledAgainBeforeExpiration()
    {
        // Arrange
        var checker = new SamePressChecker(1.0, 1);

        // Act
        checker.SetOn(0);
        System.Threading.Thread.Sleep(500); // Sleep for less than allowableSec
        checker.SetOn(0); // Reset the timer
        System.Threading.Thread.Sleep(600); // Sleep for less than allowableSec
        var result = checker.IsAllOn();

        // Assert
        Assert.True(result); // The timer should have been reset, so the flag should still be true
        return null;
    }

    [UnityTest]
    public IEnumerator Dispose_ShouldDisposeAllDisposables()
    {
        // Arrange
        var checker = new SamePressChecker(1.0, 3);
        checker.SetOn(0);
        checker.SetOn(1);
        checker.SetOn(2);

        // Act
        checker.Dispose();

        // Assert
        // There is no easy way to directly check if disposables are disposed, 
        // but we can ensure that calling IsOn again does not throw any exceptions.
        Exception caughtException = null;
        try
        {
            checker.SetOn(0);
        }
        catch (Exception ex)
        {
            caughtException = ex;
        }

        Assert.IsNull(caughtException);
        yield return null;
    }
}
