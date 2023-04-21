﻿namespace System.Validations;

public static class CollectionValidation {
    public static IFinishesValidation ForEachItemIn<TItem>(IValidation<IEnumerable<TItem>?> validation, Func<TItem, IFinishesValidation> validateUsing, bool addIsNullError = true) {
        if (validation.Subject is null) {
            if (addIsNullError)
                validation.Errors.Add(new(CannotBeNull, validation.Source));
            return validation;
        }

        var dictionary = validation.Subject as IDictionary;
        var index = 0;
        foreach (var item in validation.Subject) {
            var key = dictionary is not null ? item!.GetType().GetProperty("Key")!.GetValue(item) : index++;
            var source = $"{validation.Source}[{key}]";
            foreach (var error in validateUsing(item).Result.Errors) {
                var path = ((string)error.Arguments[0]!).Split('.');
                var skip = dictionary is not null && path.Length > 1 && path[1] == "Value" ? 2 : 1;
                error.Arguments[0] = path.Length > skip ? $"{source}.{string.Join('.', path[skip..])}" : source;
                validation.Errors.Add(error);
            }
        }

        return validation;
    }
}

public class CollectionValidation<TItem> :
    Validation<ICollection<TItem>?, ICollectionValidators<TItem>>,
    ICollectionValidation<TItem> {
    public CollectionValidation(ICollection<TItem>? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<ICollectionValidators<TItem>> IsNotEmpty() {
        if (Subject is null) return this;
        if (!Subject.Any())
            Errors.Add(new(CannotBeEmpty, Source));
        return this;
    }

    public IConnectsToOrFinishes<ICollectionValidators<TItem>> MinimumCountIs(int size) {
        if (Subject is null) return this;
        if (Subject.Count < size)
            Errors.Add(new(CannotHaveLessThan, Source, size, Subject.Count));
        return this;
    }

    public IConnectsToOrFinishes<ICollectionValidators<TItem>> CountIs(int size) {
        if (Subject is null) return this;
        if (Subject.Count != size)
            Errors.Add(new(MustHave, Source, size, Subject.Count));
        return this;
    }

    public IConnectsToOrFinishes<ICollectionValidators<TItem>> Contains(TItem item) {
        if (Subject is null) return this;
        if (!Subject.Contains(item))
            Errors.Add(new(MustContain, Source, item));
        return this;
    }

    public IConnectsToOrFinishes<ICollectionValidators<TItem>> NotContains(TItem item) {
        if (Subject is null) return this;
        if (Subject.Contains(item))
            Errors.Add(new(MustNotContain, Source, item));
        return this;
    }

    public IConnectsToOrFinishes<ICollectionValidators<TItem>> MaximumCountIs(int size) {
        if (Subject is null) return this;
        if (Subject.Count > size)
            Errors.Add(new(CannotHaveMoreThan, Source, size, Subject.Count));
        return this;
    }

    public IFinishesValidation ForEach(Func<TItem, IFinishesValidation> validateUsing)
        => CollectionValidation.ForEachItemIn(this, validateUsing, false);
}