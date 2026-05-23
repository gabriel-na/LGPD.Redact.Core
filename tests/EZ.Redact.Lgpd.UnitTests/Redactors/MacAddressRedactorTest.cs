using EZ.Redact.Lgpd.Core.Redactors;
using EZ.Redact.Lgpd.UnitTests.Helpers;

namespace EZ.Redact.Lgpd.UnitTests.Redactors;

public class MacAddressRedactorTest
{
    [Theory]
    [InlineData("00:1A:2B:3C:4D:5E", "00:1A:2B:**:**:**")]
    [InlineData("00-1A-2B-3C-4D-5E", "00-1A-2B-**-**-**")]
    [InlineData("FF:FF:FF:FF:FF:FF", "FF:FF:FF:**:**:**")]
    [InlineData("aa:bb:cc:dd:ee:ff", "aa:bb:cc:**:**:**")]
    public void MacAddressRedactor_DeveMascararMac(string input, string expected)
    {
        var redactor = new MacAddressRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void MacAddressRedactor_DeveLidarComInputVazio()
    {
        var redactor = new MacAddressRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void MacAddressRedactor_DeveLidarComInputNulo()
    {
        var redactor = new MacAddressRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
