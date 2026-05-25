using EZ.Redact.Lgpd.Core.Redactors;
using EZ.Redact.Lgpd.UnitTests.Helpers;

namespace EZ.Redact.Lgpd.UnitTests.Redactors;

public class GuidRedactorTest
{
    [Theory]
    [InlineData("e8d26618-2e11-4b22-8d26-66182e114b22", "e8d2****-****-****-****-********4b22")]
    [InlineData("e8d266182e114b228d2666182e114b22", "e8d2************************4b22")]
    public void GuidRedactor_DeveMascararGuid(string input, string expected)
    {
        var redactor = new GuidRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void GuidRedactor_DeveLidarComInputVazio()
    {
        var redactor = new GuidRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void GuidRedactor_DeveLidarComInputNulo()
    {
        var redactor = new GuidRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
