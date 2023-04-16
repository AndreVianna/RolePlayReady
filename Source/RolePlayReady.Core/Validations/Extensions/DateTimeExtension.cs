﻿namespace System.Validations.Extensions;

public static class DateTimeExtension {
    public static IDateTimeValidation Is(this DateTime subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DateTimeValidation(subject, source);
}