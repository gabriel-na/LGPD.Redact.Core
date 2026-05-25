using System;
using EZ.Redact.Lgpd.Core.Redactors;
using EZ.Redact.Lgpd.UnitTests.Helpers;

namespace EZ.Redact.Lgpd.UnitTests.Redactors;

public class EmailRedactorTest
{
    [Theory]
    [InlineData("felipe.siqueira@gmail.com", "f**************@gmail.com")]
    [InlineData("a@empresa.com", "a@empresa.com")]
    public void EmailRedactor_DevePreservarApenasDominioEInicial(string input, string expected)
    {
        var redactor = new EmailRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void EmailRedactor_DeveLidarComInputVazio()
    {
        var redactor = new EmailRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void EmailRedactor_DeveLidarComInputNulo()
    {
        var redactor = new EmailRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
