﻿namespace System.Validation.Builder.Abstractions;

public interface IBinaryOperator<out TValidator>
    where TValidator : IValidator {
    TValidator And(Func<TValidator, ITerminator> validateRight);
    TValidator Or(Func<TValidator, ITerminator> validateRight);
    TValidator AndNot(Func<TValidator, ITerminator> validateRight);
    TValidator OrNot(Func<TValidator, ITerminator> validateRight);
}