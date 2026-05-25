using EZ.Redact.Lgpd.Core.Redactors;
using EZ.Redact.Lgpd.UnitTests.Helpers;

namespace EZ.Redact.Lgpd.UnitTests.Redactors;

public class CNHRedactorTest
{
    [Theory]
    [InlineData("12345678901", "123******01")]
    [InlineData("00000000000", "000******00")]
    [InlineData("11111111111", "111******11")]
    public void CnhRedactor_DeveMascararMiolo(string input, string expected)
    {
        var redactor = new CNHRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void CnhRedactor_DeveLidarComInputVazio()
    {
        var redactor = new CNHRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void CnhRedactor_DeveLidarComInputNulo()
    {
        var redactor = new CNHRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
