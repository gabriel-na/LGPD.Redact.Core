using LGPD.Redact.Core.Redactors;
using LGPD.Redact.UnitTests.Helpers;

namespace LGPD.Redact.UnitTests.Redactors;

public class DataGenericaRedactorTest
{
    [Theory]
    [InlineData("15/03/1990", "**/**/1990")]
    [InlineData("1/03/1990", "*/**/1990")]
    [InlineData("15/03/90", "**/**/90")]
    [InlineData("1990-03-15", "1990-**-**")]
    [InlineData("2024-12-25", "2024-**-**")]
    public void DataGenericaRedactor_DeveMascararDiaEMes(string input, string expected)
    {
        var redactor = new DataGenericaRedactor();
        AssertionHelper.Equal(redactor, input, expected);
    }

    [Fact]
    public void DataGenericaRedactor_DeveLidarComInputVazio()
    {
        var redactor = new DataGenericaRedactor();
        AssertionHelper.Equal(redactor, "", "");
    }

    [Fact]
    public void DataGenericaRedactor_DeveLidarComInputNulo()
    {
        var redactor = new DataGenericaRedactor();
        AssertionHelper.Equal(redactor, null!, "");
    }
}
