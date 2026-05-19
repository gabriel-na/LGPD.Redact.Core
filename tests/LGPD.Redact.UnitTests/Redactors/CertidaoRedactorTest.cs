using LGPD.Redact.Core.Redactors;
using LGPD.Redact.UnitTests.Helpers;

namespace LGPD.Redact.UnitTests.Redactors;

public class CertidaoRedactorTest
{
    [Theory]
    [InlineData("123456.78.1234.5.6.7890.1.12345-67", "123456.**.****.*.*.****.*.*****-67")]
    [InlineData("12345678123456789011234567", "123456******************67")]
    public void CertidaoRedactor_DeveMascararMiolo(string input, string expected)
    {
        var redactor = new CertidaoRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void CertidaoRedactor_DeveLidarComInputVazio()
    {
        var redactor = new CertidaoRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void CertidaoRedactor_DeveLidarComInputNulo()
    {
        var redactor = new CertidaoRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
