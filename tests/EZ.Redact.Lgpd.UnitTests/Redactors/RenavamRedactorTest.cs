using EZ.Redact.Lgpd.Core.Redactors;
using EZ.Redact.Lgpd.UnitTests.Helpers;

namespace EZ.Redact.Lgpd.UnitTests.Redactors;

public class RenavamRedactorTest
{
    [Theory]
    [InlineData("12345678901", "123*****901")]
    [InlineData("00000000000", "000*****000")]
    [InlineData("11111111111", "111*****111")]
    public void RenavamRedactor_DeveMascararMiolo(string input, string expected)
    {
        var redactor = new RenavamRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void RenavamRedactor_DeveLidarComInputVazio()
    {
        var redactor = new RenavamRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void RenavamRedactor_DeveLidarComInputNulo()
    {
        var redactor = new RenavamRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
