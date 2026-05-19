using LGPD.Redact.Core.Redactors;
using LGPD.Redact.UnitTests.Helpers;

namespace LGPD.Redact.UnitTests.Redactors;

public class PISRedactorTest
{
    [Theory]
    [InlineData("123.45678.90-1", "123.*****.**-1")]
    [InlineData("12345678901", "123*******1")]
    [InlineData("98765432100", "987*******0")]
    [InlineData("111.22233.44-5", "111.*****.**-5")]
    public void PISRedactor_DeveMascararMiolo(string input, string expected)
    {
        var redactor = new PISRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void PISRedactor_DeveLidarComInputVazio()
    {
        var redactor = new PISRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void PISRedactor_DeveLidarComInputNulo()
    {
        var redactor = new PISRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
