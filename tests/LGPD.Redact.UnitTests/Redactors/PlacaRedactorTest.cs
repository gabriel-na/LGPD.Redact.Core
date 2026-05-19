using LGPD.Redact.Core.Redactors;
using LGPD.Redact.UnitTests.Helpers;

namespace LGPD.Redact.UnitTests.Redactors;

public class PlacaRedactorTest
{
    [Theory]
    [InlineData("ABC-1234", "ABC-****")]
    [InlineData("ABC1234", "ABC****")]
    [InlineData("XYZ-9876", "XYZ-****")]
    [InlineData("ABC1D23", "ABC****")]
    [InlineData("XYZ9Z99", "XYZ****")]
    [InlineData("BRA0A00", "BRA****")]
    public void PlacaRedactor_DeveMascararPlaca(string input, string expected)
    {
        var redactor = new PlacaRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void PlacaRedactor_DeveLidarComInputVazio()
    {
        var redactor = new PlacaRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void PlacaRedactor_DeveLidarComInputNulo()
    {
        var redactor = new PlacaRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
