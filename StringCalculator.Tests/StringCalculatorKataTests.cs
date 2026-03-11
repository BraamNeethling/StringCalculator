using NUnit.Framework;

namespace StringCalculator.Tests;

public class StringCalculatorKataTests
{
    private readonly global::StringCalculator.StringCalculator _calculator = new();

    [Test]
    [Category("Step01")]
    public void Add_EmptyString_ReturnsZero()
    {
        Assert.That(_calculator.Add(string.Empty), Is.EqualTo(0));
    }

    [Test]
    [Category("Step01")]
    public void Add_SingleNumber_ReturnsThatNumber()
    {
        Assert.That(_calculator.Add("1"), Is.EqualTo(1));
    }

    [Test]
    [Category("Step01")]
    public void Add_TwoNumbers_ReturnsSum()
    {
        Assert.That(_calculator.Add("1,2"), Is.EqualTo(3));
    }

    [Test]
    [Category("Step02")]
    [TestCase("4,5,6", 15)]
    [TestCase("1,2,3,4,5", 15)]
    public void Add_UnknownAmountOfNumbers_ReturnsSum(string input, int expected)
    {
        Assert.That(_calculator.Add(input), Is.EqualTo(expected));
    }

    [Test]
    [Category("Step04")]
    [TestCase("1\n2", 3)]
    [TestCase("1\n2,3", 6)]
    public void Add_NewLinesBetweenNumbers_AreHandled(string input, int expected)
    {
        Assert.That(_calculator.Add(input), Is.EqualTo(expected));
    }

    [Test]
    [Category("Step05")]
    [TestCase("//;\n1;2", 3)]
    [TestCase("//|\n1|2|3", 6)]
    public void Add_CustomSingleCharacterDelimiter_IsHandled(string input, int expected)
    {
        Assert.That(_calculator.Add(input), Is.EqualTo(expected));
    }

    [Test]
    [Category("Step06")]
    public void Add_NegativeNumber_ThrowsExceptionWithNegativeValue()
    {
        var ex = Assert.Throws<ArgumentException>(() => _calculator.Add("1,-2,3"));

        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Does.Contain("-2"));
    }

    [Test]
    [Category("Step06")]
    public void Add_MultipleNegativeNumbers_ThrowsExceptionWithAllNegativeValues()
    {
        var ex = Assert.Throws<ArgumentException>(() => _calculator.Add("-1,2,-3"));

        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Does.Contain("-1"));
        Assert.That(ex!.Message, Does.Contain("-3"));
    }

    [Test]
    [Category("Step07")]
    [TestCase("2,1001", 2)]
    [TestCase("1000,2", 1002)]
    [TestCase("1001,1002", 0)]
    public void Add_NumbersGreaterThan1000_AreIgnored(string input, int expected)
    {
        Assert.That(_calculator.Add(input), Is.EqualTo(expected));
    }

    [Test]
    [Category("Step08")]
    [TestCase("//[***]\n1***2***3", 6)]
    [TestCase("//[abc]\n4abc5", 9)]
    public void Add_CustomDelimiterWithAnyLength_IsHandled(string input, int expected)
    {
        Assert.That(_calculator.Add(input), Is.EqualTo(expected));
    }

    [Test]
    [Category("Step09")]
    [TestCase("//[*][%]\n1*2%3", 6)]
    [TestCase("//[;][|]\n1;2|3", 6)]
    public void Add_MultipleCustomDelimiters_AreHandled(string input, int expected)
    {
        Assert.That(_calculator.Add(input), Is.EqualTo(expected));
    }

    [Test]
    [Category("Step10")]
    [TestCase("//[***][%%]\n1***2%%3", 6)]
    [TestCase("//[abc][def]\n4abc5def6", 15)]
    public void Add_MultipleCustomDelimitersWithMultipleLengths_AreHandled(string input, int expected)
    {
        Assert.That(_calculator.Add(input), Is.EqualTo(expected));
    }
}
