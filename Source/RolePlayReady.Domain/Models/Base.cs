﻿using System.Validations.Extensions;

namespace RolePlayReady.Models;

public abstract record Base<TKey> : Persistent<TKey>, IBase<TKey> {

    protected Base(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public const int MaxNameSize = 100;
    public required string Name { get; init; }

    public const int MaxDescriptionSize = 1000;
    public required string Description { get; init; }

    public const int MaxShortNameSize = 10;
    public string? ShortName { get; init; }

    public const int MaxTagSize = 20;
    public IList<string> Tags { get; init; } = new List<string>();

    public virtual ValidationResult Validate() {
        var result = new ValidationResult();
        result += Name.Is().NotNull().And.NotEmptyOrWhiteSpace().And.NotLongerThan(MaxNameSize).Result;
        result += Description.Is().NotNull().And.NotEmptyOrWhiteSpace().And.NotLongerThan(MaxDescriptionSize).Result;
        result += ShortName.Is().NotEmptyOrWhiteSpace().And.NotLongerThan(MaxShortNameSize).Result;
        result += Tags.ForEach(item => item.Is().NotNull().And.NotLongerThan(MaxTagSize)).Result;
        return result;
    }

    public sealed override string ToString() => $"[{GetType().Name}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}";
}
