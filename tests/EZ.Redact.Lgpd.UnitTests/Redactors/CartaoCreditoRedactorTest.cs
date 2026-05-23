using System;
using EZ.Redact.Lgpd.Core.Redactors;
using EZ.Redact.Lgpd.UnitTests.Helpers;

namespace EZ.Redact.Lgpd.UnitTests.Redactors;

public class CartaoCreditoRedactorTest
{
    [Theory]
    [InlineData("4532 1178 9012 3456", "4532 **** **** 3456")]
    public void CartaoCreditoRedactor_DeveSeguirPadraoBancario(string input, string expected)
    {
        var redactor = new CartaoCreditoRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void CartaoCreditoRedactor_DeveLidarComInputVazio()
    {
        var redactor = new CartaoCreditoRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void CartaoCreditoRedactor_DeveLidarComInputNulo()
    {
        var redactor = new CartaoCreditoRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
