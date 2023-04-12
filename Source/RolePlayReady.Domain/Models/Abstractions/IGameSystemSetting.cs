﻿namespace RolePlayReady.Models.Abstractions;

public interface IGameSystemSetting : IBase<Guid> {
    IList<AttributeDefinition> AttributeDefinitions { get; }
}