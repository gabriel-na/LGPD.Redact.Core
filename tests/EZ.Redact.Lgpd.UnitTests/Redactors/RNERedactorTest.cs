using EZ.Redact.Lgpd.Core.Redactors;
using EZ.Redact.Lgpd.UnitTests.Helpers;

namespace EZ.Redact.Lgpd.UnitTests.Redactors;

public class RNERedactorTest
{
    [Theory]
    [InlineData("V1234567-8", "V*******-8")]
    [InlineData("V123456-7", "V******-7")]
    [InlineData("1234567-8", "*******-8")]
    public void RNERedactor_DeveMascararNumero(string input, string expected)
    {
        var redactor = new RNERedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void RNERedactor_DeveLidarComInputVazio()
    {
        var redactor = new RNERedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void RNERedactor_DeveLidarComInputNulo()
    {
        var redactor = new RNERedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
