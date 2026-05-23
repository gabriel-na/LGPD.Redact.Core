using System;
using EZ.Redact.Lgpd.Core.Redactors;
using EZ.Redact.Lgpd.UnitTests.Helpers;

namespace EZ.Redact.Lgpd.UnitTests.Redactors;

public class TelefoneRedactorTest
{
    [Theory]
    [InlineData("(11) 9 8888-4444", "(11) 9 ****-4444")]
    [InlineData("11 9 8888-4444", "11 9 ****-4444")]
    [InlineData("11 9 88884444", "11 9 ****4444")]
    [InlineData("11 988888444", "11 9****8444")]
    public void TelefoneRedactor_DevePreservarDDDeFinal(string input, string expected)
    {
        var redactor = new TelefoneRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void TelefoneRedactor_DeveLidarComInputVazio()
    {
        var redactor = new TelefoneRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void TelefoneRedactor_DeveLidarComInputNulo()
    {
        var redactor = new TelefoneRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
