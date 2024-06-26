namespace Application.Test;

using Application;

public class Tests
{
    [Theory]
    [InlineData(11)]
    [InlineData(43)]
    [InlineData(59)]
    public void IsBubblicious_IsBubblicious_ReturnsTrue(int number)
    {
        Assert.True(Bubblicious.IsBubblicious(number));
    }

    [Theory]
    [MemberData(nameof(EvenData))]
    public void IsBubblicious_IsEvenNotBubblicious_ReturnsFalse(int number)
    {
        Assert.False(Bubblicious.IsBubblicious(number));
    }

    public static IEnumerable<object[]> EvenData()
    {
        yield return new object[] { 2 };
        yield return new object[] { 50_000 };
        yield return new object[] { 90_500 };
    }
}
