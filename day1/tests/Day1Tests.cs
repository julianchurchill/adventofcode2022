using NUnit.Framework;
using FluentAssertions;
using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;

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
    public void GoldenInputTestPart1()
    {
        line = 0;
        var input = File.ReadAllText("../../../input.txt");
        FindHighestCalories(input).Should().Be(66306);
    }

    [Test]
    public void GoldenInputTestPart2()
    {
        line = 0;
        var input = File.ReadAllText("../../../input.txt");
        FindTop3HighestCalories(input).Should().Be(195292);
    }

    int FindTop3HighestCalories(string input)
    {
        var calories = FindAllCalories(input);
        if(calories.Count() == 0)
            return 0;
        return calories.OrderByDescending(x => x).Take(3).Sum();
    }

    int FindHighestCalories(string input)
    {
        var calories = FindAllCalories(input);
        if(calories.Count() == 0)
            return 0;
        return calories.Max();
    }

    IEnumerable<int> FindAllCalories(string input)
    {
        if(string.IsNullOrEmpty(input))
            return new List<int>();

        return splitByElf(input)
            .Select(calculateTotalCalories);
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