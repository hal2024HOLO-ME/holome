using System;
using System.Collections;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CheckSleepTimeTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void CheckSleepTimeTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CheckSleepTimeTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return null;
    }

    [Category("AQ‚Ì‹«ŠE’lƒeƒXƒg")]
    [UnityTest]
    public IEnumerator ‡–°ŠÔˆÈ“à‚ÌÅ‰‚ÌuŠÔ‚È‚çtrue‚ğ•Ô‚·()
    {
        DateTime now = SetDateTime(new DateTime(2023, 11, 23, 0, 0, 0));
        HealthMonitor healthMonitor = new(now);
        bool isSleepTime = healthMonitor.CheckSleepTime();

        Assert.IsTrue(isSleepTime);
        yield return null;
    }

    [Category("AQ‚Ì‹«ŠE’lƒeƒXƒg")]
    [UnityTest]
    public IEnumerator ‡–°ŠÔˆÈ“à‚ÌÅŒã‚ÌuŠÔ‚È‚çtrue‚ğ•Ô‚·()
    {
        DateTime now = SetDateTime(new DateTime(2023, 11, 23, 6, 59, 59));
        HealthMonitor healthMonitor = new(now);
        bool isSleepTime = healthMonitor.CheckSleepTime();

        Assert.IsTrue(isSleepTime);
        yield return null;
    }

    [Category("AQ‚Ì‹«ŠE’lƒeƒXƒg")]
    [UnityTest]
    public IEnumerator ‡–°ŠÔŠO‚ÌÅ‰‚ÌuŠÔ‚È‚çfalse‚ğ•Ô‚·()
    {
        DateTime now = SetDateTime(new DateTime(2023, 11, 22, 23, 59, 59));
        HealthMonitor healthMonitor = new(now);
        bool isSleepTime = healthMonitor.CheckSleepTime();

        Assert.IsFalse(isSleepTime);
        yield return null;
    }

    [Category("AQ‚Ì‹«ŠE’lƒeƒXƒg")]
    [UnityTest]
    public IEnumerator ‡–°ŠÔŠO‚ÌÅŒã‚ÌuŠÔ‚È‚çfalse‚ğ•Ô‚·()
    {
        DateTime now = SetDateTime(new DateTime(2023, 11, 23, 7, 0, 1));
        HealthMonitor healthMonitor = new(now);
        bool isSleepTime = healthMonitor.CheckSleepTime();

        Assert.IsFalse(isSleepTime);
        yield return null;
    }

    private DateTime SetDateTime(DateTime dateTime)
    {
        var mockDateTimeWrapper = new Mock<IMockableDateTimeWrapper>();
        mockDateTimeWrapper.Setup(wrapper => wrapper.Now).Returns(dateTime);

        DateTime now = mockDateTimeWrapper.Object.Now;
        return now;
    }
}