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

    // [TestCase(RockThem, LoseIndicator, 4)]
    // [TestCase(PaperThem, DrawIndicator, 5)]
    // [TestCase(ScissorsThem, WinIndicator, 6)]
    // public void DrawsAreTheValueOfTheItemPlayedPlus3(char theirPlay, char ourPlay, int expectedScore)
    // {
    //     CalculateTotalScore($"{theirPlay} {ourPlay}").Should().Be(expectedScore);
    // }

    // [TestCase(RockThem, WinIndicator, 3)]
    // [TestCase(PaperThem, LoseIndicator, 1)]
    // [TestCase(ScissorsThem, DrawIndicator, 2)]
    // public void ALossIsWorthTheItemPlayed(char theirPlay, char ourPlay, int expectedScore)
    // {
    //     CalculateTotalScore($"{theirPlay} {ourPlay}").Should().Be(expectedScore);
    // }

    // [TestCase(RockThem, DrawIndicator, 8)]
    // [TestCase(PaperThem, WinIndicator, 9)]
    // [TestCase(ScissorsThem, LoseIndicator, 7)]
    // public void AWinIsWorthTheItemPlayedPlus6(char theirPlay, char ourPlay, int expectedScore)
    // {
    //     CalculateTotalScore($"{theirPlay} {ourPlay}").Should().Be(expectedScore);
    // }

    // [Test]
    // public void GoldenInputTestPart1()
    // {
    //     var input = File.ReadAllText("../../../input.txt");
    //     input
    //         .Split("\r\n")
    //         .Select(x => CalculateTotalScore(x))
    //         .Sum()
    //         .Should().Be(12156);
    // }


    [TestCase(RockThem, DrawIndicator, 4)]
    [TestCase(PaperThem, DrawIndicator, 5)]
    [TestCase(ScissorsThem, DrawIndicator, 6)]
    public void DrawsAreTheValueOfTheItemPlayedPlus3(char theirPlay, char ourPlay, int expectedScore)
    {
        CalculateTotalScore($"{theirPlay} {ourPlay}").Should().Be(expectedScore);
    }

    [TestCase(RockThem, LoseIndicator, 3)]
    [TestCase(PaperThem, LoseIndicator, 1)]
    [TestCase(ScissorsThem, LoseIndicator, 2)]
    public void ALossIsWorthTheItemPlayed(char theirPlay, char ourPlay, int expectedScore)
    {
        CalculateTotalScore($"{theirPlay} {ourPlay}").Should().Be(expectedScore);
    }

    [TestCase(RockThem, WinIndicator, 8)]
    [TestCase(PaperThem, WinIndicator, 9)]
    [TestCase(ScissorsThem, WinIndicator, 7)]
    public void AWinIsWorthTheItemPlayedPlus6(char theirPlay, char ourPlay, int expectedScore)
    {
        CalculateTotalScore($"{theirPlay} {ourPlay}").Should().Be(expectedScore);
    }

    [Test]
    public void GoldenInputTestPart2()
    {
        var input = File.ReadAllText("../../../input.txt");
        input
            .Split("\r\n")
            .Select(x => CalculateTotalScore(x))
            .Sum()
            .Should().Be(10835);
    }

    private const char RockThem = 'A';
    private const char PaperThem = 'B';
    private const char ScissorsThem = 'C';
    private const char LoseIndicator = 'X';
    private const char DrawIndicator = 'Y';
    private const char WinIndicator = 'Z';
    private const int RockValue = 1;
    private const int PaperValue = 2;
    private const int ScissorsValue = 3;
    private const int LossValue = 0;
    private const int DrawValue = 3;
    private const int WinValue = 6;

    private int CalculateTotalScore(string input)
    {
        if (string.IsNullOrEmpty(input))
            return 0;

        RoShamBo theirItem = ToRoShamBo(input.First());
        RoShamBo ourItem = FigureOutWhatWeShouldPlay(input, theirItem);
        return GetValueOfOurPlayedItem(ourItem) + GetWinLossDrawValue(theirItem, ourItem);
    }

    private static RoShamBo FigureOutWhatWeShouldPlay(string input, RoShamBo theirItem)
    {
        char winDrawLossIndicator = input.Last();
        if (winDrawLossIndicator == LoseIndicator)
        {
            if(theirItem == RoShamBo.Rock) return RoShamBo.Scissors;
            if(theirItem == RoShamBo.Paper) return RoShamBo.Rock;
            if(theirItem == RoShamBo.Scissors) return RoShamBo.Paper;
        }
        else if(winDrawLossIndicator == WinIndicator)
        {
            if(theirItem == RoShamBo.Rock) return RoShamBo.Paper;
            if(theirItem == RoShamBo.Paper) return RoShamBo.Scissors;
            if(theirItem == RoShamBo.Scissors) return RoShamBo.Rock;
        }
        return theirItem;
    }

    private enum RoShamBo 
    {
        Rock,
        Paper,
        Scissors
    }

    private RoShamBo ToRoShamBo(char item)
    {
        if(item == RockThem) return RoShamBo.Rock;
        if(item == PaperThem) return RoShamBo.Paper;
        if(item == ScissorsThem) return RoShamBo.Scissors;
        return RoShamBo.Rock;
    }

    private int GetWinLossDrawValue(RoShamBo theirItem, RoShamBo ourItem)
    {
        if (theirItem == ourItem) return DrawValue;

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

    private int GetValueOfOurPlayedItem(RoShamBo ourItem)
    {
        if(ourItem == RoShamBo.Rock) return RockValue;
        if(ourItem == RoShamBo.Paper) return PaperValue;
        if(ourItem == RoShamBo.Scissors) return ScissorsValue;
        return 0;
    }
}