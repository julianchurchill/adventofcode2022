using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace day3;

public class Day3Tests
{
    [Test]
    public void SumPrioritiesOfDuplicateItemsForEmptyRucksackIsZero()
    {
        SumPrioritiesOfDuplicateItems("").Should().Be(0);
    }

    [TestCase("aa", 1)]
    [TestCase("bb", 2)]
    [TestCase("cc", 3)]
    [TestCase("AA", 27)]
    public void SumPrioritiesOfDuplicateItems_OneSimpleRucksack(string rucksack, int expectedSum)
    {
        SumPrioritiesOfDuplicateItems(rucksack).Should().Be(expectedSum);
    }

    [TestCase("aa", "bb", 3)]
    public void SumPrioritiesOfDuplicateItems_TwoSimpleRucksacks(string rucksack1, string rucksack2, int expectedSum)
    {
        SumPrioritiesOfDuplicateItems(string.Join("\r\n", rucksack1, rucksack2)).Should().Be(expectedSum);
    }

    [TestCase("abcdeb", 2)]
    [TestCase("mfdfdfdHfQplZJJh", 6)]
    public void SumPrioritiesOfDuplicateItems_OneComplexRucksack(string rucksack, int expectedSum)
    {
        SumPrioritiesOfDuplicateItems(rucksack).Should().Be(expectedSum);
    }

    [Test]
    public void GoldenInputTestPart1()
    {
        var input = File.ReadAllText("../../../input.txt");
        SumPrioritiesOfDuplicateItems(input)
            .Should().Be(7875);
    }

    private object SumPrioritiesOfDuplicateItems(string rucksacks)
    {
        if (string.IsNullOrEmpty(rucksacks))
            return 0;

        return rucksacks.Split("\r\n")
            .Select(FindDuplicateItem)
            .Select(GetItemPriorityValue)
            .Sum();
    }

    private static char FindDuplicateItem(string rucksack)
    {
        string firstCompartment = rucksack.Substring(0, rucksack.Length / 2);
        string secondCompartment = rucksack.Substring(rucksack.Length / 2);
        return firstCompartment.Intersect(secondCompartment).First();
    }

    private static int GetItemPriorityValue(char dupe)
    {
        if(dupe >= 'a' && dupe <= 'z')
            return dupe - 'a' + 1;
        return dupe - 'A' + 27;
    }
}