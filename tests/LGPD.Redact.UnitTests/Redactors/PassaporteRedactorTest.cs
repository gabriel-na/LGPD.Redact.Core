using LGPD.Redact.Core.Redactors;
using LGPD.Redact.UnitTests.Helpers;

namespace LGPD.Redact.UnitTests.Redactors;

public class PassaporteRedactorTest
{
    [Theory]
    [InlineData("AB123456", "AB****56")]
    [InlineData("AB-123456", "AB-****56")]
    [InlineData("AB 123456", "AB ****56")]
    [InlineData("C12345678", "C******78")]
    [InlineData("ABC123456", "ABC****56")]
    [InlineData("123456789", "*******89")]
    public void PassaporteRedactor_DeveMascararNumero(string input, string expected)
    {
        var redactor = new PassaporteRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void PassaporteRedactor_DeveLidarComInputVazio()
    {
        var redactor = new PassaporteRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void PassaporteRedactor_DeveLidarComInputNulo()
    {
        var redactor = new PassaporteRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
