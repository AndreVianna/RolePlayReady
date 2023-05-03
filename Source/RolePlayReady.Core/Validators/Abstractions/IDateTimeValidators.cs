﻿namespace System.Validators.Abstractions;

public interface IDateTimeValidators {
    IConnectsToOrFinishes<IDateTimeValidators> IsAfter(DateTime reference);
    IConnectsToOrFinishes<IDateTimeValidators> IsBefore(DateTime reference);
    IConnectsToOrFinishes<IDateTimeValidators> StartsOn(DateTime reference);
    IConnectsToOrFinishes<IDateTimeValidators> EndsOn(DateTime reference);
}