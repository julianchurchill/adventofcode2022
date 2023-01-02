using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace day4;

public class Day4Tests
{
    [Test]
    public void NonOverlappingRangesMeansZeroRangesContainTheOther()
    {
        CountFullyOverlappingRanges("1-1,2-2").Should().Be(0);
    }

    [TestCase("1-1,1-1")]
    [TestCase("11-11,11-11")]
    public void OneSimpleFullyOverlappingRangeMeansOneRangeContainsTheOther(string input)
    {
        CountFullyOverlappingRanges(input).Should().Be(1);
    }

    [TestCase("1-3,2-3")]
    [TestCase("2-3,1-3")]
    [TestCase("22-24,23-23")]
    public void OneComplexFullyOverlappingRangeMeansOneRangeContainsTheOther(string input)
    {
        CountFullyOverlappingRanges(input).Should().Be(1);
    }

    [TestCase("1-3,2-4")]
    [TestCase("2-4,1-3")]
    [TestCase("22-24,31-33")]
    public void OneComplexNonFullyOverlappingRangeMeansNoRangeContainsTheOther(string input)
    {
        CountFullyOverlappingRanges(input).Should().Be(0);
    }

    [Test]
    public void GoldenInputTestPart1()
    {
        var input = File.ReadAllText("../../../input.txt");
        input.Split("\r\n")
            .Select(CountFullyOverlappingRanges)
            .Sum()
            .Should().Be(453);
    }

    private int CountFullyOverlappingRanges(string input)
    {
        var elfPairs = input.Split(",");
        var firstElf = elfPairs[0];
        var secondElf = elfPairs[1];
        if (IsFullyOverlapping(firstElf, secondElf))
            return 1;
        return 0;
    }

    private static bool IsFullyOverlapping(string firstElf, string secondElf)
    {
        var (firstStart, firstEnd) = FindRange(firstElf);
        var (secondStart, secondEnd) = FindRange(secondElf);

        if (IsInclusivelyBetween(firstStart, secondStart, secondEnd) && IsInclusivelyBetween(firstEnd, secondStart, secondEnd))
            return true;
        if (IsInclusivelyBetween(secondStart, firstStart, firstEnd) && IsInclusivelyBetween(secondEnd, firstStart, firstEnd))
            return true;
        return false;
    }

    private static (int, int) FindRange(string firstElf)
    {
        string[] firstElfSplit = firstElf.Split("-");
        string firstElfStart = firstElfSplit[0];
        string firstElfEnd = firstElfSplit[1];
        var firstStart = int.Parse(firstElfStart);
        var firstEnd = int.Parse(firstElfEnd);
        return (firstStart, firstEnd);
    }

    private static bool IsInclusivelyBetween(int first, int start, int end)
    {
        return first <= end && first >= start;
    }
}