using NUnit.Framework;
using FluentAssertions;
using System.Linq;
using System.IO;
using System;

namespace day1;

public class Day1Tests
{
    static int line = 0;
    static string newline = "\r\n";

    [Test]
    public void NoElvesMeansNoCalories()
    {
        var input = "";
        FindHighestCalories(input).Should().Be(0);
    }

    [Test]
    public void ForOneElfJustTotalAllCalories()
    {
        var input = $"1000{newline}2000{newline}3000";
        FindHighestCalories(input).Should().Be(6000);
    }

    [Test]
    public void ForTwoElvesPickTheElfWithTheHighestCalories()
    {
        var input = $"1000{newline}2000{newline}3000{newline}{newline}7000";
        FindHighestCalories(input).Should().Be(7000);
    }

    [Test]
    public void GoldenInputTest()
    {
        line = 0;
        var input = File.ReadAllText("../../../input.txt");
        FindHighestCalories(input).Should().Be(66306);
    }

    int FindHighestCalories(string input)
    {
        if(string.IsNullOrEmpty(input))
            return 0;

        return splitByElf(input)
            .Select(calculateTotalCalories)
            .Max();
    }

    string[] splitByElf(string input)
    {
        return input.Split($"{newline}{newline}");
    }

    int calculateTotalCalories(string input)
    {
        return input.Split($"{newline}").Select(x =>
        {
            line++;
            if(int.TryParse(x, out int result))
                return result;
            throw new Exception($"Couldn't parse '{x}' at line {line}");
        }
        ).Sum();
    }
}