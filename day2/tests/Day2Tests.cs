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
    public void DrawsAreTheValueOfTheItemPlayedPlus3(char theirPlay, char ourPlay, int expectedScore)
    {
        CalculateTotalScore($"{theirPlay} {ourPlay}").Should().Be(expectedScore);
    }

    [TestCase(RockThem, ScissorsUs, 3)]
    [TestCase(PaperThem, RockUs, 1)]
    [TestCase(ScissorsThem, PaperUs, 2)]
    public void ALossIsWorthTheItemPlayed(char theirPlay, char ourPlay, int expectedScore)
    {
        CalculateTotalScore($"{theirPlay} {ourPlay}").Should().Be(expectedScore);
    }

    [TestCase(RockThem, PaperUs, 8)]
    [TestCase(PaperThem, ScissorsUs, 9)]
    [TestCase(ScissorsThem, RockUs, 7)]
    public void AWinIsWorthTheItemPlayedPlus6(char theirPlay, char ourPlay, int expectedScore)
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

    private const char RockThem = 'A';
    private const char PaperThem = 'B';
    private const char ScissorsThem = 'C';
    private const char RockUs = 'X';
    private const char PaperUs = 'Y';
    private const char ScissorsUs = 'Z';
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

    private enum RoShamBo 
    {
        Rock,
        Paper,
        Scissors
    }

    private RoShamBo ToRoShamBo(char item)
    {
        if(item == RockThem || item == RockUs) return RoShamBo.Rock;
        if(item == PaperThem || item == PaperUs) return RoShamBo.Paper;
        if(item == ScissorsThem || item == ScissorsUs) return RoShamBo.Scissors;
        return RoShamBo.Rock;
    }

    private int GetWinLossDrawValue(string input)
    {
        RoShamBo theirItem = ToRoShamBo(input.First());
        RoShamBo ourItem = ToRoShamBo(input.Last());
        if (theirItem == ourItem)
            return DrawValue;
        if (theirItem == RoShamBo.Rock && ourItem == RoShamBo.Scissors)
            return LossValue;
        if(theirItem == RoShamBo.Paper && ourItem == RoShamBo.Rock)
            return LossValue;
        if(theirItem == RoShamBo.Scissors && ourItem == RoShamBo.Paper)
            return LossValue;
        if(theirItem == RoShamBo.Rock && ourItem == RoShamBo.Paper)
            return WinValue;
        if(theirItem == RoShamBo.Paper && ourItem == RoShamBo.Scissors)
            return WinValue;
        if(theirItem == RoShamBo.Scissors && ourItem == RoShamBo.Rock)
            return WinValue;
        return 0;
    }

    private int GetValueOfOurPlayedItem(string input)
    {
        RoShamBo ourItem = ToRoShamBo(input.Last());
        if(ourItem == RoShamBo.Rock)
            return RockValue;
        if(ourItem == RoShamBo.Paper)
            return PaperValue;
        if(ourItem == RoShamBo.Scissors)
            return ScissorsValue;
        return 0;
    }
}