﻿namespace RoleplayReady.Domain.Models.Contracts;

public interface IElementAttribute : IAmChildOf, IHaveElement, IHaveAttribute, IHaveValue { }

public interface IElementAttribute<TValue> : IElementAttribute, IHaveValue<TValue> { }