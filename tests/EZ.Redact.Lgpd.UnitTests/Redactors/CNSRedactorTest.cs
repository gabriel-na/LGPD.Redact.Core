using EZ.Redact.Lgpd.Core.Redactors;
using EZ.Redact.Lgpd.UnitTests.Helpers;

namespace EZ.Redact.Lgpd.UnitTests.Redactors;

public class CNSRedactorTest
{
    [Theory]
    [InlineData("123 4567 8901 2345", "123 **** **** 2345")]
    [InlineData("123456789012345", "123********2345")]
    [InlineData("987 6543 2109 8765", "987 **** **** 8765")]
    public void CNSRedactor_DeveMascararMiolo(string input, string expected)
    {
        var redactor = new CNSRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void CNSRedactor_DeveLidarComInputVazio()
    {
        var redactor = new CNSRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void CNSRedactor_DeveLidarComInputNulo()
    {
        var redactor = new CNSRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
