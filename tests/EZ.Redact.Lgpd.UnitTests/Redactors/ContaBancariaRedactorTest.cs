using EZ.Redact.Lgpd.Core.Redactors;
using EZ.Redact.Lgpd.UnitTests.Helpers;

namespace EZ.Redact.Lgpd.UnitTests.Redactors;

public class ContaBancariaRedactorTest
{
    [Theory]
    [InlineData("013.123456-7", "013.******-7")]
    [InlineData("013.123456789-7", "013.*********-7")]
    [InlineData("12345-6", "*****-6")]
    [InlineData("123456789-0", "*********-0")]
    [InlineData("001.12345-6", "001.*****-6")]
    [InlineData("13.123456-7", "13.******-7")]
    [InlineData("123456-7", "******-7")]
    [InlineData("013.123456-A", "013.******-A")]
    public void ContaBancariaRedactor_DeveMascararConta(string input, string expected)
    {
        var redactor = new ContaBancariaRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void ContaBancariaRedactor_DeveLidarComInputVazio()
    {
        var redactor = new ContaBancariaRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void ContaBancariaRedactor_DeveLidarComInputNulo()
    {
        var redactor = new ContaBancariaRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
