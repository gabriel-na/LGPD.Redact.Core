using System;
using EZ.Redact.Lgpd.Core.Redactors;
using EZ.Redact.Lgpd.UnitTests.Helpers;

namespace EZ.Redact.Lgpd.UnitTests.Redactors;

public class PixRedactorTeste
{
    [Theory]
    [InlineData("e8d26618-2e11-4b22-8d26-66182e114b22", "e8d2****-****-****-****-****2e114b22")]
    public void PixRedactor_DeveMascararChaveAleatoria(string input, string expected)
    {
        var redactor = new PixRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void PixRedactor_DeveLidarComInputVazio()
    {
        var redactor = new PixRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void PixRedactor_DeveLidarComInputNulo()
    {
        var redactor = new PixRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
