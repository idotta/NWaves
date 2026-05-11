using System;
using System.Linq;
using NUnit.Framework;
using NWaves.Transforms;

namespace NWaves.Tests.TransformTests;

[TestFixture]
public class TestStft
{
    [Test]
    public void TestOutputSizeWhenMatchesWindowSize()
    {
        var windowSize = 1024;
        var hopSize = 256;
        var stft = new Stft(windowSize, hopSize);
        var input = new float[windowSize];

        var output = stft.Direct(input);
        Assert.That(output.Count, Is.EqualTo(1));
    }

    [Test]
    public void TestOutputSizeWhenDoesNotMatchWindowSize()
    {
        var windowSize = 1024;
        var hopSize = 256;
        var  stft = new Stft(windowSize, hopSize);
        var input = new float[windowSize + 1];
        input[windowSize] = 1;
        var output = stft.Direct(input);

        Assert.That(output.Count, Is.EqualTo(2));
        Assert.That(output[1].Item1.Sum(x => x * x) + output[1].Item2.Sum(x => x * x), Is.GreaterThan(0));
    }

    [Test]
    public void TestOutputSizeWhenInputShorterThanWindow()
    {
        var windowSize = 1024;
        var hopSize = 256;
        var stft = new Stft(windowSize, hopSize);
        var input = new float[windowSize - 1];
        var output = stft.Direct(input);

        Assert.That(output.Count, Is.EqualTo(1));
    }

    [Test]
    public void TestOutputSizeZeroWhenInputSizeZero()
    {
        var windowSize = 1024;
        var hopSize = 255;
        var stft = new Stft(windowSize, hopSize);
        var input = new float[0];
        var output = stft.Direct(input);

        Assert.That(output.Count, Is.EqualTo(0));
    }
}
