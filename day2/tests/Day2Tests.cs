using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace day2;

public class Day2Tests
{
    [Test]
    public void NoInputMeansZeroScore()
    {
        CalculateTotalScore("").Should().Be(0);
    }

    [TestCase(RockThem, RockUs, 4)]
    [TestCase(PaperThem, PaperUs, 5)]
    [TestCase(ScissorsThem, ScissorsUs, 6)]
    public void DrawsAreTheValueOfTheItemPlayedPlus3(string theirPlay, string ourPlay, int expectedScore)
    {
        CalculateTotalScore($"{theirPlay} {ourPlay}").Should().Be(expectedScore);
    }

    [TestCase(RockThem, ScissorsUs, 3)]
    [TestCase(PaperThem, RockUs, 1)]
    [TestCase(ScissorsThem, PaperUs, 2)]
    public void ALossIsWorthTheItemPlayed(string theirPlay, string ourPlay, int expectedScore)
    {
        CalculateTotalScore($"{theirPlay} {ourPlay}").Should().Be(expectedScore);
    }

    [TestCase(RockThem, PaperUs, 8)]
    [TestCase(PaperThem, ScissorsUs, 9)]
    [TestCase(ScissorsThem, RockUs, 7)]
    public void AWinIsWorthTheItemPlayedPlus6(string theirPlay, string ourPlay, int expectedScore)
    {
        CalculateTotalScore($"{theirPlay} {ourPlay}").Should().Be(expectedScore);
    }

    [Test]
    public void GoldenInputTestPart1()
    {
        var input = File.ReadAllText("../../../input.txt");
        input
            .Split("\r\n")
            .Select(x => CalculateTotalScore(x))
            .Sum()
            .Should().Be(12156);
    }

    private const string RockThem = "A";
    private const string PaperThem = "B";
    private const string ScissorsThem = "C";
    private const string RockUs = "X";
    private const string PaperUs = "Y";
    private const string ScissorsUs = "Z";
    private const int RockValue = 1;
    private const int PaperValue = 2;
    private const int ScissorsValue = 3;
    private const int LossValue = 0;
    private const int DrawValue = 3;
    private const int WinValue = 6;

    private int CalculateTotalScore(string input)
    {
        if(string.IsNullOrEmpty(input))
            return 0;

        return GetValueOfOurPlayedItem(input) + GetWinLossDrawValue(input);
    }

    private int GetWinLossDrawValue(string input)
    {
        if(input.StartsWith(RockThem) && input.EndsWith(ScissorsUs))
            return LossValue;
        if(input.StartsWith(RockThem) && input.EndsWith(PaperUs))
            return WinValue;
        if(input.StartsWith(RockThem) && input.EndsWith(RockUs))
            return DrawValue;
        if(input.StartsWith(PaperThem) && input.EndsWith(RockUs))
            return LossValue;
        if(input.StartsWith(PaperThem) && input.EndsWith(ScissorsUs))
            return WinValue;
        if(input.StartsWith(PaperThem) && input.EndsWith(PaperUs))
            return DrawValue;
        if(input.StartsWith(ScissorsThem) && input.EndsWith(PaperUs))
            return LossValue;
        if(input.StartsWith(ScissorsThem) && input.EndsWith(RockUs))
            return WinValue;
        if(input.StartsWith(ScissorsThem) && input.EndsWith(ScissorsUs))
            return DrawValue;
        return 0;
    }

    private int GetValueOfOurPlayedItem(string input)
    {
        if(input.EndsWith(RockUs))
            return RockValue;
        if(input.EndsWith(PaperUs))
            return PaperValue;
        if(input.EndsWith(ScissorsUs))
            return ScissorsValue;
        return 0;
    }
}