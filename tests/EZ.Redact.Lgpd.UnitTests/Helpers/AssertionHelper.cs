using System;
using Microsoft.Extensions.Compliance.Redaction;

namespace EZ.Redact.Lgpd.UnitTests.Helpers;

public static class AssertionHelper
{
    public static void Equal(Redactor redactor, string input, string expected)
    {
        var source = input.AsSpan();
        Span<char> destination = new char[redactor.GetRedactedLength(source)];
        redactor.Redact(source, destination);
        Assert.Equal(expected, destination.ToString());
    }
}
