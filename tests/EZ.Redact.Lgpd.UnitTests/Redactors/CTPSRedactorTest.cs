using EZ.Redact.Lgpd.Core.Redactors;
using EZ.Redact.Lgpd.UnitTests.Helpers;

namespace EZ.Redact.Lgpd.UnitTests.Redactors;

public class CTPSRedactorTest
{
    [Theory]
    [InlineData("1234567890", "123****890")]
    [InlineData("1234567", "123*567")]
    [InlineData("1234567890123", "123*******123")]
    [InlineData("987654321", "987***321")]
    public void CTPSRedactor_DeveMascararMiolo(string input, string expected)
    {
        var redactor = new CTPSRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void CTPSRedactor_DeveLidarComInputVazio()
    {
        var redactor = new CTPSRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void CTPSRedactor_DeveLidarComInputNulo()
    {
        var redactor = new CTPSRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
