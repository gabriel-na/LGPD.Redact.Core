using EZ.Redact.Lgpd.Core.Redactors;
using EZ.Redact.Lgpd.UnitTests.Helpers;

namespace EZ.Redact.Lgpd.UnitTests.Redactors;

public class EnderecoIPRedactorTest
{
    [Theory]
    [InlineData("192.168.1.100", "192.168.*.***")]
    [InlineData("10.0.0.1", "10.0.*.*")]
    [InlineData("255.255.255.0", "255.255.***.*")]
    [InlineData("0.0.0.0", "0.0.*.*")]
    [InlineData("2001:0db8:85a3:0000:0000:8a2e:0370:7334", "2001:0db8:85a3:0000:0000:****:****:****")]
    [InlineData("fe80:0000:0000:0000:0000:0000:0000:0001", "fe80:0000:0000:0000:0000:****:****:****")]
    public void EnderecoIPRedactor_DeveMascararIP(string input, string expected)
    {
        var redactor = new EnderecoIPRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void EnderecoIPRedactor_DeveLidarComInputVazio()
    {
        var redactor = new EnderecoIPRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void EnderecoIPRedactor_DeveLidarComInputNulo()
    {
        var redactor = new EnderecoIPRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
